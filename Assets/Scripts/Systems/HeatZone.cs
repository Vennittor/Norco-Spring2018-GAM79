using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatZone : MonoBehaviour
{
    public Party _party;
    public int myHeatIntensity;

    public void OnTriggerEnter(Collider other)
    {
        //TODO "if" statements are temporary, since the trigger enter was detecting the ground as well
        if (other.gameObject.name == "Player")
        {
            Debug.Log("character entered a heat zone");
            _party = other.gameObject.GetComponent<Party>();
            _party.heatState = Party.HeatZone.InHeat;
            foreach (PlayerCharacter _char in _party.partyMembers)
            {
                _char.SetHeatRate(myHeatIntensity);
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("character exited a heat zone");
            _party = other.gameObject.GetComponent<Party>();
            foreach (PlayerCharacter _char in _party.partyMembers)
            {
                _char.SetHeatRate(-myHeatIntensity);
            }
            _party.heatState = Party.HeatZone.OutofHeat;
        }
    }
}