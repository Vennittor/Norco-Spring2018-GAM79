using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect
{
    public StatusEffectType effectType;

	

    public void LethargyFunction(int speed)
    {
        effectType = StatusEffectType.Lethargy;
        speed = speed / 2;
    }

    public void BerserkFunction()
    {
        effectType = StatusEffectType.Berserk;
        //player can only use ability 1
    }

    public void StunAbility()
    {
        effectType = StatusEffectType.Stun;
        //player looses 1 turn and then has heat reduced
    }
}
