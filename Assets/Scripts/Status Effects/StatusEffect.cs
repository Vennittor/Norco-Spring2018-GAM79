using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect
{
    public int duration;
    public bool applyImmediately;
    public bool checkAtStart;
    public bool isBuff;
    public uint smokeTurnCounter;

    StatusEffectType statusEffectType;

    public Character.EffectClass AddStatus(StatusEffectType status)
    {
        if (status == StatusEffectType.Lethargy)
        {
            return ApplyLethargy();
        }
        else if (status == StatusEffectType.Berserk)
        {
            return ApplyBerserk();
        }
        else if (status == StatusEffectType.Stun)
        {
            return ApplyStun();
        }
        else if (status == StatusEffectType.Smoke)
        {
            return InjectDank();
        }
        else if (status == StatusEffectType.Scared)
        {
            return ApplyScared();
        }
        else if (status == StatusEffectType.Blind)
        {
            return ApplyBlind();
        }
        else if (status == StatusEffectType.Bleed)
        {
            return ApplyBleed();
        }
        else
        {
            Debug.LogError("No status type was given");
            return null;
        }
    }
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

    public void LethargyStatus(Character character)
    {
        character.speedMod -= .5f;
    }
    public void RemoveLethargy(Character character)
    {
        character.speedMod += .5f;
    }
    public Character.EffectClass ApplyLethargy()
    {
        statusEffectType = StatusEffectType.Lethargy;
        isBuff = false;
        duration = -1;
        applyImmediately = true;
        checkAtStart = true;
        return new Character.EffectClass(statusEffectType, isBuff, duration, applyImmediately, checkAtStart);
    }

    public void BerserkStatus(Character character)
    {
        Debug.Log(character.name + " is super mad!!!!!!!!!!!!!");
    }
    public Character.EffectClass ApplyBerserk()
    {
        statusEffectType = StatusEffectType.Berserk;
        isBuff = false;
        duration = -1;
        applyImmediately = false;
        checkAtStart = true;
        return new Character.EffectClass(statusEffectType, isBuff, duration, applyImmediately, checkAtStart);
    }

    public void StunStatus(Character character)
    {
        Debug.Log("Talk shit get hit");
    }
    public void RemoveStun(Character character)
    {
        character.combatState = Character.CombatState.ABLE;
    }
    public Character.EffectClass ApplyStun()
    {
        statusEffectType = StatusEffectType.Stun;
        isBuff = false;
        duration = 2;
        applyImmediately = false;
        checkAtStart = true;
        return new Character.EffectClass(statusEffectType, isBuff, duration, applyImmediately, checkAtStart);
    }

    public void LegalizeGayWeed(Character character)
    {
        character.evadeBonus += 1000;
    }
    public void Rehab(Character character)
    {
        character.evadeBonus -= 1000;
    }
    public Character.EffectClass InjectDank()
    {
        smokeTurnCounter = CombatManager.Instance.roundCounter;
        statusEffectType = StatusEffectType.Smoke;
        isBuff = true;
        duration = 1;
        applyImmediately = true;
        checkAtStart = true;
        return new Character.EffectClass(statusEffectType, isBuff, duration, applyImmediately, checkAtStart);
    }

    public void ScaredStatus(Character character)
    {
        character.attackMod -= .3f; // 70% damage output
    }
    public void RemoveScared(Character character)
    {
        character.attackMod += .3f;
    }
    public Character.EffectClass ApplyScared()
    {
        statusEffectType = StatusEffectType.Scared;
        isBuff = true;
        duration = 2;
        applyImmediately = true;
        checkAtStart = true;
        return new Character.EffectClass(statusEffectType, isBuff, duration, applyImmediately, checkAtStart);
    }

    public void BlindStatus(Character character)
    {
        character.accuracyMod -= .3f;
    }
    public void RemoveBlind(Character character)
    {
        character.accuracyMod += .3f;
    }
    public Character.EffectClass ApplyBlind()
    {
        statusEffectType = StatusEffectType.Blind;
        isBuff = true;
        duration = 2;
        applyImmediately = false;
        checkAtStart = true;
        return new Character.EffectClass(statusEffectType, isBuff, duration, applyImmediately, checkAtStart);
    }

    public void BleedStatus(Character character)
    {
        uint dot = 1;
        character.ApplyDamage(dot, ElementType.BLEED);
    }
    public Character.EffectClass ApplyBleed()
    {
        statusEffectType = StatusEffectType.Bleed;
        isBuff = false;
        duration = 2;
        applyImmediately = false;
        checkAtStart = false;
        return new Character.EffectClass(statusEffectType, isBuff, duration, applyImmediately, checkAtStart);
    }
}
