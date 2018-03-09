using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatZone : MonoBehaviour
{
    public PlayerCharacter _char;
    public int myHeatIntensity;

	public void OnTriggerEnter(Collider other)
    {
        Debug.Log("yo i gotS it");
        _char = other.gameObject.GetComponent<PlayerCharacter>();
        _char.heatState = PlayerCharacter.HeatZone.InHeat;
        _char.SetHeatRate(myHeatIntensity);
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("yo i dont not gots it");
        _char = other.gameObject.GetComponent<PlayerCharacter>();
        _char.SetHeatRate(-myHeatIntensity);
        if (_char.heatIntensity == 0)
        {
            _char.heatState = PlayerCharacter.HeatZone.OutofHeat;
        }
    }
}
