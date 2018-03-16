using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    public HeatZone heatState;
    public List<Character> partyMembers;

    public enum PartyType
    {
        PLAYER,
        ENEMY
    }
    public enum HeatZone
    {
        OutofHeat,
        InHeat
    }

    void Start()
    {

    }


    void Update()
    {

    }

}
