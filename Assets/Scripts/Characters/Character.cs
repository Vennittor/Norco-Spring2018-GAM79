using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum CombatState
    {
        ACTIVE, INACTIVE
    }
    public CombatState combatState;
    protected CombatManager combatManager;

    public new string name;
    public int speed;

    public float maxhealth;
    public float currentHealth;

    protected void Start()
    {
        combatManager = CombatManager.Instance;
        combatState = CombatState.ACTIVE;
    }

	public virtual void BeginTurn()
	{

	}

    public void TakeDamage()
    {

    }

    public void Death()
    {

    }
}
