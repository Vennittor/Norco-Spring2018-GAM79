using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatZone : MonoBehaviour
{
    public Party _party;
    public int myHeatIntensity;

	void OnDrawGizmos()
	{
		Gizmos.color = new Color(1.0f, 0.36f, 0.016f, 0.5f);

		Gizmos.DrawCube (this.transform.position, this.GetComponent<Collider>().bounds.size);
	}

    public void OnTriggerEnter(Collider other)
	{
        //TODO "if" statements are temporary, since the trigger enter was detecting the ground as well
		if (other.gameObject.tag == "Player") {
			Debug.Log ("character entered a heat zone");

			_party = other.gameObject.GetComponent<Party>();
            

			_party.IncreaseHeatRate((uint)myHeatIntensity);
		}
    }

    public void OnTriggerExit(Collider other)
    {
		if (other.gameObject.name == "Player")
		{

			Debug.Log("character exited a heat zone");

			_party = other.gameObject.GetComponent<Party>();
            
            _party.DecreaseHeatRate((uint)myHeatIntensity);
		}
    }
}


