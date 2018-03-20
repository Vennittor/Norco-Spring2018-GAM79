using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
//    public List<StatusEffect> statusEffects = new List<StatusEffect>();
    
    [SerializeField] protected List<Ability> abilities = new List<Ability>(); 

	[SerializeField] protected List<Status> statuses = new List<Status>();     	// Tandy: List of Status to show what Character is affected by


    public enum CombatState
    {
        ABLE, DISABLED, USEABILITY, EXHAUSTED
    }
    public CombatState combatState;
    protected CombatManager combatManager;

    public new string name;
    public int speed;

    public uint maxhealth;
    public uint currentHealth;

    public uint currentHeat;
    public uint maxHeat;
    public uint defense;
    public float accuracy = 84.5f;
    public float evade = 10;

    protected void Start()
    {
        combatManager = CombatManager.Instance;
		combatState = CombatState.ABLE;
    }

	public bool BeginTurn()
	{
		Debug.Log (name + " begins their turn.");

		if (combatState == CombatState.DISABLED || combatState == CombatState.EXHAUSTED)		//Checks if the Character is in a state that they cannot act in, and return true/false if the can/cannot;
		{
			Debug.Log(name + " cannot act this turn");

			combatManager.NextTurn();

			return false;
		}
		else
		{
			return true;
		}
	}

	public void EndTurn()
	{
		if (combatManager.activeCharacter == this)
		{
			combatManager.NextTurn();
		}
	}

    public Ability SkillOne() // Basic Attack
    {
        if (abilities[0].Usable)
        {           // if cooldown can start, do rest  of Ability
            combatState = CombatState.USEABILITY;

            // In Ability
            return abilities[0];
        }
        return null;
    }
    public Ability SkillTwo()
    {
        if (abilities[1].Usable)
        {           // if cooldown can start, do rest  of Ability
            combatState = CombatState.USEABILITY;

            // In Ability
            return abilities[1];
        }
        return null;
    }
    public Ability SkillThree()
    {
        if (abilities[2].Usable)
        {           // if cooldown can start, do rest  of Ability
            combatState = CombatState.USEABILITY;

            // In Ability
            return abilities[2];
        }
        return null;
    }

    public void TakeDamage(uint damage = 0, DamageType damageType = DamageType.PHYSICAL)
    {
		if (damageType == DamageType.PHYSICAL)
		{
			DealPhysicalDamage (damage);
		}
		else if(damageType == DamageType.HEAT)
		{
			DealHeatDamage (damage);
		}
		else if(damageType == DamageType.POISON)
		{
			DealPoisonDamage (damage);
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

    void DealHeatDamage(uint heatDamage = 0)
    {
        currentHeat += (uint)Mathf.Clamp(heatDamage, 0, (maxHeat - currentHeat));		//Clamps the amount of heat damage so that it does not go above the maximumn.
        if (currentHeat == maxHeat)
        {
            EndTurn();
            currentHeat = maxHeat - 100; //or whatever we settle on the value for 1 bar is
        }
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
}
