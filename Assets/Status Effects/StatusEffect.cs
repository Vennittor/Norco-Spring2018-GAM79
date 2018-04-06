using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect: MonoBehaviour 
{
	public enum EffectType
	{
		Stun,
		Berserk,
		Lethargy
	}

    public void LethargyFunction(int speed)
    {
        speed = speed / 2;
    }

    public void BerserkFunction()
    {
        //player can only use ability 1
    }

    public void StunAbility()
    {
        //player looses 1 turn and then has heat reduced
    }
}
