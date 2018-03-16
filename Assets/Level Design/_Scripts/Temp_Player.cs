using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_Player : MonoBehaviour
{
    private GameObject player;
    public float moveSpeed = 0.0f;
    private Vector2 dir;
    private Rigidbody rB;

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void Start()
    {
        player = this.gameObject;
        player.transform.position = transform.position;
        moveSpeed = 1.0f;
        rB = GetComponent<Rigidbody>();
    }

    void MovePlayer()
    {
        dir = player.transform.position;

        if(Input.GetKey(KeyCode.D))
        {
            moveSpeed += 1.0f;
            dir = Vector2.right;
            rB.AddForce(dir * moveSpeed);
            player.transform.position = Vector2.zero;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveSpeed -= 1.0f;
            dir = Vector2.left;
            rB.AddForce(-dir * moveSpeed);
            player.transform.position = Vector2.zero;
        }

        player.transform.position = transform.position;
    }
}