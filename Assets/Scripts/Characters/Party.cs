﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    public HeatZone heatState;
    public List<Character> partyMembers;
    public PartyType type = PartyType.ENEMY;
    
    float heatInterval = 2;
    float NextTickAt = 0;
    public uint myCurrentHeatIntensity;
    public uint lastHeat;

    public Party playerParty { get; private set; }

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
		for (int i = partyMembers.Count - 1; i >= 0; i--) 
		{
			if (partyMembers [i] == null)
			{
				partyMembers.RemoveAt(i);
			}
		}

		if (partyMembers.Count == 0)
		{
			Debug.LogError ("The " + this.gameObject.name + " has no party Memebers!");
		}
		else 
		{
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
			foreach (Character character in partyMembers)
			{
				if (character.GetComponent<SpriteRenderer> () != null) 
				{
					character.GetComponent<SpriteRenderer>().enabled = false;
				}
				if (character.GetComponent<MeshRenderer> () != null) 
				{
					character.GetComponent<MeshRenderer>().enabled = false;
				}

				character.GetComponent<Collider>().enabled = false;
			}
		}

    }


    public void Update()
    {
		if (lastHeat != myCurrentHeatIntensity)
		{
			LevelManager.Instance.GetHeat(myCurrentHeatIntensity);
			lastHeat = myCurrentHeatIntensity;
		}
				
		if (CombatManager.Instance.inCombat == false)
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
		myCurrentHeatIntensity = (uint)Mathf.Clamp (myCurrentHeatIntensity, 0, myCurrentHeatIntensity);
        if (myCurrentHeatIntensity <= 0)
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
		if (this.type == PartyType.PLAYER)
		{
			if (collision.gameObject.tag != "Enemy") //TODO change the way we check for this
			{
				return;
			}
			else if (collision.gameObject.GetComponent<Party>().type == PartyType.ENEMY)
			{
				//List<Character> enemies = new List<Character>();
				Party enemyParty = collision.gameObject.GetComponent<Party>();
				collision.gameObject.GetComponent<Collider>().enabled = false;

				LevelManager.Instance.StartCoInitiateCombat (this, enemyParty);
			}
		}

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Exit")
        {
			NewTransitionManager.Instance.RunCoOut ();
			LevelManager.Instance.StartCoLoadSceneAsync();
        }

        if (col.gameObject.tag == "ExitToMainMenu")
        {
            Debug.Log("Loading Back to Main Menu");
            NewTransitionManager.Instance.StartCoroutine(NewTransitionManager.Instance.Out());
			LevelManager.Instance.LoadScene(1);
            NewTransitionManager.Instance.StopCoroutine(NewTransitionManager.Instance.In());
            NewTransitionManager.Instance.transitionImage.enabled = false;
            NewTransitionManager.Instance.iAnim.enabled = false;
            Destroy(NewTransitionManager.Instance.gameObject);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Exit")
        {
			NewTransitionManager.Instance.StartCoroutine(NewTransitionManager.Instance.Nuetral());
			NewTransitionManager.Instance.exitTransform.gameObject.SetActive(false);
        }

        if (col.gameObject.tag == "Entrance")
        {
			NewTransitionManager.Instance.StartCoroutine(NewTransitionManager.Instance.Nuetral());
			NewTransitionManager.Instance.entranceTransform.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Exit")
        {
			NewTransitionManager.Instance.StartCoroutine(NewTransitionManager.Instance.In());
			LevelManager.Instance.StartCoDoneWithTransition( playerParty);
        }

        if (col.gameObject.tag == "Entrance")
        {
			NewTransitionManager.Instance.StopCoroutine(NewTransitionManager.Instance.In());
			NewTransitionManager.Instance.transitionImage.enabled = false;
        }
    }
}