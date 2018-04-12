using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect
{
    public Character connectedCharacter;
    public int duration;
    public bool checkAtStart;

    StatusEffectType statusEffectType;

    public void LethargyStatus()
    {
        //connectedCharacter.speedMod -= .5f;
    }
    public Character.EffectStruct ApplyLethargy()
    {
        statusEffectType = StatusEffectType.Lethargy;
        duration = -1;
        checkAtStart = true;
        return new Character.EffectStruct(statusEffectType, duration, checkAtStart);
    }

    public void BerserkStatus()
    {
        checkAtStart = true;
        statusEffectType = StatusEffectType.Berserk;
        //player can only use ability 1
        duration = -1;
        //Character.EffectStruct effect = new Character.EffectStruct(checkAtStart, statusEffectType, duration);
    }

    public void StunStatus()
    {
        checkAtStart = true;
        statusEffectType = StatusEffectType.Stun;
        //player loses 1 turn and then has heat reduced
        duration = 1;
        //connectedCharacter.currentHealth -= 100;
        //Character.EffectStruct effect = new Character.EffectStruct(checkAtStart, statusEffectType, duration);
    }

    public void LegalizeGayWeed()
    {
        checkAtStart = true;
        statusEffectType = StatusEffectType.Smoke;
        //connectedCharacter.evade = 100;
        duration = 1;
        //Character.EffectStruct effect = new Character.EffectStruct(checkAtStart, statusEffectType, duration);
    }

    public void ScaredStatus(Character character)
    {
        checkAtStart = true;
        statusEffectType = StatusEffectType.Scared;
        //connectedCharacter.attackMod = -.3f; // 70% damage output
        //Character.EffectStruct effect = new Character.EffectStruct(checkAtStart, statusEffectType, duration);
    }

    public void BlindStatus()
    {
        checkAtStart = true;
        statusEffectType = StatusEffectType.Blind;
        //connectedCharacter.accuracy = 70;
        //Character.EffectStruct effect = new Character.EffectStruct(checkAtStart, statusEffectType, duration);
    }

    public void BleedStatus()
    {
        checkAtStart = false;
        statusEffectType = StatusEffectType.Bleed;
        //uint dot = 1;
        duration = 3;
        //Character.EffectStruct effect = new Character.EffectStruct(checkAtStart, statusEffectType, duration);
    }
}
