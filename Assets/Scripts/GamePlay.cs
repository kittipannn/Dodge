using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{

    //Health System 
    private float health;
    public float Health { get => health; }

    private float maxHealth = 50;
    public float MaxHealth { get => maxHealth; set { maxHealth = value; } }

    //Fever System 
    private float maxfeverGauge = 20;
    public float MaxFeverGauge { get => maxfeverGauge; }

    private float feverGauge = 0;
    public float FeverGauge { get => feverGauge; set { feverGauge = value; } }
    bool Onfever = false;

    [SerializeField] int multiplyToDecreaseHp = 2;
    [SerializeField] int multiplyToDecreaseFever = 2;

    void Start()
    {
        health = maxHealth;
        //feverGauge = maxfeverGauge;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            health = 0;
        if (feverGauge <= 0)
            feverGauge = 0;
        if (health > 50)
            health = 50;
        if (GameSetting.gamesettingInstance.startGame) // ������������
        {
            if (health > 0) //Ŵ���ʹ��ʹ����
                decreaseHealth(multiplyToDecreaseHp);
            if (feverGauge >= maxfeverGauge) //check ��� gauge ��������ѧ ��������ӧҹ �ӻ���������¹�Ҩ���Ԥ
                Onfever = true;
            else if (feverGauge <= 0)
                Onfever = false;
        }

        if (Onfever)
            OnfeverMode();
    }
    void decreaseHealth( int multiply) 
    {
        health -= multiply * Time.deltaTime; 
    }
    void OnfeverMode() 
    {
        decreaseFever(multiplyToDecreaseFever);
    }
    void decreaseFever(int multiply)
    {
        feverGauge -= multiply * Time.deltaTime;
    }
    public void onHitObstacle(float damage) 
    {
        if (!Onfever)
            health -= damage;
    }

    public void addFeverGauge(float value) //���� hp �Ѻ fever gague
    {
        Debug.Log("Increses Fever");
        feverGauge += value;
    }
    public void addHP(float value) //���� hp �Ѻ fever gague
    {
        Debug.Log("Increses Health");
        health += value;
    }



}
