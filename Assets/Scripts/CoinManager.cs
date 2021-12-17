using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] ScoreScript score;
    
    //coin รวม
    private int coin;
    public int Coin { get => coin; } // โชว์หน้าหลัก
    public int coinValue = 10;
    //coin ต่อรอบ
    private int currentCoin;
    public int CurrentCoin { get => currentCoin; } // โชว์หน้าสรุปผล
    [SerializeField] private float ratioOfCoin = 0.5f;
    bool checkTotalCoin = false;
    void Start()
    {
        coin = PlayerPrefs.GetInt("CoinPoint");

    }
    private void Update()
    {
        if (GameSetting.gamesettingInstance.playerDead && !checkTotalCoin && GameSetting.gamesettingInstance.watchAds)
        {
            checkTotalCoin = true;
            float coin = score.Score * ratioOfCoin;
            currentCoin = Mathf.RoundToInt(coin);
            addCoinPoint(currentCoin);
        }
    }
    public void addCoinPoint(int coinPoint)
    {
        coin += coinPoint;
        PlayerPrefs.SetInt("CoinPoint", coin);
    }

    public void useCoin(int value) 
    {
        if (coin >= value)
        {
            coin -= value;
            PlayerPrefs.SetInt("CoinPoint", coin);
        }
        else
        {
            Debug.Log("Failed");
        }
    }
}
