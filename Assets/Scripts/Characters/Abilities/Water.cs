using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Water", menuName = "Water Ability")]
public class Water : Ability
{
    [SerializeField] private uint usesLeft = 3;
    [SerializeField] private uint maxUses = 3;
    uint amount = 100;

    public void RefillWater(uint uses)
    {
        usesLeft = (uint)Mathf.Clamp((float)uses, 0, (float)maxUses);
    }

    public new void UseAbility()
    {
        if (usesLeft > 0)
        {
			characterUser.ApplyDamage(amount, ElementType.WATER);
            usesLeft--;
        }
        
        EndAbility();
    }
}
