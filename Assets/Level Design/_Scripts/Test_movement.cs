using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_movement : MonoBehaviour
{
    // simple movement system + interaction; 
    private float playerSpeed = 2.0f;
    public GameObject player;
    private Vector3 pos;
    private bool hasInteracted = false;
    private bool isInBattle = false;
    private Rigidbody rB;
    
    private void Start()
    {
        player = this.gameObject;
        pos = this.transform.position;
        hasInteracted = false;
        isInBattle = false;
        rB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        direction();

        if(Input.GetKey(KeyCode.D))
        {
            isInBattle = false;
            Proceed();
            rB.AddForce(Vector3.right);
        }
        if(Input.GetKey(KeyCode.A))
        {
            isInBattle = false;
            Proceed();
            rB.AddForce(Vector3.left);
        }

        
    }

    private Vector3 direction()
    {
        isInBattle = false;
        if(isInBattle == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("canMove");
            }
            Proceed();
        }
        else
        {
            return pos;
        }

        float x = transform.position.x * playerSpeed * Time.deltaTime;
        return pos;
    }

    private void Proceed()
    {
        hasInteracted = true;
        if(hasInteracted)
        {
            float z = transform.position.z * playerSpeed * Time.deltaTime;

        }
    }
}