using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Ability
{
    //Water Related(Robert)
    [SerializeField]
    private int waterUses = 3;

    //for water ability(robert)
    public void UseWater()
    {
        if (waterUses > 0)
        {
            Announcer.UseAbility(name, name, "water", "gotta hydrate my dude");
            waterUses--;
        }
        else if (waterUses == 0)
        {
            Debug.Log("you lean back for a swig, but only drink in disappointment");
        }
    }
}
