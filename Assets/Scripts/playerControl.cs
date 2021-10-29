using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D rigidbody2D;
    SoundManager sound;
    bool hit = false;
    private int soundIndex = 0;
    private void Awake()
    {
        rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        sound = SoundManager.SoundInstance;

    }
    void Update()
    {
        changeDirectionPlayer();

    }
    private void FixedUpdate()
    {
        //this.transform.position += new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
        rigidbody2D.velocity = new Vector3(1, 0, 0) * moveSpeed ;
    }
    void changeDirectionPlayer() 
    {
        if (Input.GetMouseButtonDown(0) && !hit)
        {
             moveSpeed = moveSpeed * (-1);
             //soundMove();
        }

    }
    void soundMove() 
    {
        string[] Nsound = { "Touch1", "Touch2" };
        string Psound;
        if (soundIndex == 0)
        {
             Psound = Nsound[0];
            soundIndex++;
        }
        else
        {
            Psound = Nsound[1];
            soundIndex = 0;
        }
        sound.Play(Psound);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border") && !hit)
        {
            hit = true;
            moveSpeed = moveSpeed * (-1);
            
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            hit = false;
        }
    }



}
