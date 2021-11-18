using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObs : ObstacleBehav
{
    [SerializeField] ParticleSystem psWall;
    private void OnEnable()
    {
        psWall = GameObject.FindGameObjectWithTag("Pswall").GetComponent<ParticleSystem>();
    }
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
            SoundManager.SoundInstance.Play("Hit");
            if (GameSetting.gamesettingInstance.vibrate)
            {
                Handheld.Vibrate();
            }
        }
        if (collision.gameObject.CompareTag("PsCheck"))
        {
            psWall.transform.position = new Vector2(this.transform.position.x,psWall.transform.position.y);
            psWall.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        psWall.Stop();
    }
}
