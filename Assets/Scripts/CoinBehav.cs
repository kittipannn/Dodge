using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehav : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            this.gameObject.SetActive(false);
        }
    }
    
}
