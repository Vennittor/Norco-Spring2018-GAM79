using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected CombatManager combatManager = CombatManager.Instance;

    public new string name;
    public int speed;

    public float maxhealth;
    public float currentHealth;

    public void TakeDamage()
    {

    }

    public void Death()
    {

    }
}
