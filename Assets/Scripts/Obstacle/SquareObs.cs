using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareObs : ObstacleBehav
{
    Vector2 playerPos;
    [SerializeField] float speedRot = 5;
    private void OnEnable()
    {

        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
    }
    private void FixedUpdate()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, playerPos, fallSpaeed * Time.deltaTime);
        this.transform.Rotate(new Vector3(0, 0 , speedRot));
    }
}
