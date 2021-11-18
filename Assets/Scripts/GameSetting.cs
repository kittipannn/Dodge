using UnityEngine;

public class GameSetting : MonoBehaviour
{
    [Header("Scripts")]
    public static GameSetting gamesettingInstance;
    [SerializeField] ScoreScript scoreScript;
    [SerializeField] CoinManager coinManager;
    [SerializeField] GamePlay gamePlay;
    [SerializeField] playerControl playerControl;
    [SerializeField] UiManager uiManager;
    

    [Header("GameObject")]
    public GameObject player;
    public GameObject mainCam, shopCam ,menuCam;
    public GameObject borderLeft, borderRight;

    [Header("Variables")]
    public bool playerDead = false;
    public bool startGame = false;
    public bool pauseGame = false;
    public bool onShop = false;
    public bool OnstartGame = false;
    public bool watchAds = false;
    public int countInterstitial = 0;
    bool setBorder = false;
    bool soundDead = false;
    public bool tutorials;
    public bool vibrate = true;
    //whenPlayerDead
    public Vector2 positionSpawnPlayer;
    private void Awake()
    {
        Time.timeScale = 1;
        if (gamesettingInstance == null)
        {
            gamesettingInstance = this;
        }
        tutorials = PlayerPrefs.GetInt("FirstPlay") == 1 ? true : false;
    }
    void Start()
    {
        playerDead = false;
        scoreScript.enabled = false;
        //setPosPlayer
        positionSpawnPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform.position;
        //set countInterstitial
        countInterstitial = PlayerPrefs.GetInt("countInterstitialAd" , 0);
        countInterstitial++;
        PlayerPrefs.SetInt("countInterstitialAd", countInterstitial);
        //Vibrate
        if (!PlayerPrefs.HasKey("vibrate"))
        {
            PlayerPrefs.SetInt("vibrate", 0);
            LoadValueSoundControl();
        }
        else
        {
            LoadValueSoundControl();
        }
        uiManager.UpdateButtonIcon();
    }
   
    // Update is called once per frame
    void Update()
    {
        if (startGame && !playerDead && !pauseGame && tutorials)
        {
            if (gamePlay.Health > 0)
                scoreScript.enabled = true;

            if (!setBorder)
            {
                setBorder = true;
                OnsetBorder();
            }
        }
        if (pauseGame)
        {
            scoreScript.enabled = false;
        }
        if (gamePlay.Health <= 0)
        {
            endGame();
        }

    }
    public void setTutorials() 
    {
        tutorials = true;
        uiManager.tutorialsShow();
        PlayerPrefs.SetInt("FirstPlay", tutorials ? 1 : 0);
        PlayerPrefs.Save();
    }

    void OnsetBorder() 
    {
        borderLeft.transform.position = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f));
        borderRight.transform.position = Camera.main.ViewportToWorldPoint(new Vector2(1, 0.5f));
    }
    void endGame() //กรณี player ตาย จะทำการ save highscore
    {
        scoreScript.saveHighscore();
        scoreScript.enabled = false;
        playerDead = true;
        if (!soundDead)
        {
            SoundManager.SoundInstance.Play("GameOver");
            soundDead = true;
        }
    }
    public void rewardWhenPlayerWatch() 
    {
        gamePlay.addHP(50);
        watchAds = true;
        player.SetActive(true);
        player.transform.position = GameObject.FindGameObjectWithTag("PosPlayer").GetComponent<Transform>().position;
        playerDead = false;
        uiManager.afterWatchAds();
        GameObject.FindObjectOfType<SpawnerScript>().setSpawner();
        soundDead = false;
        uiManager.OnPlayGame();
    }
    public void setCameraShop() 
    {
        if (onShop)
        {
            menuCam.SetActive(false);
            shopCam.SetActive(true);
        }
        else 
        {
            menuCam.SetActive(true);
            shopCam.SetActive(false);
        }
    }
    public void setMainCamera() 
    {
        menuCam.SetActive(false);
        mainCam.SetActive(true);
    }

    public void OnVibrateControl()
    {
        if (vibrate)
        {
            vibrate = false;
            Debug.Log("Not Vibrate");
        }
        else
        {
            vibrate = true;
            Handheld.Vibrate();
            Debug.Log("Vibrate");
        }
        SaveValueSoundControl();
        uiManager.UpdateButtonIcon();
    }
    private void LoadValueSoundControl()
    {
        vibrate = PlayerPrefs.GetInt("vibrate") == 0;
    }
    private void SaveValueSoundControl()
    {
        PlayerPrefs.SetInt("vibrate", vibrate ? 1 : 0);
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Vector2 borderLeftCamera = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
    //    //borderLeftCamera.x = borderLeftCamera.x + 1f;
    //    Vector2 borderRightCamera = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    //    //borderRightCamera.x -= 1f;
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(borderLeftCamera, 0.1F);
    //    Gizmos.DrawSphere(borderRightCamera, 0.1F);
    //}

}
