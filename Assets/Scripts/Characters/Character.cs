﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
//    public List<StatusEffect> statusEffects = new List<StatusEffect>();
    
    [SerializeField] protected List<Ability> abilities = new List<Ability>(); 

	[SerializeField] protected List<Status> statuses = new List<Status>();     	// Tandy: List of Status to show what Character is affected by

	protected CombatManager combatManager;

    public enum CombatState
    {
        ABLE, DISABLED, USEABILITY, EXHAUSTED
    }
    public CombatState combatState;
	protected bool _canActThisTurn = true;

    public Animator animator;
    public bool isAnimating;

    public new string name;
    public int speed;

    public uint maxhealth;
    public uint currentHealth;

    public uint currentHeat;
    public uint maxHeat;
    public uint defense;
    public float accuracy = 84.5f;
    public float evade = 10;

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
	}

	public Ability AbilityOne() // Basic Attack						//selectedAbility, UI calls back to Character.UseAbility(targets) which then runs selectedAbility.DoAbility(targets);
    {																//UI NEEDS to go through Character in case Character needs to redirect target info before Ability is used
		if (abilities [0] == null) 
		{
			Debug.Log ("There is no AbilityOne for " + this.name);
			return null;
		}

		if (abilities [0].Usable) 
		{
			combatManager.uiManager.GetTargets (abilities [0].targetType);
		}
		else
		{
			Debug.Log ("AbilityOne is not usable");
			return null;
		}


		if(abilities.Count != 0)
		{
	        if (abilities[0].Usable)
	        {           // if cooldown can start, do rest  of Ability
	            combatState = CombatState.USEABILITY;

                if (!isAnimating)
                {
                    isAnimating = true;
                    animator.SetInteger("animState", 1);
                    print("played animation");
                    isAnimating = false;
                }

                animator.SetInteger("animState", 0);

	            return abilities[0];
	        }
		}
        return null;
    }

	public Ability AbilityTwo()
    {
		if(abilities.Count != 0)
		{
	        if (abilities[1].Usable)
	        {           // if cooldown can start, do rest  of Ability
	            combatState = CombatState.USEABILITY;

                if (!isAnimating)
                {
                    animator.SetInteger("animState", 2);
                    print("played animation");
                }

                animator.SetInteger("animState", 0);

                return abilities[1];
	        }
		}
        return null;
    }

	public Ability AbilityThree()
    {
		if(abilities.Count != 0)
		{
	        if (abilities[2].Usable)
	        {           // if cooldown can start, do rest  of Ability
	            combatState = CombatState.USEABILITY;

                if (!isAnimating)
                {
                    animator.SetInteger("animState", 3);
                    print("played animation");
                }

                animator.SetInteger("animState", 0);

                return abilities[2];
	        }
		}
        return null;
    }

    public void TakeDamage(uint damage = 0, ElementType damageType = ElementType.PHYSICAL)
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
            ApplyWater(damage);
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

    void ApplyWater(uint amount = 0)
    {
        currentHeat -= (uint)Mathf.Clamp((float)amount, 0f, (float)currentHeat);
    }

    public void DealHeatDamage(int heatDamage)
    {
        currentHeat += (uint)Mathf.Clamp(heatDamage, 0, (maxHeat - currentHeat));		//Clamps the amount of heat damage so that it does not go above the maximumn.
        /*if (currentHeat == maxHeat)
        {
            EndTurn();
            //TODO  move this to EndTurn function.  should not be in deal damage Functions currentHeat = maxHeat - 100; //or whatever we settle on the value for 1 bar is
        }*/
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
        Debug.Log(name + " died!");
        combatState = CombatState.EXHAUSTED;
        EndTurn();
    }

    IEnumerator PlayAnimation()
    {
        animator.GetCurrentAnimatorStateInfo(0).IsName("state") == true);
    }
}
