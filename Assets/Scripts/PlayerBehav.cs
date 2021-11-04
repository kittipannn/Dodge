using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehav : MonoBehaviour
{
    playerControl playerControl;
    [SerializeField] CoinManager coinManager;
    [SerializeField] GamePlay gamePlay;
    public ParticleSystem psPlayerDead;


    private void Awake()
    {
        playerControl = this.GetComponent<playerControl>();
    }
    void Start()
    {
        playerControl.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameSetting.gamesettingInstance.startGame && GameSetting.gamesettingInstance.tutorials)
        {
            if (GameSetting.gamesettingInstance.playerDead)
            {
                playerControl.enabled = false;
                Instantiate(psPlayerDead, this.transform.position, Quaternion.identity);
                this.gameObject.SetActive(false);
            }
            else
            {
                playerControl.enabled = true;
            }
        }



        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        //if (collision.gameObject.CompareTag("Coin"))
        //{
        //    Debug.Log("Collect Coin");
        //    collision.gameObject.SetActive(false);
        //    coinManager.addCoinPoint(coinManager.coinValue);
        //}
        
        if (collision.gameObject.CompareTag("Floor"))
        {
            if (GameSetting.gamesettingInstance.OnstartGame)
            {
                GameSetting.gamesettingInstance.setMainCamera();
            }
        }
    }
}
