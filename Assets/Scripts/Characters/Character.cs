using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
//    public List<StatusEffect> statusEffects = new List<StatusEffect>();
    
    [SerializeField] protected List<Ability> abilities = new List<Ability>(); 
    // Tandy: maybe abilites[0] is basic attack? then abilities[1] is SkillOne, etc.

    [SerializeField] protected List<Status> statuses = new List<Status>();
    // Tandy: List of Status to show what Character is affected by

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

    public void SetState(CombatState state)
    {
        combatState = state;
		if (combatState == CombatState.ABLE)
        {

        }
		else if (combatState == CombatState.DISABLED)
        {
            combatManager.Disable(this);
        }
    }

	public virtual void BeginTurn()
	{

	}

	public void EndTurn()
	{
		if (combatManager.activeCharacter == this)
		{
			combatManager.NextTurn();
		}
	}

    public void TakeDamage(uint damage = 0)
    {
        damage -= defense;
        if (damage >= 1)
        {
            currentHealth -= (uint)Mathf.Clamp(damage, 0, currentHealth);
            if (currentHealth == 0)
            {
                Faint();
            }
        }
    }
    public void TakeHeatDamage(uint heatDamage = 0)
    {
        currentHeat += (uint)Mathf.Clamp(heatDamage, 0, (maxHeat - currentHeat));
        if (currentHeat == maxHeat)
        {
            EndTurn();
            currentHeat = maxHeat - 100; //or whatever we settle on the value for 1 bar is
        }
    }

    public void ApplyStatus(Status status) { // Tandy: added this to work with Ability
        if(statuses.Contains(status) == false) { // if not already affected by Status
            statuses.Add(status); // add Status to List to show it affects Character
        }
    }

    public void Faint()
    {
        Debug.Log(name + " died!");
        combatState = CombatState.EXHAUSTED;
        combatManager.Disable(this);
		EndTurn();
    }
}
