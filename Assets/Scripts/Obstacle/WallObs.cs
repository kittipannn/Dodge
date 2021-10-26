using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObs : ObstacleBehav
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WallCheck"))
        {
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
