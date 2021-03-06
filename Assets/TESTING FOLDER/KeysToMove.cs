﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class KeysToMove : MonoBehaviour
{
	public Animator myAnimator;

    NavMeshAgent agent;
    //Set the agent's angular speed in the inspector to zero
	public float walkingSpeed = 12;

//    private bool walking;
    //private bool facingRight = true;
    public GameObject interactionPrefab;

	public bool movementAllowed = true;


    private void Start()
    {
        //Gemme dat booty
        agent = GetComponent<NavMeshAgent>();
        //This causes the agent to not rotate whatsoever when moving
        agent.angularSpeed = 0;

        //TEST
        LevelManager levMan = LevelManager.Instance;
        levMan.playerParty = this.gameObject;

		if (myAnimator == null) 
		{
			myAnimator = this.gameObject.GetComponent<Animator> ();
		}

		agent.speed = walkingSpeed;
    }

    void Update ()
    {
		if (movementAllowed)
        {
            Vector3 input = Vector3.right;

			if (input != Vector3.zero) 
			{
				//walking = true;
				if (myAnimator != null) 
				{
					myAnimator.SetBool ("Walk", true);
				}

			}
			else 
			{
				//walking = false;
				if (myAnimator != null) 
				{
					myAnimator.SetBool("Walk", false);
				}

			}
            agent.destination = transform.position + input;
            //So if the agent is given a destination that is too far away it will make a path to it, as it should
            //However, an exception needs to be made if the player is holding right against a ledge with a walkable area
            //on the other side. So we check if the distance to this point THROUGH THE NAVMESHAGENT'S PATHING CODE to see
            //if the agent should move to it. (The value is 1.5 to account for the distance of ~1.3 for diagonal movement
            if (agent.remainingDistance > 1.5)
            {
                agent.destination = transform.position;
            }

            //if (Input.GetKeyDown(KeyCode.Z))
            //{
            //    Instantiate(interactionPrefab, transform.position + Vector3.right * (facingRight ? 1 : -1), Quaternion.identity);
            //}

            if (agent.velocity.x > 0)
            {
                //facingRight = true;
				this.gameObject.GetComponent<SpriteRenderer> ().flipX = false;
            }
            else if (agent.velocity.x < 0)
            {
                //facingRight = false;
				this.gameObject.GetComponent<SpriteRenderer> ().flipX = true;
            }
        }
	}
}
