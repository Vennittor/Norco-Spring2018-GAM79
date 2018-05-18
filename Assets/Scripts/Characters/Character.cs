using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Animator))]
public abstract class Character : MonoBehaviour
{   
	protected CombatManager combatManager;
    public UIManager uiManager;

    public enum CombatState
    {
        ABLE, DISABLED, USINGABILITY, EXHAUSTED
    }
		
    public string characterName;
	private Animator _animator; 

	[SerializeField] protected CharacterBaseStats baseStats = null;
	private Image _currentQueueImage = null;

	public uint maxhealth;
	public uint currentHealth;

	public uint maxHeat;
	public uint currentHeat;

	[SerializeField] protected List<Ability> abilities;

	public uint attack;				//base Stats ('attack', 'defense', 'speed' are derived from CharacterBaseStats
    public int attackBonus;			//stat bonuses are direct additions or substractions to a base stat.  thescould be applied by StatusEffects or Equipment or any other effect.
	public float attackMod;			//stat mods are percentage adjustments to a stat (which includes the base stat and the attackBonus)
    public float accuracy;
    public int accuracyBonus;
    public float accuracyMod;
	public float evade;
	public int evadeBonus;
    public float evadeMod;
    public uint speed;
	public int speedBonus;
    public float speedMod;
    public uint defense;
	public int defenseBonus;
    public float defenseMod;

	public CombatState combatState;

	public int selectedAbilityIndex;

	protected StatusEffect statusEffect;

	public List<EffectClass> effectClassList;
	public List<StatusEffectType> statuses;
	public List<EffectClass> removeStatus;
    public bool hasT1Status;
    public bool hasT2Status;

	//[SerializeField] protected List<uint> cooldownTimers;

	protected bool _canActThisTurn = true;

	public Animator animator
	{
		protected set { _animator = value; }
		get { return _animator; }
	}

	public Image queueImage
	{
		set { _currentQueueImage = value; }
		get { return _currentQueueImage; }
	}

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

	protected void Awake()
	{

	}

    protected virtual void Start()
	{
        combatManager = CombatManager.Instance;
		combatState = CombatState.ABLE;

		uiManager = UIManager.Instance;

		statusEffect = new StatusEffect();
        effectClassList = new List<EffectClass>();
        statuses = new List<StatusEffectType>();
        removeStatus = new List<EffectClass>();
        hasT1Status = false;
        hasT2Status = false;

		if (animator == null) 
		{
			animator = GetComponent<Animator> ();
		}

		SetDefaultStats ();

        if(baseStats == null)
        {
            baseStats = FindObjectOfType<CharacterBaseStats>();
        }

      //  cooldownTimers = new List<uint>();									//enfore size of cooldownTimers to abilities and set the timer to 0
		/*foreach(Ability ability in abilities)
		{
			cooldownTimers.Add(0);
		}*/
        selectedAbilityIndex = -1;
    }

	public virtual void BeginTurn()
	{
		Announcer.BeginTurn (this.characterName);
        if (combatManager.activeCharacter is EnemyCharacter)
        {
            uiManager.GetComponentInChildren<CanvasGroup>().interactable = false;
        }
        else
        {
            uiManager.GetComponentInChildren<CanvasGroup>().interactable = true;
        }
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
                        statusEffect.Apply(this, status);
                    }
                    else if (status.isBuff && !statuses.Contains(status.statusEffectType))
                    {
                        statusEffect.Apply(this, status);
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

	public void UseAbility(List<Character> targets, float modifier = 0.0f)
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
                        statusEffect.Apply(this, status);
                    }
                    else if (status.isBuff && !statuses.Contains(status.statusEffectType))
                    {
                        statusEffect.Apply(this, status);
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
        else if (damageType == ElementType.BLEED)
        {
            DealBleedDamage(damage);
        }
        else if (damageType == ElementType.POISON)
        {
            DealPoisonDamage(damage);
        }

        uiManager.UpdateHealthBar();

    }

	void DealPhysicalDamage(uint physicalDamage = 0)
	{
		physicalDamage -= defense;
		if (physicalDamage >= 1)
		{
            //play sound for taking damage
			currentHealth -= (uint)Mathf.Clamp(physicalDamage, 0, currentHealth);          
			if (currentHealth <= 0)
			{
				Faint();
			}
		}
	}

	void Heal(uint healing = 0)
	{
        //replenish health sound
		currentHealth = (currentHealth + healing) > maxhealth ? maxhealth : (currentHealth + healing);
	}

    public void ReduceHeat(uint amount = 0)
    {
        currentHeat -= (uint)Mathf.Clamp(amount, 0, currentHeat);
        HeatReduceThreshold();
    }

    public void DealHeatDamage(int heatDamage)
    {
        //increase heat sound
        currentHeat += (uint)Mathf.Clamp(heatDamage, 0, (maxHeat - currentHeat));		//Clamps the amount of heat damage so that it does not go above the maximumn.
        Debug.Log(name + " current heat is " + currentHeat);
        CheckHeatThreshold();
    }

