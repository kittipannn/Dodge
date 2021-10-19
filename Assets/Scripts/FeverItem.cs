using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverItem : MonoBehaviour
{
    GamePlay gamePlay;
    float feverValue = 5;
    public float fallSpaeed;
    private void OnEnable()
    {
        gamePlay = GameObject.FindObjectOfType<GamePlay>();
    }

    private void FixedUpdate()
    {
        this.transform.position += new Vector3(0, -1, 0) * fallSpaeed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gamePlay.addFeverGauge(feverValue);
            this.gameObject.SetActive(false);
            SoundManager.SoundInstance.Play("Fever");

        }
        if (collision.gameObject.CompareTag("Floor"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
