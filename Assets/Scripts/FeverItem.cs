using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverItem : MonoBehaviour
{
    GamePlay gamePlay;
    float feverValue = 5;
    public float fallSpaeed;
    public ParticleSystem psObj;
    public ParticleSystem psObjHitPlayer;
    ParticleSystem fever;
    private void OnEnable()
    {
        gamePlay = GameObject.FindObjectOfType<GamePlay>();
    }
    private void Start()
    {
        fever = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetComponent<ParticleSystem>();
        psObj.startColor = this.GetComponent<SpriteRenderer>().color;
        psObjHitPlayer.startColor = this.GetComponent<SpriteRenderer>().color;
    }

    private void FixedUpdate()
    {
        this.transform.position += new Vector3(0, -1, 0) * fallSpaeed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gamePlay.addFeverGauge(feverValue);
            this.gameObject.SetActive(false);
            SoundManager.SoundInstance.Play("Fever");
            fever.Play();
            Instantiate(psObjHitPlayer, this.transform.position, Quaternion.identity);
        }
        if (collision.gameObject.CompareTag("Floor"))
        {
            this.gameObject.SetActive(false);
            Instantiate(psObj, this.transform.position, Quaternion.identity);

        }
    }
    
}
