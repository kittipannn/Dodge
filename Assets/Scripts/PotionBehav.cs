using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBehav : MonoBehaviour
{
    float increaseHealth = 50;
    public float fallSpaeed;
    public ParticleSystem psObj;
    public ParticleSystem psObjHitPlayer;
    ParticleSystem heal;
    private void Start()
    {
        heal = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<ParticleSystem>();
        psObj.startColor = this.GetComponent<SpriteRenderer>().color;
        psObjHitPlayer.startColor = this.GetComponent<SpriteRenderer>().color;
    }
    private void FixedUpdate()
    {
        this.transform.position += new Vector3(0, -1, 0) * fallSpaeed * Time.deltaTime;
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            this.gameObject.SetActive(false);
            Instantiate(psObj, this.transform.position, Quaternion.identity);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GamePlay>().addHP(increaseHealth);
            this.gameObject.SetActive(false);
            SoundManager.SoundInstance.Play("Potion");
            heal.Play();
            Instantiate(psObjHitPlayer, this.transform.position, Quaternion.identity);
        }
    }
}
