using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Ability
{
    

    //for water ability(robert)
    public void UseWater(int uses, float hLevel)
    {
        uses--;
        hLevel -= 100;//going off of what i recall the size of the heat bar is (300), since design said that they want 1 use to lower 1/3 of the bar
        
    }
}