    public void CheckHeatThreshold()
    {
        if (!hasT1Status && currentHeat >= 100)
        {
            print("my heat is now 100, im a little thirsty");
            int rand = Random.Range(0, combatManager.t1Statuses.Count);
            hasT1Status = true;
            effectClassList.Add(statusEffect.AddStatus(combatManager.t1Statuses[rand]));
            statuses.Add(combatManager.t1Statuses[rand]);
        }
        if (!hasT2Status && currentHeat >= 200)
        {
            print("my heat is now 200, i need AC");
            int rand = Random.Range(0, combatManager.t2Statuses.Count);
            hasT2Status = true;
            effectClassList.Add(statusEffect.AddStatus(combatManager.t2Statuses[rand]));
            statuses.Add(combatManager.t2Statuses[rand]);
        }
        if (currentHeat == 300)
        {
            print("my heat has reached 300, i am now stunned"); //TODO clean up here
            if (!statuses.Contains(StatusEffectType.Stun))
            {
                effectClassList.Add(statusEffect.AddStatus(StatusEffectType.Stun, 1));
                statuses.Add(StatusEffectType.Stun);
            }
            currentHeat = 200;
        }
    }
    public void HeatReduceThreshold()
    {
        if (hasT1Status && currentHeat <= 80)
        {
            foreach (EffectClass status in effectClassList)
            {
                if (combatManager.t1Statuses.Contains(status.statusEffectType))
                {
                    statusEffect.RemoveStatus(this, status.statusEffectType);
                    statuses.Remove(status.statusEffectType);
                }
            }
            hasT1Status = false;
        }
        if (hasT2Status && currentHeat <= 180)
        {
            foreach (EffectClass status in effectClassList)
            {
                if (combatManager.t2Statuses.Contains(status.statusEffectType))
                {
                    statusEffect.RemoveStatus(this, status.statusEffectType);
                    statuses.Remove(status.statusEffectType);
                }
            }
            hasT2Status = false;
        }
    }

	void DealPoisonDamage(uint poisonDamage)
	{
        if (poisonDamage >= 1)
        {
            //poison sound
            currentHealth -= (uint)Mathf.Clamp(poisonDamage, 0, currentHealth);
            if (currentHealth <= 0)
            {
                Faint();
            }
        }
	}

    void DealBleedDamage(uint bleedDamage)
    {
        if (bleedDamage >= 1)
        {
            //bleeding sound
            currentHealth -= (uint)Mathf.Clamp(bleedDamage, 0, currentHealth);
            if (currentHealth <= 0)
            {
                Faint();
            }
        }
    }

    public void ApplyStatus(StatusEffectType status, uint duration = 0)
    {                                               // Tandy: added this to work with Ability
        if (!statuses.Contains(status) || status == StatusEffectType.Poison)         // if not already affected by Status
        {
            if (status != StatusEffectType.Poison || !statuses.Contains(StatusEffectType.Poison))
            {
                EffectClass statusClass;
                if (status == StatusEffectType.Stun)
                {
                    statusClass = statusEffect.AddStatus(status, duration);
                }
                else
                {
                    statusClass = statusEffect.AddStatus(status);
                }
                effectClassList.Add(statusClass);                   // add Status to List to show it affects Character
                statuses.Add(status);
                if (statusClass.applyImmediately)
                {
                    statusEffect.Apply(this, statusClass);
                }
            }
            else
            {
                foreach (EffectClass maybePoison in effectClassList)
                {
                    if (maybePoison.statusEffectType == StatusEffectType.Poison)
                    {
                        maybePoison.duration = maybePoison.initDuration;
                    }
                }
            }
        }
    }

    public void Faint()
    {
		Debug.Log(this.gameObject.name + " died!");
        combatState = CombatState.EXHAUSTED;
        //fainting sound
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
			//EndTurn();
		}

    }

	private void SetDefaultStats()
	{
		if (baseStats == null)
		{
			Debug.LogError (this.gameObject.name + " does not have a CharacterBaseStats attached.  Assign one through the Inspector.");
		}
		else
		{
			characterName = baseStats.name;
			this.gameObject.name = characterName;

			//TODO set up connection from CharacterBaseStats material to this objects material

			animator.runtimeAnimatorController = baseStats.animatorController;

			queueImage = baseStats.queueImage;

			maxhealth = baseStats.maxHealth;
			maxHeat = baseStats.maxHeat;

			if (baseStats.abilities.Count > 0)
			{
				abilities.AddRange (baseStats.abilities);
			}

			attack = baseStats.attack;
			accuracy = baseStats.accuracy;
			evade = baseStats.evade;
			speed = baseStats.speed;
			defense = baseStats.defense;
		}



	}

}
