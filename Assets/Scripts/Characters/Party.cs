﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    public LevelManager levelMan;

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
        levelMan = LevelManager.Instance;

        if (type == PartyType.PLAYER)
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


    public void FixedUpdate()
    {
    }

    public void HeatChecker(int myHeatIntensity)
    {
        if (heatState == HeatZone.InHeat)
        {
            foreach (Character _char in partyMembers)
            {
                _char.DealHeatDamage(myHeatIntensity);
            }
        }
        else if (heatState == HeatZone.OutofHeat)
        {
            foreach (Character _char in partyMembers)
            {
                _char.DealHeatDamage(-myHeatIntensity);
            }
        }
        else
        {
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag != "Enemy") //TODO change the way we check for this
        {
            return;
        }
        else if (collision.gameObject.GetComponent<Party>().type == PartyType.ENEMY)
        {
            Debug.Log("mom hes touching me");
            //List<Character> enemies = new List<Character>();
            Party enemyParty = collision.gameObject.GetComponent<Party>();
            collision.gameObject.GetComponent<Collider>().enabled = false;

            levelMan.InitiateCombat(this, enemyParty);
        }
    }
}