using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnButtonManager : MonoBehaviour
{
    [SerializeField] UiManager uiManager;
    [SerializeField] UIAnim uIAnim;
    [SerializeField] GameObject noInputPanel;
    [Header("Menu Panel")]
    public Button playBtn;
    public Button muteBtn;
    public Button shopBtn;
    public Button infoBtn;
    [Header("Result Panel")]
    public Button nothanksBtn;
    public Button menuResultBtn;
    [Header("InGame Panel")]
    public Button pauseBtn;
    [Header("Pause Panel")]
    public Button menuPauseBtn;
    public Button resumeBtn;
    bool onPause = false;
    [Header("Shop Panel")]
    public Button menuShopBtn;
    [Header("Ads Panel")]
    public Button watchAdsBtn;
    public GameObject panelMenu;
    public GameObject uiInGame;
    [Header("Info Panel")]
    public Button menuInfoBtn;
    [Header("Tutorials Panel")]
    public Button tapBtn;
    int checktap = 1;
    void Start()
    {
        //Menu Panel
        playBtn.onClick.AddListener(() => playGame());
        muteBtn.onClick.AddListener(() => SoundManager.SoundInstance.OnButtonSoundControl());
        shopBtn.onClick.AddListener(() => OpenShop());
        infoBtn.onClick.AddListener(() => OpenInfo());
        //infoBtn.onClick.AddListener(() => );
        //result Panel
        nothanksBtn.onClick.AddListener(() => OnNothanksBtn());
        menuResultBtn.onClick.AddListener(() => OnMenuBtn());
        //Ingame Panel
        pauseBtn.onClick.AddListener(() => OnPauseBtn());
        //Pause Panel
        menuPauseBtn.onClick.AddListener(() => OnMenuBtn());
        resumeBtn.onClick.AddListener(() => OnPauseBtn());
        //Shop Panel
        menuShopBtn.onClick.AddListener(() => CloseShop());
        //Ads Panel
        watchAdsBtn.onClick.AddListener(() => watchAds());
        //Info Panel
        menuInfoBtn.onClick.AddListener(() => CloseInfo());
        PreventInput(3.6f);
        //Tutorials Panel
        tapBtn.onClick.AddListener(() => tapTutorial());
    }


    void playGame()
    {
        uIAnim.playGameTween();
        PreventInput(1.6f);
    }
    void watchAds()
    {
        //ถ้า player ดู ads หลังจากที่ตาย
        GameObject.FindObjectOfType<AdsManager>().UserChoseToWatchGameOverRewardAds();
    }
    void OnNothanksBtn() 
    {
        uiManager.OnPanelResult();
        GameSetting.gamesettingInstance.watchAds = true;
    }
    void OnMenuBtn() 
    {
        SceneManager.LoadScene(0);
    }
    void OnPauseBtn() 
    {
        if (!onPause)
        {
            GameSetting.gamesettingInstance.pauseGame = true;
            onPause = true;
        }
        else
        {
            GameSetting.gamesettingInstance.pauseGame = false;
            onPause = false;
        }
        uiManager.OnPanelPause();
    }
    void OpenShop()
    {
        uIAnim.OpenShopTween(1.8f);
        PreventInput(1.9f);
        GameSetting.gamesettingInstance.onShop = true;
        GameSetting.gamesettingInstance.setCameraShop();
       //uiManager.OnPanelShop();
    }
    void CloseShop() 
    {
        uIAnim.CloseShopTween(2.5f);
        PreventInput(2.6f);
        GameSetting.gamesettingInstance.onShop = false;
        GameSetting.gamesettingInstance.setCameraShop();
        //uiManager.OnPanelShop();
    }
    void OpenInfo() 
    {
        uiManager.OnInfoPanel = true;
        uIAnim.OpenInfoTween();
        uiManager.OnPanelInfo();
        PreventInput(3.6f);
    }
    void CloseInfo()
    {
        uiManager.OnInfoPanel = false;
        uIAnim.CloseInFoTween();
        PreventInput(2);
    }
    void tapTutorial() 
    {
        switch (checktap)
        {
            case 1:
                uIAnim.page2();
                checktap++;
                break;
            case 2:
                uIAnim.closeTutorials();
                break;
            default:  
                checktap = 0;
                break;
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
