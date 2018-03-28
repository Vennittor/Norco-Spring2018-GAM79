using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_movement : MonoBehaviour
{
    // simple movement system + interaction; 
    public float playerSpeed = 2.0f;
    public GameObject player;
    private Vector3 pos;
    private bool hasInteracted = false;
    private bool isInBattle = false;
    private Rigidbody rB;
    
    private void Start()
    {
        DontDestroyOnLoad(this);
        player = this.gameObject;
        pos = this.transform.position;
        hasInteracted = false;
        isInBattle = false;
        rB = GetComponent<Rigidbody>();
    }

    void Update() // temp movement
    {
        direction();

        if(Input.GetKey(KeyCode.D))
        {
            isInBattle = false;
            Proceed();
            rB.AddForce(Vector3.right * playerSpeed * Time.fixedDeltaTime);
        }
        if(Input.GetKey(KeyCode.A))
        {
            isInBattle = false;
            rB.AddForce(Vector3.left * playerSpeed * Time.fixedDeltaTime);
        }

        if(Input.GetKey(KeyCode.W))
        {
            Proceed();
            rB.AddForce(Vector3.forward * playerSpeed * Time.fixedDeltaTime);
            rB.freezeRotation = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rB.AddForce(Vector3.back * playerSpeed * Time.fixedDeltaTime);
            rB.freezeRotation = true;
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