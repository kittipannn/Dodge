using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class UiManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] ScoreScript scoreScript;
    [SerializeField] GamePlay gamePlay;
    [SerializeField] AdsManager adsManager;
    [SerializeField] CoinManager coinManager;
    [SerializeField] UIAnim uIAnim;
    [SerializeField] GameObject noInputPanel;

    [Header("Objects")]
    public CinemachineBrain cinemachineBrain;
    public GameObject mainCamera, shopCamera, menuCamera;
    public GameObject player;

    [Header("Menu Panel")]
    public GameObject panelMenu;
    public GameObject purchaseNoAds;

    [Header("InGame Panel")]
    public GameObject panelInGame;
    public Text scoreText;
    public Slider healthSlider;
    public Slider feverSlider;

    [Header("Ads Panel")]
    public GameObject panelAds;
    public Text currentScoreAds;
    bool showAdsPanel = false;

    [Header("Result Panel")]
    public GameObject panelResult;
    public Text resultScore;
    public Text highScoreResult;
    bool showResultPanel = false;

    [Header("Pause Panel")]
    public GameObject panelPause;
    public Text scorePause;

    [Header("Shop Panel")]
    public GameObject panelShop;
    public Text coin;

    [Header("Info Panel")]
    public GameObject infoPanel;
    public bool OnInfoPanel = false;

    [Header("Tutorials Panel")]
    public GameObject tutorialsPanel;

    void Start()
    {
        healthSlider.maxValue = gamePlay.MaxHealth;
        feverSlider.maxValue = gamePlay.MaxFeverGauge;
        updateUIAds();
    }

    // Update is called once per frame
    void Update()
    {
        updateUIAds();
        if (GameSetting.gamesettingInstance.startGame)
        {
            updateUI(); // UI in Game
           
        }
        if (GameSetting.gamesettingInstance.playerDead)
        {
            if (!GameSetting.gamesettingInstance.watchAds && !showAdsPanel)
            {
                showAdsPanel = true;
                OnPanelAds();
            }
            else if(GameSetting.gamesettingInstance.watchAds && !showResultPanel)
            {
                showResultPanel = true;
                OnPanelResult();
            }
        }
        
        
    }
    private void LateUpdate()
    {
        if (!GameSetting.gamesettingInstance.startGame && !GameSetting.gamesettingInstance.OnstartGame)
        {
            OnPanelShop();
        }
    }
    void updateUI()
    {
        //text
        scoreText.text = scoreScript.Score.ToString();

        //gague
        healthSlider.value = gamePlay.Health;
        feverSlider.value = gamePlay.FeverGauge;

        //Ads
       
    }
    public void tutorialsShow() 
    {
        if (GameSetting.gamesettingInstance.startGame)
        {
            if (!GameSetting.gamesettingInstance.tutorials)
            {
                tutorialsPanel.SetActive(true);
                uIAnim.page1();
                //GameSetting.gamesettingInstance.tutorials = true;
                //PlayerPrefs.SetInt("FirstPlay", GameSetting.gamesettingInstance.tutorials ? 1 : 0);
                //PlayerPrefs.Save();
            }
            else
            {
                tutorialsPanel.SetActive(false);
            }
        }
    }
    public void OnPlayGame() 
    {
        if (!GameSetting.gamesettingInstance.OnstartGame)
        {
            GameSetting.gamesettingInstance.OnstartGame = true;
            panelMenu.SetActive(false);
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        if (GameSetting.gamesettingInstance.startGame)
        {
            if (!GameSetting.gamesettingInstance.playerDead && !GameSetting.gamesettingInstance.pauseGame)
            {
                tutorialsShow();
                panelInGame.SetActive(true);
            }
            else
            {
                panelInGame.SetActive(false);
            }
        }
    }
    public void afterWatchAds() 
    {
        panelAds.SetActive(false);
        panelResult.SetActive(false);
    }
    public void OnPanelAds() 
    {
        currentScoreAds.text = scoreScript.Score.ToString();
        panelInGame.SetActive(false);
        panelAds.SetActive(true);
        uIAnim.AdsTween();
        PreventInput(2.1f);
    }
    public void OnPanelResult() 
    {
        int countInterAds = GameSetting.gamesettingInstance.countInterstitial;
        if (countInterAds >= 3)
        {
            adsManager.showInterstitialAds();
            GameSetting.gamesettingInstance.countInterstitial = 0;
            PlayerPrefs.SetInt("countInterstitialAd", GameSetting.gamesettingInstance.countInterstitial);
        }
        highScoreResult.text = scoreScript.HighScore.ToString();
        resultScore.text = scoreScript.Score.ToString();
        panelAds.SetActive(false);
        panelResult.SetActive(true);
        uIAnim.resultTween(1, 2);
        PreventInput(2.1f);
    }

    public void OnPanelPause() 
    {
        if (GameSetting.gamesettingInstance.pauseGame) // bool pause = true
        {
            scorePause.text = scoreScript.Score.ToString();
            highScoreResult.text = scoreScript.HighScore.ToString();
            panelInGame.SetActive(false);
            panelPause.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            panelPause.SetActive(false);
            panelInGame.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void OnPanelShop() 
    {
        if (GameSetting.gamesettingInstance.onShop)
        {

            if (!cinemachineBrain.IsBlending)
            {
                ICinemachineCamera menuCam = menuCamera.GetComponent<ICinemachineCamera>();
                bool menuCamLive = CinemachineCore.Instance.IsLive(menuCam);
                if (!menuCamLive)
                {
                    updateCoinInShop();
                    panelMenu.SetActive(false);
                    panelShop.SetActive(true);
                }
            }
        }
        else
        {
            panelShop.SetActive(false);
            panelMenu.SetActive(true);
            //if (!cinemachineBrain.IsBlending)
            //{
            //    ICinemachineCamera shopCam = shopCamera.GetComponent<ICinemachineCamera>();
            //    bool shopCamLive = CinemachineCore.Instance.IsLive(shopCam);
            //    if (!shopCamLive && !OnInfoPanel)
            //    {
            //        panelMenu.SetActive(true);
            //    }
            //}
        }
    }
    public void OnPanelInfo() 
    {
        if (OnInfoPanel)
        {
            panelMenu.SetActive(false);
            infoPanel.SetActive(true);
        }
        else
        {
            panelMenu.SetActive(true);
            infoPanel.SetActive(false);
        }
    }
    public void updateCoinInShop() 
    {
        coin.text = coinManager.Coin.ToString();
    }

    public void updateUIAds() 
    {
        if (PlayerPrefs.HasKey("ads"))
        {
            purchaseNoAds.SetActive(true);
        }
        else if (!PlayerPrefs.HasKey("ads"))
        {
            purchaseNoAds.SetActive(false);
        }
    }
    void PreventInput(float duration)
    {
        noInputPanel.transform.SetAsLastSibling();
        StartCoroutine(ChangeSceneDelay(noInputPanel, noInputPanel, duration));
    }

    IEnumerator ChangeSceneDelay(GameObject newCanvas, GameObject oldCanvas, float duration)
    {
        newCanvas.SetActive(true);
        yield return new WaitForSeconds(duration);
        oldCanvas.SetActive(false);
    }
}
