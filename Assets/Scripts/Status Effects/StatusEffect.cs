using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect
{
    public Character connectedCharacter;
    public int duration;

    StatusEffectType statusEffectType;

    public void LethargyStatus()
    {
        statusEffectType = StatusEffectType.Lethargy;
        connectedCharacter.speed /= 2;
        duration = -1;
    }

    public void BerserkStatus()
    {
        statusEffectType = StatusEffectType.Berserk;
        //player can only use ability 1
        duration = -1;
    }

    public void StunStatus()
    {
        statusEffectType = StatusEffectType.Stun;
        //player loses 1 turn and then has heat reduced
        duration = 1;
        connectedCharacter.currentHealth -= 100;
    }

    public void LegalizeGayWeed()
    {
        statusEffectType = StatusEffectType.Smoke;
        connectedCharacter.evade = 100;
        duration = 1;
    }

    public void ScaredStatus(Character character)
    {
        statusEffectType = StatusEffectType.Scared;
        connectedCharacter.attackMod = .7f; // 70% damage output
    }

    public void BlindStatus()
    {
        statusEffectType = StatusEffectType.Blind;
        connectedCharacter.accuracy = 70;
    }

    public void BleedStatus()
    {
        statusEffectType = StatusEffectType.Bleed;
        uint dot = 1;
        duration = 3;
    }
}
