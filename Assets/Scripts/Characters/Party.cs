﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    public HeatZone heatState;
    public List<Character> partyMembers;
    public PartyType type = PartyType.ENEMY;

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
        if(type == PartyType.PLAYER)
        {
            for (int i = partyMembers.Count-1; i > 0; i--)
            {
                PlayerCharacter charType = partyMembers[i].GetComponent<PlayerCharacter>();
                if (charType == null)
                {
                    partyMembers.RemoveAt(i);
                }
            }
        }
        else
        {
            for (int i = partyMembers.Count - 1; i > 0; i--)
            {
                EnemyCharacter charType = partyMembers[i].GetComponent<EnemyCharacter>();
                if (charType == null)
                {
                    partyMembers.RemoveAt(i);
                }
            }
        }
    }


    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Enemy") //TODO change the way we check for this
        {
            return;
        }
        else if (collision.gameObject.GetComponent<Party>().type == PartyType.ENEMY)
        {
            List<Character> enemies = new List<Character>();
            Party otherParty = collision.gameObject.GetComponent<Party>();
            enemies.AddRange(otherParty.partyMembers);

            Debug.Log("mom hes touching me");
        }
    }
}