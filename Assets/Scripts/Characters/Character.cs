﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{   
	protected CombatManager combatManager;

    public enum CombatState
    {
        ABLE, DISABLED, USINGABILITY, EXHAUSTED
    }

    public string characterName;
	public Animator animator;

	public uint maxhealth;
	public uint currentHealth;

	public uint maxHeat;
	public uint currentHeat;

	public float accuracy = 84.5f;
	public float evade = 10;

    public int speed;
    public uint defense;

	public CombatState combatState;

	[SerializeField] protected List<Ability> abilities = new List<Ability>(); 
	[SerializeField] protected List<Status> statuses = new List<Status>();     	// Tandy: List of Status to show what Character is affected by

	protected bool _canActThisTurn = true;

	public bool canAct
	{
		get
		{
			return _canActThisTurn;
		}
	}

    protected void Start()
    {
        combatManager = CombatManager.Instance;
		combatState = CombatState.ABLE;

		if (animator == null) 
		{
			animator = GetComponent<Animator> ();
		}

		foreach(Ability ability in abilities)
		{
			ability.EquipAbility(this);
		}
    }

	public virtual void BeginTurn()
	{
		Debug.Log (gameObject.name + " begins their turn.");

		if (combatState == CombatState.DISABLED || combatState == CombatState.EXHAUSTED)		//Checks if the Character is in a state that they cannot act in, and return true/false if the can/cannot;
		{
			Debug.Log(gameObject.name + " cannot act this turn");

			EndTurn ();

			_canActThisTurn = false;
		}
		else
		{
			_canActThisTurn = true;

			ChooseAbility ();
		}
	}

	protected abstract void ChooseAbility();

	public abstract void AbilityComplete(CombatState newState = CombatState.ABLE);

	public void EndTurn()
	{
		if (combatManager.activeCharacter == this)
		{
			combatManager.NextTurn();
		}
		else
		{
			Debug.LogWarning ("Attempting to run EndTurn() on " + this.gameObject.name + " while they are not the gameManager.activeCharacter. This normally should not be done.");
		}
	}

	public Ability ReadyAbilityOne() // Basic Attack						//selectedAbility, UI calls back to Character.UseAbility(targets) which then runs selectedAbility.DoAbility(targets);
    {																//UI NEEDS to go through Character in case Character needs to redirect target info before Ability is used
		if (abilities [0] == null) 
		{
			Debug.Log ("There is no AbilityOne for " + this.characterName);
			return null;
		}

		if (abilities [0].Usable) 
		{
			//combatManager.uiManager.GetTargets (abilities [0].targetType);

			if (animator != null)
			{
				animator.SetBool ("Ready", true);
				animator.SetBool ("Idle", false);
			}
			else
			{
				Debug.LogWarning (this.gameObject.name + "Is trying to call it's animator in AbilityOne(), and does not have reference to it");
			}

			return abilities [0];
		}
		else
		{
			Debug.Log ("AbilityOne is not usable");
			return null;
		}
			
    }

	public Ability ReadyAbilityTwo()
    {
		//TODO TEST empties reference within abilities[1] so that it will return null when searched for

		if (abilities.Count > 1) 
		{
			if (abilities [1] != null) 
			{
				abilities[1] = null;
			}
		}
		else
		{
			abilities [1] = null;
		}

		return abilities [1];
    }

	public Ability ReadyAbilityThree()
    {
		//TODO TEST Currently Empty for Testing Reasons
		if (abilities.Count > 2) 
		{
			abilities.RemoveRange (2, abilities.Count-2);
		}
        return null;
    }

    public void ApplyDamage(uint damage = 0, ElementType damageType = ElementType.PHYSICAL)
    {
        if (damageType == ElementType.PHYSICAL)
        {
            DealPhysicalDamage(damage);
        }
        else if (damageType == ElementType.HEAT)
        {
            DealHeatDamage((int)damage);
        }
        else if (damageType == ElementType.HEALING)
        {
            Heal(damage);
        }
        else if (damageType == ElementType.WATER)
        {
			ReduceHeat(damage);
        }
        else if (damageType == ElementType.POISON)
        {
            DealPoisonDamage(damage);
        }
    }

	void DealPhysicalDamage(uint physicalDamage = 0)
	{
		physicalDamage -= defense;
		if (physicalDamage >= 1)
		{
			currentHealth -= (uint)Mathf.Clamp(physicalDamage, 0, currentHealth);
			if (currentHealth == 0)
			{
				Faint();
			}
		}
	}

	void Heal(uint healing = 0)
	{
		currentHealth = (currentHealth + healing) > maxhealth ? maxhealth : (currentHealth + healing);
	}

    void ReduceHeat(uint amount = 0)
    {
        currentHeat -= (uint)Mathf.Clamp((float)amount, 0f, (float)currentHeat);
    }

    public void DealHeatDamage(int heatDamage)
    {
        currentHeat += (uint)Mathf.Clamp(heatDamage, 0, (maxHeat - currentHeat));		//Clamps the amount of heat damage so that it does not go above the maximumn.
    }

	void DealPoisonDamage(uint poisonDamage)
	{
		//reduce poisonDamage here.
		DealPhysicalDamage (poisonDamage);
		Debug.Log ("Poison Damage is not currently implemented, Physical damage was dealt instead.");
	}

    public void ApplyStatus(Status status) 
	{ 												// Tandy: added this to work with Ability
		if(statuses.Contains(status) == false) 		// if not already affected by Status
		{ 										
            statuses.Add(status); 					// add Status to List to show it affects Character
        }
    }

    public void Faint()
    {
		Debug.Log(characterName + " died!");
        combatState = CombatState.EXHAUSTED;
        EndTurn();
    }

//    IEnumerator PlayAnimation()
//    {
//        animator.GetCurrentAnimatorStateInfo(0).IsName("state") == true;
//    }
}
