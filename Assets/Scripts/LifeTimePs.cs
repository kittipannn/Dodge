using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimePs : MonoBehaviour
{
    public float setLifeTime;
    private float currentTime;
    void Start()
    {
        currentTime = setLifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }
}
