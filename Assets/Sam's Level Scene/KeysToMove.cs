using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class KeysToMove : MonoBehaviour
{
    NavMeshAgent agent;
    //Set the agent's angular speed in the inspector to zero
    public bool walking;
    public bool facingRight = true;
    public GameObject interactionPrefab;

    private void Start()
    {
        //Gemme dat booty
        agent = GetComponent<NavMeshAgent>();
        //This causes the agent to not rotate whatsoever when moving
        agent.angularSpeed = 0;
    }

    void Update ()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        agent.destination = transform.position + input;
        //So if the agent is given a destination that is too far away it will make a path to it, as it should
        //However, an exception needs to be made if the player is holding right against a ledge with a walkable area
        //on the other side. So we check if the distance to this point THROUGH THE NAVMESHAGENT'S PATHING CODE to see
        //if the agent should move to it. (The value is 1.5 to account for the distance of ~1.3 for diagonal movement
        if(agent.remainingDistance > 1.5)
        {
            agent.destination = transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(interactionPrefab, transform.position + Vector3.right * (facingRight ? 1 : -1), Quaternion.identity);
        }

        if(agent.velocity != Vector3.zero)
        {
            print("im walkin");
        }

        if (agent.velocity.x > 0)
        {
            print("right");
            facingRight = true;
        }
        else if(agent.velocity.x < 0)
        {
            print("left");
            facingRight = false;
        }
	}
}
