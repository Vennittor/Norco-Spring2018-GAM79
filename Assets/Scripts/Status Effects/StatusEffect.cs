using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect
{
    public int duration;
    public bool applyImmediately;
    public bool checkAtStart;

    StatusEffectType statusEffectType;

    public void Apply(Character character, StatusEffectType statusType)
    {
        if (statusType == StatusEffectType.Lethargy)
        {
            LethargyStatus(character);
        }
        else if (statusType == StatusEffectType.Berserk)
        {
            BerserkStatus(character);
        }
        else if (statusType == StatusEffectType.Stun)
        {
            StunStatus(character);
        }
        else if (statusType == StatusEffectType.Smoke)
        {
            LegalizeGayWeed(character);
        }
        else if (statusType == StatusEffectType.Scared)
        {
            ScaredStatus(character);
        }
        else if (statusType == StatusEffectType.Blind)
        {
            BlindStatus(character);
        }
        else if (statusType == StatusEffectType.Bleed)
        {
            BleedStatus(character);
        }
    }

    public void LethargyStatus(Character connectedCharacter)
    {
        connectedCharacter.speedMod -= .5f;
    }
    public Character.EffectStruct ApplyLethargy()
    {
        statusEffectType = StatusEffectType.Lethargy;
        duration = -1;
        applyImmediately = true;
        checkAtStart = true;
        return new Character.EffectStruct(statusEffectType, duration, applyImmediately, checkAtStart);
    }

    public void BerserkStatus(Character connectedCharacter)
    {
        //player can only use ability 1
    }
    public Character.EffectStruct ApplyBerserk()
    {
        statusEffectType = StatusEffectType.Berserk;
        duration = -1;
        applyImmediately = false;
        checkAtStart = true;
        return new Character.EffectStruct(statusEffectType, duration, applyImmediately, checkAtStart);
    }

    public void StunStatus(Character connectedCharacter)
    {
        //player loses 2 turns
    }
    public Character.EffectStruct ApplyStun()
    {
        statusEffectType = StatusEffectType.Stun;
        duration = 2;
        applyImmediately = false;
        checkAtStart = true;
        return new Character.EffectStruct(statusEffectType, duration, applyImmediately, checkAtStart);
    }

    // How long does this last? Until end of round?
    public void LegalizeGayWeed(Character connectedCharacter)
    {
        connectedCharacter.evadeBonus = 10000;
    }
    public Character.EffectStruct InjectDank()
    {
        statusEffectType = StatusEffectType.Smoke;
        duration = 1;
        applyImmediately = true;
        checkAtStart = true;
        return new Character.EffectStruct(statusEffectType, duration, applyImmediately, checkAtStart);
    }

    public void ScaredStatus(Character connectedCharacter)
    {
        connectedCharacter.attackMod = -.3f; // 70% damage output
    }
    public Character.EffectStruct ApplyScared()
    {
        statusEffectType = StatusEffectType.Scared;
        duration = 2;
        applyImmediately = true;
        checkAtStart = true;
        return new Character.EffectStruct(statusEffectType, duration, applyImmediately, checkAtStart);
    }

    public void BlindStatus(Character connectedCharacter)
    {
        connectedCharacter.accuracyMod = 70;
    }
    public Character.EffectStruct ApplyBlind()
    {
        statusEffectType = StatusEffectType.Blind;
        duration = 2;
        applyImmediately = false;
        checkAtStart = true;
        return new Character.EffectStruct(statusEffectType, duration, applyImmediately, checkAtStart);
    }

    public void BleedStatus(Character connectedCharacter)
    {
        uint dot = 1;
        connectedCharacter.ApplyDamage(dot, ElementType.BLEED);
    }
    public Character.EffectStruct ApplyBleed()
    {
        statusEffectType = StatusEffectType.Bleed;
        duration = 2;
        applyImmediately = false;
        checkAtStart = false;
        return new Character.EffectStruct(statusEffectType, duration, applyImmediately, checkAtStart);
    }
}
