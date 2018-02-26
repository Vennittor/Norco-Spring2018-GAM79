using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public float PlayerHealth;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown("1"))
        {
            Debug.Log("Player used 1-key attack");
        }
        else if (Input.GetKeyDown("2"))
        {
            Debug.Log("Player used 2-key attack");
        }
        if (Input.GetKeyDown("3"))
        {
            Debug.Log("Player used 3-key attack");
        }
    }
    
}
