using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class UiManager : MonoBehaviour
{
    [SerializeField] ScoreScript scoreScript;

    [SerializeField] GamePlay gamePlay;
    [SerializeField] AdsManager adsManager;
    [SerializeField] CoinManager coinManager;


    public CinemachineBrain cinemachineBrain;
    public GameObject mainCamera, shopCamera, menuCamera;
    public GameObject player;
    //panel Menu
    public GameObject panelMenu;
    public GameObject purchaseNoAds;

    //UI In Game
    public GameObject panelInGame;
    public Text scoreText;
    public Slider healthSlider;
    public Slider feverSlider;

    //panel Ads
    public GameObject panelAds;
    public Text currentScoreAds;

    //panel Result
    public GameObject panelResult;
    public Text resultScore;
    public Text highScoreResult;

    //panel Option
    //public GameObject panelOption;

    //panel Pause
    public GameObject panelPause;
    public Text scorePause;
    //panel Shop
    public GameObject panelShop;
    public Text coin;

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
            OnPanelPause();
        }
        if (GameSetting.gamesettingInstance.playerDead)
        {
            if (!GameSetting.gamesettingInstance.watchAds)
            {
                OnPanelAds();
            }
            else
            {
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
            if (!GameSetting.gamesettingInstance.playerDead)
            {
                
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
        panelInGame.SetActive(false);
        highScoreResult.text = scoreScript.HighScore.ToString();
        resultScore.text = scoreScript.Score.ToString();
        panelAds.SetActive(false);
        panelResult.SetActive(true);
    }
    
    void OnPanelPause() 
    {
        if (GameSetting.gamesettingInstance.pauseGame) // bool pause = true
        {
            scorePause.text = scoreScript.Score.ToString();
            highScoreResult.text = scoreScript.HighScore.ToString();
            panelInGame.SetActive(false);
            panelPause.SetActive(true);
            Time.timeScale = 0;
        }
        else // pause = false
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
            panelMenu.SetActive(false);
            if (!cinemachineBrain.IsBlending)
            {
                ICinemachineCamera menuCam = menuCamera.GetComponent<ICinemachineCamera>();
                bool menuCamLive = CinemachineCore.Instance.IsLive(menuCam);
                if (!menuCamLive)
                {
                    updateCoinInShop();
                    panelShop.SetActive(true);
                }
            }
        }
        else
        {
            panelShop.SetActive(false);
            if (!cinemachineBrain.IsBlending)
            {
                ICinemachineCamera shopCam = shopCamera.GetComponent<ICinemachineCamera>();
                bool shopCamLive = CinemachineCore.Instance.IsLive(shopCam);
                if (!shopCamLive)
                {
                    panelMenu.SetActive(true);
                }
            }
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
}
