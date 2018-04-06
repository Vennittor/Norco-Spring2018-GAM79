using System.Collections;
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

	public int selectedAbilityIndex = -1;

	[SerializeField] protected List<Ability> abilities = new List<Ability>();
	[SerializeField] protected List<uint> cooldownTimers = new List<uint>();
	[SerializeField] protected List<StatusEffect> statuses = new List<StatusEffect>();     	// Tandy: List of Status to show what Character is affected by

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
			
		cooldownTimers.Clear();									//enfore size of cooldownTimers to abilities and set the timer to 0
		foreach(Ability ability in abilities)
		{
			cooldownTimers.Add(0);
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

			if (this is PlayerCharacter) {

				combatManager.uiManager.UpdateAbilityButtons (abilities);
			}

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

		if (cooldownTimers[abilityIndex] == 0)
		{
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
		}
		else
		{
			Debug.Log ( this.gameObject.name + "'s Ability at index " + selectedAbilityIndex.ToString()+ " is on cooldown");
			return null;
		}

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
		cooldownTimers [selectedAbilityIndex] = abilities [selectedAbilityIndex].Cooldown;

		selectedAbilityIndex = -1;

		combatState = enterNewState;

		EndTurn ();
	}

	public void EndTurn()
	{	Debug.Log (this.gameObject.name + " is ending their turn.");
		if (combatManager.activeCharacter == this)
		{
			ProgressCooldowns ();

			combatManager.NextTurn();
			//TODO uiManager.BlockInput until next PlayerCharacter starts turn
		}
		else
		{
			Debug.LogWarning ("Attempting to run EndTurn() on " + this.gameObject.name + " while they are not the gameManager.activeCharacter. This normally should not be done.");
		}
	}
		
	private void StartCooldown(int i = 0)
	{
		cooldownTimers[i] = abilities[i].Cooldown;
	}

	public void ProgressCooldowns()
	{
		for (int i = 0; i < cooldownTimers.Count; i++) 
		{
			if (cooldownTimers[i] > 0)
			{
				cooldownTimers[i]--;
			}
		}
	}


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

    void ReduceHeat(uint amount = 0)
    {
        currentHeat -= (uint)Mathf.Clamp((float)amount, 0f, (float)currentHeat);
    }

    public void DealHeatDamage(int heatDamage)
    {
        currentHeat += (uint)Mathf.Clamp(heatDamage, 0, (maxHeat - currentHeat));		//Clamps the amount of heat damage so that it does not go above the maximumn.
        if(currentHeat == 100)
        {
            print("my heat is now 100, im a little thirsty");
        }
        else if(currentHeat == 200)
        {
            print("my heat is now 200, i need AC");
        }
        else if(currentHeat == 300)
        {
            print("my heat has reached 300, i am now stunned");
        }
    }

	void DealPoisonDamage(uint poisonDamage)
	{
		//reduce poisonDamage here.
		DealPhysicalDamage (poisonDamage);
		Debug.Log ("Poison Damage is not currently implemented, Physical damage was dealt instead.");
	}

    public void ApplyStatus(StatusEffect status) 
	{ 												// Tandy: added this to work with Ability
		if(statuses.Contains(status) == false) 		// if not already affected by Status
		{ 										
            statuses.Add(status); 					// add Status to List to show it affects Character
        }
    }

    public void Faint()
    {
		Debug.Log(this.gameObject.name + " died!");
        combatState = CombatState.EXHAUSTED;
		if (this as Character == combatManager.activeCharacter)
		{	Debug.Log ("Active Character died");
			EndTurn();
		}

    }

}
