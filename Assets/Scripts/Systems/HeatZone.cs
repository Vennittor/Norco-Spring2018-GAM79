using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatZone : MonoBehaviour
{
    public Party _party;
    public PlayerCharacter _char;
    public int myHeatIntensity;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("character entered a heat zone");
        _party = other.gameObject.GetComponent<Party>();
        _party.heatState = Party.HeatZone.InHeat;
        _char.SetHeatRate(myHeatIntensity);
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("character exited a heat zone");
        _party = other.gameObject.GetComponent<Party>();
        _char.SetHeatRate(-myHeatIntensity);
        if (_char.heatIntensity == 0)
        {
            _party.heatState = Party.HeatZone.OutofHeat;
        }
    }
}


