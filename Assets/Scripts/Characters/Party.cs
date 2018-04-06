using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    public LevelManager levelMan;

    public HeatZone heatState;
    public List<Character> partyMembers;
    public PartyType type = PartyType.ENEMY;

    
    float heatInterval = 2;
    float NextTickAt = 0;
    public uint myCurrentHeatIntensity;

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


    public void Update()
    {
        if (levelMan.combatManager.inCombat == false)
        {
            if (NextTickAt <= Time.time)
            {
                if (heatState == HeatZone.InHeat)
                {
                    LevelHeatApplication();
                    NextTickAt = Time.time + heatInterval;
                }
            }
        }
    }

    public void IncreaseHeatRate(uint heatZoneIntensity)
    {
        myCurrentHeatIntensity += heatZoneIntensity;
        if (myCurrentHeatIntensity > 0)
        {
            heatState = HeatZone.InHeat;
        }
    }

    public void DecreaseHeatRate(uint heatZoneIntensity)
    {
        myCurrentHeatIntensity -= heatZoneIntensity;
        if (myCurrentHeatIntensity == 0)
        {
            heatState = HeatZone.OutofHeat;
        }
    }

    public void LevelHeatApplication()
    {
        if (heatState == HeatZone.InHeat)
        {
            foreach (Character _char in partyMembers)
            {
                _char.ApplyDamage(myCurrentHeatIntensity, ElementType.HEAT);
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