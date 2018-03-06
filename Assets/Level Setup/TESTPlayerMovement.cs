using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTPlayerMovement : MonoBehaviour 
{
	public float speedForward = 2.0f;
	public float speedBackward = 2.0f;
	public float speedUp = 1.0f;
	public float speedDown = 1.0f;


	void Start () 
	{
		
	}

	void Update () 
	{
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) 
		{
			transform.Translate (Vector3.forward * speedUp * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) 
		{
			transform.Translate (Vector3.back * speedDown * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) 
		{
			transform.Translate (Vector3.right * speedForward * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) 
		{
			transform.Translate (Vector3.left * speedBackward * Time.deltaTime);
		}

	}
		
}

