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
    [System.Serializable]
    public class EffectClass
    {
        public StatusEffectType statusEffectType;
        public bool isBuff;
        public int duration;
        public bool applyImmediately;
        public bool checkAtStart;

        public EffectClass(StatusEffectType statusType, bool isBuff, int duration, bool applyNow, bool checkStart)
        {
            statusEffectType = statusType;
            this.isBuff = isBuff;
            this.duration = duration;
            applyImmediately = applyNow;
            checkAtStart = checkStart;
        }

        public void DecDuration()
        {
            duration--;
        }
    }
    public List<EffectClass> effectClassList;
    public List<StatusEffectType> statuses;
    public List<EffectClass> removeStatus;

    public string characterName;
	public Animator animator;
    protected StatusEffect statusEffect;

	public uint maxhealth;
	public uint currentHealth;

	public uint maxHeat;
	public uint currentHeat;

	[SerializeField] protected List<Ability> abilities;

    public uint attack;
    public float attackBonus;
    public float attackMod;
    public float accuracy;
    public float accuracyBonus;
    public float accuracyMod;
	public float evade;
    public float evadeBonus;
    public float evadeMod;
    public uint speed;
    public float speedBonus;
    public float speedMod;
    public uint defense;
    public float defenseBonus;
    public float defenseMod;

	public CombatState combatState;

	public int selectedAbilityIndex;


	//[SerializeField] protected List<uint> cooldownTimers;

	protected bool _canActThisTurn = true;


	public bool canAct
	{
		get
		{
			return _canActThisTurn;
		}
	}

	public int abilityCount
	{
		get{ return abilities.Count; }
	}

    protected void Start()
    {
        statusEffect = new StatusEffect();
        combatManager = CombatManager.Instance;
		combatState = CombatState.ABLE;
        effectClassList = new List<EffectClass>();
        statuses = new List<StatusEffectType>();
        removeStatus = new List<EffectClass>();
		if (animator == null) 
		{
			animator = GetComponent<Animator> ();
		}

      //  cooldownTimers = new List<uint>();									//enfore size of cooldownTimers to abilities and set the timer to 0
		/*foreach(Ability ability in abilities)
		{
			cooldownTimers.Add(0);
		}*/
        accuracy = 84.5f;
        evade = 10;
        selectedAbilityIndex = -1;
    }

	public virtual void BeginTurn()
	{
		Announcer.BeginTurn (this.characterName);

        if (statuses.Contains(StatusEffectType.Stun))
        {
            Debug.Log("Boi got knocked in the head");
            this.combatState = CombatState.DISABLED;
        }
		if (effectClassList.Count > 0)
		{
			foreach (EffectClass status in effectClassList)
			{
				if (status.checkAtStart == true)
				{
                    if (!status.isBuff)
                    {
                        statusEffect.Apply(this, status.statusEffectType);
                    }
                    else if (status.isBuff && !statuses.Contains(status.statusEffectType))
                    {
                        statusEffect.Apply(this, status.statusEffectType);
                    }
                }
			}
		}


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

	protected abstract void ChooseAbility();					//This should be used by an inheriting class based on how it will decided upon the Abilities it will use.  via UI input, AI, or some other method

	public abstract void GetNewTargets ();					//this is should call ReadyAbility again using the selectedAbilityIndex to go back up to the UI or AI to get another set of targets.  Needs to be called before AbilityHasCompleted

	public Ability ReadyAbility(int abilityIndex = 0) 			//Takes in an index number to reference that index in the abilities list, and will return that Ability if one is found and Usable
	{
		if (abilities [abilityIndex] == null || abilities.Count <= abilityIndex) 
		{
			Debug.Log ("There is no AbilityOne for " + this.characterName);
			return null;
		}

		/*if (cooldownTimers[abilityIndex] == 0)
		{*/
			if (animator != null)
			{
				animator.SetBool ("Ready", true);
				animator.SetBool ("Idle", false);
			}
			else
			{
				Debug.LogWarning (this.gameObject.name + " is trying to call it's animator in AbilityOne(), and does not have reference to it");
			}

			selectedAbilityIndex = abilityIndex;
			abilities [selectedAbilityIndex].PrepAbility (this as Character);		
			abilities [selectedAbilityIndex].StartAbility ();

			return abilities [selectedAbilityIndex];
		/*}
		else
		{
			Debug.Log ( this.gameObject.name + "'s Ability at index " + selectedAbilityIndex.ToString()+ " is on cooldown");
			return null;
		}*/

	}

	public void UseAbility(List<Character> targets)
	{
		if (selectedAbilityIndex < 0 || selectedAbilityIndex >= abilities.Count) 
		{
			Debug.LogError (this.gameObject.name + "'s UseAbility(): No Ability was Selected, or an Ability was selected that the Character does not have. selectedAbilityIndex is out of abilities range");
		}
		else 
		{
			if (targets.Count == 0) 
			{
				Debug.LogWarning ("No Targets were passed to UseAbility");
			}
			Debug.Log (this.gameObject.name + " is using " + abilities [selectedAbilityIndex].abilityName);
			abilities [selectedAbilityIndex].SetTargets (targets);
			abilities [selectedAbilityIndex].UseAbility ();
		}
	}

	public void AbilityHasCompleted(CombatState enterNewState = CombatState.ABLE)
	{	Debug.Log (this.gameObject.name + "'s ability has completed.");
		//cooldownTimers [selectedAbilityIndex] = abilities [selectedAbilityIndex].Cooldown;

		selectedAbilityIndex = -1;

		combatState = enterNewState;

		EndTurn ();
	}

	public void EndTurn()
	{	Debug.Log (this.gameObject.name + " is ending their turn.");
		if (combatManager.activeCharacter == this)
		{
            foreach (EffectClass status in effectClassList)
            {
                if (status.checkAtStart == false)
                {
                    if (!status.isBuff)
                    {
                        statusEffect.Apply(this, status.statusEffectType);
                    }
                    else if (status.isBuff && !statuses.Contains(status.statusEffectType))
                    {
                        statusEffect.Apply(this, status.statusEffectType);
                    }
                }

                if (status.statusEffectType == StatusEffectType.Smoke)
                {
                    if (statusEffect.smokeTurnCounter != combatManager.roundCounter)
                    {
                        if (status.duration != -1)
                        {
                            status.DecDuration();
                        }
                        if (status.duration == 0)
                        {
                            removeStatus.Add(status);
                        }
                    }
                    else
                    {
                        Debug.Log("Wait one more turn");
                    }
                }
                else
                {
                    if (status.duration != -1)
                    {
                        status.DecDuration();
                    }
                    if (status.duration == 0)
                    {
                        removeStatus.Add(status);
                    }
                }
            }
            RemoveStatuses();

           // ProgressCooldowns ();

			combatManager.NextTurn();
			//TODO uiManager.BlockInput until next PlayerCharacter starts turn
		}
		else
		{
			Debug.LogWarning ("Attempting to run EndTurn() on " + this.gameObject.name + " while they are not the gameManager.activeCharacter. This normally should not be done.");
		}
	}
    public void RemoveStatuses()
    {
        foreach (EffectClass status in removeStatus)
        {
            if (status.statusEffectType == StatusEffectType.Stun)
            {
                combatState = CombatState.ABLE;
            }
            if (status.isBuff)
            {
                statusEffect.RemoveStatus(this, status.statusEffectType);
            }
            effectClassList.Remove(status);
            statuses.Remove(status.statusEffectType);
        }
        removeStatus.Clear();
    }
		
	/*private void StartCooldown(int i = 0)
	{
		cooldownTimers[i] = abilities[i].Cooldown;
	}*/

	/*public void ProgressCooldowns()
	{
		for (int i = 0; i < cooldownTimers.Count; i++) 
		{
			if (cooldownTimers[i] > 0)
			{
				cooldownTimers[i]--;
			}
		}
	}*/


    public void ApplyDamage(uint damage = 0, ElementType damageType = ElementType.PHYSICAL)
	{
        Debug.Log (this.gameObject.name + " takes " + damage.ToString () + " of " + damageType.ToString() + " damage!");
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
			if (currentHealth <= 0)
			{
				Faint();
			}
		}
	}

	void Heal(uint healing = 0)
	{
		currentHealth = (currentHealth + healing) > maxhealth ? maxhealth : (currentHealth + healing);
	}

    public void ReduceHeat(uint amount = 0)
    {
        currentHeat -= (uint)Mathf.Clamp(amount, 0, currentHeat);
    }

    public void DealHeatDamage(int heatDamage)
    {
        Debug.Log("heat +");
        currentHeat += (uint)Mathf.Clamp(heatDamage, 0, (maxHeat - currentHeat));		//Clamps the amount of heat damage so that it does not go above the maximumn.
        Debug.Log(name + " current heat is " + currentHeat);
        CheckHeatThreshold();
    }

    public void CheckHeatThreshold()
    {
        if (currentHeat >= 100)
        {
            print("my heat is now 100, im a little thirsty");
            effectClassList.Add(statusEffect.AddStatus(StatusEffectType.Lethargy));
        }
        else if (currentHeat >= 200)
        {
            print("my heat is now 200, i need AC");
            effectClassList.Add(statusEffect.AddStatus(StatusEffectType.Berserk));
        }
        else if (currentHeat == 300)
        {
            print("my heat has reached 300, i am now stunned"); //TODO clean up here
            effectClassList.Add(statusEffect.AddStatus(StatusEffectType.Stun));
            //combatManager.activeCharacter.EndTurn();
            combatManager.activeCharacter.currentHeat = 200;
        }
        else
        {
            //return;
        }
    }

	void DealPoisonDamage(uint poisonDamage)
	{
		//reduce poisonDamage here.
		DealPhysicalDamage (poisonDamage);
		Debug.Log ("Poison Damage is not currently implemented, Physical damage was dealt instead.");
	}

    void DealBleedDamage(uint bleedDamage)
    {
        //reduce poisonDamage here.
        DealPhysicalDamage(bleedDamage);
        Debug.Log("Bleed Damage is not currently implemented, Physical damage was dealt instead.");
    }

    public void ApplyStatus(StatusEffectType status)
    {                                               // Tandy: added this to work with Ability
        if (!statuses.Contains(status))         // if not already affected by Status
        {
            EffectClass statusClass = statusEffect.AddStatus(status);
            effectClassList.Add(statusClass); 					// add Status to List to show it affects Character
            statuses.Add(status);
            if (statusClass.applyImmediately)
            {
                statusEffect.Apply(this, status);
            }
        }
    }

    public void Faint()
    {
		Debug.Log(this.gameObject.name + " died!");
        combatState = CombatState.EXHAUSTED;

		if (this is EnemyCharacter)
		{
			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			this.gameObject.GetComponent<Collider> ().enabled = false;
		}
		else 
		{
			//Set Animation state to Dead
			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			this.gameObject.GetComponent<Collider> ().enabled = false;
		}

		if (this as Character == combatManager.activeCharacter)
		{	Debug.Log ("Active Character died");
			EndTurn();
		}

    }

}
