using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    float time = 0;
    float timeSingle = 0;
    bool startSingle = false;
    float timeWave = 10;
    bool startWave = false;


    private float elapsedTimeObs = 0.0f;
    
    private void Update()
    {
        
        Spawn();
        time += Time.deltaTime;
        onSpawn();
    }
    void Spawn() 
    {
        if (time >= timeSingle && !startSingle)
        {
            startSingle = true;
            timeSingle += 15;
            StartCoroutine("onSpawnSingle");
        }
        if (time >= timeWave && !startWave)
        {
            startWave = true;
            timeWave += 15;
            StartCoroutine("onWaveSpawn");
        }
    }
    void onSpawn()
    {
        if (startSingle)
        {
            elapsedTimeObs += Time.deltaTime;
            if (elapsedTimeObs > 1)
            {
                elapsedTimeObs = 0;
                Debug.Log("Spawn Single");
            }
        }
        else if (startWave)
        {
            elapsedTimeObs += Time.deltaTime;
            if (elapsedTimeObs > 0.5f)
            {
                elapsedTimeObs = 0;
                Debug.Log("Spawn Wave");
            }
        }
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
}
