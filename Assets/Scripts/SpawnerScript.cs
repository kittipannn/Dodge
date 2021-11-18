using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject spawnerArea;
    string[] tagObstacle = { "Obs1", "Obs2", "Obs3" };



    private float elapsedTimeObs = 0.0f;
    [SerializeField] float timeToSpawnSingle = 1;
    [SerializeField] float timeToSpawnWave = 0.5f;
    private float elapsedTimePotion = 0.0f;
    [SerializeField] float timeToSpawnPotion = 10;
    private float elapsedTimeFever = 0.0f;
    [SerializeField] float timeToSpawnFever = 15;


    float time = 0;
    [SerializeField] float timeSingle = 0; //‡√‘Ë¡µÕπ‰Àπ¢Õßsingle
    bool startSingle = false;
    [SerializeField] float timeWave = 10;
    bool startWave = false;
    int tagIndex;
    // Update is called once per frame
    void Update()
    {
        if (GameSetting.gamesettingInstance.startGame && !GameSetting.gamesettingInstance.playerDead )
        {
            if (GameSetting.gamesettingInstance.tutorials)
            {
                OnSpawnObstacle();
                spawnController();
                OnSpawnPotion();
                OnSpawnFever();
            }
        }
        
    }

    void spawnController() 
    {
        time += Time.deltaTime;
        if (time >= timeSingle && !startSingle)
        {
            startSingle = true;
            timeSingle += 15;
            StartCoroutine("onSpawnSingle");
        }
        if (time >= timeWave && !startWave)
        {
            startWave = true;
            tagIndex = Random.Range(0, tagObstacle.Length);
            timeWave += 15;
            StartCoroutine("onWaveSpawn");
        }
    }
    void OnSpawnObstacle()
    {
        if (startSingle)
        {
            elapsedTimeObs += Time.deltaTime;
            if (elapsedTimeObs > timeToSpawnSingle)
            {
                elapsedTimeObs = 0;
                spawnSingle();
            }
        }
        else if (startWave)
        {
            elapsedTimeObs += Time.deltaTime;
            if (elapsedTimeObs > timeToSpawnWave)
            {
                elapsedTimeObs = 0;
                spawnWave(tagIndex);
            }
        }
    }
    void OnSpawnPotion() 
    {
        elapsedTimePotion += Time.deltaTime;
        if (elapsedTimePotion > timeToSpawnPotion)
        {
            elapsedTimePotion = 0;
            spawnPotion(randomPos());
        }
    }
    void OnSpawnFever()
    {
        elapsedTimeFever += Time.deltaTime;
        if (elapsedTimeFever > timeToSpawnFever)
        {
            elapsedTimeFever = 0;
            
            spawnFever(randomPos());
        }
    }
    

    void spawnSingle() 
    {
        int tagIndex = Random.Range(0, tagObstacle.Length);
        string tag = tagObstacle[tagIndex];
        if (true)
        {
            spawnObstacle(randomPos(), tag);
        }
        else if (true) // °”·æß
        {
            spawnObstacle(WallObsPos(), "Obs3");
        }

    }
    void spawnWave(int tagindex) 
    {
        string tag = tagObstacle[tagindex];
        spawnObstacle(randomPos(), tag);
    }
    IEnumerator onSpawnSingle()
    {
        yield return new WaitForSeconds(10);
        startSingle = false;

    }
    IEnumerator onWaveSpawn()
    {
        yield return new WaitForSeconds(5);
        startWave = false;
    }

    public void setSpawner() // when player watch ads
    {
        time = timeSingle;
    }
    void spawnObstacle(Vector2 position , string tagObs)
    {
        GameObject obstacle = ObjectPooler.ObjectPoolInstance.GetPooledObJect(tagObs);
        obstacle.transform.position = position;
        obstacle.SetActive(true);
    }
    void spawnFever(Vector2 position)
    {
        GameObject fever = ObjectPooler.ObjectPoolInstance.GetPooledObJect("Fever");
        fever.transform.position = position;
        fever.SetActive(true);
    }
    void spawnPotion(Vector2 position) 
    {
        GameObject potion = ObjectPooler.ObjectPoolInstance.GetPooledObJect("Potion");
        potion.transform.position = position;
        potion.SetActive(true);
    }
    Vector2 randomPos()
    {
        Vector3 pos;
        Vector2 borderLeftCamera = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        Vector2 borderRightCamera = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        pos = new Vector3(Random.Range(borderLeftCamera.x + 0.5f, borderRightCamera.x - 0.5f)
            , spawnerArea.transform.position.y, spawnerArea.transform.position.z);
        return pos;
    }
    Vector2 WallObsPos()
    {
        Vector2 pos = new Vector2(0,0);
        Vector2 borderLeftCamera = Camera.main.ViewportToWorldPoint(new Vector2(0.1f, 1));
        Vector2 borderRightCamera = Camera.main.ViewportToWorldPoint(new Vector2(0.9f, 1));
        int indexPos = Random.RandomRange(0, 2);
        switch (indexPos)
        {
            case 0:
                pos = borderLeftCamera;
                break;
            case 1:
                pos = borderRightCamera;
                break;

        }
        return pos;
       
    }
    private void OnDrawGizmosSelected()
    {
        Vector2 borderLeftCamera = Camera.main.ViewportToWorldPoint(new Vector2(0.1f, 1));
        Vector2 borderRightCamera = Camera.main.ViewportToWorldPoint(new Vector2(0.9f, 1));
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(borderLeftCamera, 0.1F);
        Gizmos.DrawSphere(borderRightCamera, 0.1F);
    }

}
