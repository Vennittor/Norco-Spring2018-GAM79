using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
//    public List<StatusEffect> statusEffects = new List<StatusEffect>();

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
    public uint defence;
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
        damage -= defence;
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
            //lose a turn;
            //heat -100;
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
