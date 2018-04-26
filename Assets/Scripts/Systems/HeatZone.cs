using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(Collider) )]
public class HeatZone : MonoBehaviour
{
    public Party _party;
    public int myHeatIntensity;

	List<Party> partiesInZone = new List<Party> ();

	void Start()
	{

	}

	void OnDrawGizmos()
	{
		Gizmos.color = new Color(1.0f, 0.36f, 0.016f, 0.5f);

		if (this.GetComponent<BoxCollider>() == true)
		{
			Gizmos.DrawCube (this.transform.position, this.GetComponent<BoxCollider>().bounds.size);
		}
		else if(this.GetComponent<SphereCollider>() == true)
		{
			Gizmos.DrawSphere (this.transform.position, this.GetComponent<SphereCollider>().radius);
		}
	}

    public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			Debug.Log ("character entered a heat zone");

			Party otherParty = other.gameObject.GetComponent<Party>();
            
			if (!partiesInZone.Contains (otherParty))
			{
				otherParty.IncreaseHeatRate((uint)myHeatIntensity);

				partiesInZone.Add (otherParty);
			}

		}
    }

    public void OnTriggerExit(Collider other)
    {
		if (other.gameObject.tag == "Player")
		{
			Debug.Log("character exited a heat zone");

			Party otherParty = other.gameObject.GetComponent<Party>();
            
			if( partiesInZone.Contains(otherParty) )
			{
				otherParty.DecreaseHeatRate((uint)myHeatIntensity);

				partiesInZone.Remove (otherParty);
			}
		}
    }
}


