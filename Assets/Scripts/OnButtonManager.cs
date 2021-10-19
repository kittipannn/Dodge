using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnButtonManager : MonoBehaviour
{
    [SerializeField] UiManager uiManager;
    
    //Menu Panel
    public Button playBtn;
    public Button muteBtn;
    public Button shopBtn;
    //result Panel
    public Button nothanksBtn;
    public Button menuResultBtn;
    //Ingame Panel
    public Button pauseBtn;
    //Pause Panel
    public Button menuPauseBtn;
    public Button resumeBtn;
    bool onPause = false;
    //Shop Panel
    public Button menuShopBtn;
    //Ads Panel
    public Button watchAdsBtn;
    public GameObject panelMenu;
    public GameObject uiInGame;

    void Start()
    {
        //Menu Panel
        playBtn.onClick.AddListener(() => playGame());
        muteBtn.onClick.AddListener(() => SoundManager.SoundInstance.OnButtonSoundControl());
        shopBtn.onClick.AddListener(() => OpenShop());
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
    }


    void playGame()
    {
        uiManager.OnPlayGame();

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
    }
    void OpenShop()
    {
        GameSetting.gamesettingInstance.onShop = true;
        GameSetting.gamesettingInstance.setCameraShop();
       //uiManager.OnPanelShop();
    }
    void CloseShop() 
    {
        GameSetting.gamesettingInstance.onShop = false;
        GameSetting.gamesettingInstance.setCameraShop();
        //uiManager.OnPanelShop();
    }
}
