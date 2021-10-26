using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehav : MonoBehaviour
{
    
    public float damageToPlayer = 10;
    public float fallSpaeed;
    public GameObject psObs;
    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        this.transform.position += new Vector3(0, -1, 0) * fallSpaeed * Time.deltaTime;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Instantiate(psObs, this.transform.position,Quaternion.identity);
            this.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GamePlay>().onHitObstacle(damageToPlayer);
            Instantiate(psObs, this.transform.position, Quaternion.identity);
            CameraShake.Instance.shakeCamera(5f, 0.1f);
            this.gameObject.SetActive(false);
        }
    }
    
}
