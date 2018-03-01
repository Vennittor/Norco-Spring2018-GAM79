using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected CombatManager combatManager;

    public new string name;
    public int speed;

    public float maxhealth;
    public float currentHealth;

    protected void Start()
    {
        combatManager = CombatManager.Instance;
    }

    public void TakeDamage()
    {

    }

    public void Death()
    {

    }
}
