﻿using System.Collections;
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
        if (status == StatusEffectType.Battlecry)
        {
            return ApplyBattlecry();
        }
        else if (status == StatusEffectType.Berserk)
        {
            return ApplyBerserk();
        }
        else if (status == StatusEffectType.Bleed)
        {
            return ApplyBleed();
        }
        else if (status == StatusEffectType.Crippled)
        {
            return ApplyCrippled();
        }
        else if (status == StatusEffectType.Dizzy)
        {
            return ApplyDizzy();
        }
        else if (status == StatusEffectType.Hobbled)
        {
            return ApplyHobbled();
        }
        else if (status == StatusEffectType.Scared)
        {
            return ApplyScared();
        }
        else if (status == StatusEffectType.Smoke)
        {
            return InjectDank();
        }
        else if (status == StatusEffectType.Stun)
        {
            return ApplyStun();
        }
        else
        {
            Debug.LogError("No status type was given");
            return null;
        }
    }
    public void Apply(Character character, Character.EffectClass status)
    {
        if (status.statusEffectType == StatusEffectType.Battlecry)
        {
            BattlecryStatus(character);
        }
        else if (status.statusEffectType == StatusEffectType.Berserk)
        {
            BerserkStatus(character);
        }
        else if (status.statusEffectType == StatusEffectType.Bleed)
        {
            BleedStatus(character);
        }
        else if (status.statusEffectType == StatusEffectType.Crippled)
        {
            CrippledStatus(character);
        }
        else if (status.statusEffectType == StatusEffectType.Dizzy)
        {
            DizzyStatus(character, status);
        }
        else if (status.statusEffectType == StatusEffectType.Hobbled)
        {
            HobbledStatus(character, status);
        }
        else if (status.statusEffectType == StatusEffectType.Scared)
        {
            ScaredStatus(character);
        }
        else if (status.statusEffectType == StatusEffectType.Smoke)
        {
            LegalizeGayWeed(character);
        }
        else if (status.statusEffectType == StatusEffectType.Stun)
        {
            StunStatus(character);
        }
        else
        {
            Debug.Log("Didn't give me a status you dingo");
        }
    }
    public void RemoveStatus(Character character, StatusEffectType status)
    {
        if (statusEffectType == StatusEffectType.Battlecry)
        {
            RemoveBattlecry(character);
        }
        else if (statusEffectType == StatusEffectType.Crippled)
        {
            RemoveCrippled(character);
        }
        else if (statusEffectType == StatusEffectType.Scared)
        {
            RemoveScared(character);
        }
        else if (statusEffectType == StatusEffectType.Smoke)
        {
            Rehab(character);
        }
        else
        {
            Debug.Log("Didn't give me a status you dingo");
        }
    }

    private void BattlecryStatus(Character character)
    {
        // increase attack bonus
    }
    private void RemoveBattlecry(Character character)
    {
        // remove attack bonus
    }
    private Character.EffectClass ApplyBattlecry()
    {
        statusEffectType = StatusEffectType.Battlecry;
        isBuff = true;
        duration = 2;
        applyImmediately = true;
        checkAtStart = false;
        return new Character.EffectClass(statusEffectType, isBuff, duration, applyImmediately, checkAtStart);
    }

    private void BerserkStatus(Character character)
    {
        Debug.Log(character.name + " is super mad!!!!!!!!!!!!!");
    }
    private Character.EffectClass ApplyBerserk()
    {
        statusEffectType = StatusEffectType.Berserk;
        isBuff = false;
        duration = -1;
        applyImmediately = false;
        checkAtStart = true;
        return new Character.EffectClass(statusEffectType, isBuff, duration, applyImmediately, checkAtStart);
    }

    private void BleedStatus(Character character)
    {
        uint dot = 1;
        character.ApplyDamage(dot, ElementType.BLEED);
    }
    private Character.EffectClass ApplyBleed()
    {
        statusEffectType = StatusEffectType.Bleed;
        isBuff = false;
        duration = 3;
        applyImmediately = false;
        checkAtStart = false;
        return new Character.EffectClass(statusEffectType, isBuff, duration, applyImmediately, checkAtStart);
    }

    private void CrippledStatus(Character character)
    {
        // reduce speed bonus
        // reduce accuracy bonus
    }
    private void RemoveCrippled(Character character)
    {
        // increase speed bonus
        // increase accuracy bonus
    }
    private Character.EffectClass ApplyCrippled()
    {
        statusEffectType = StatusEffectType.Crippled;
        isBuff = true;
        duration = 2;
        applyImmediately = true;
        checkAtStart = false;
        return new Character.EffectClass(statusEffectType, isBuff, duration, applyImmediately, checkAtStart);
    }

    private void DizzyStatus(Character character, Character.EffectClass status)
    {
        uint dot = 1;
        character.ApplyDamage(dot, ElementType.BLEED);
        if (!status.buffTicked)
        {
            //attack down
            status.buffTicked = true;
        }
    }
    private void RemoveDizzy(Character character)
    {
        //attack up
    }
    private Character.EffectClass ApplyDizzy()
    {
        statusEffectType = StatusEffectType.Dizzy;
        isBuff = false;
        duration = 2;
        applyImmediately = false;
        checkAtStart = true;
        return new Character.EffectClass(statusEffectType, isBuff, duration, applyImmediately, checkAtStart, true);
    }

    private void HobbledStatus(Character character, Character.EffectClass status)
    {
        uint dot = 1;
        character.ApplyDamage(dot, ElementType.BLEED);
        if (!status.buffTicked)
        {
            character.speedMod -= .5f;
            //evasion down
            status.buffTicked = true;
        }
    }
    private Character.EffectClass ApplyHobbled()
    {
        statusEffectType = StatusEffectType.Hobbled;
        isBuff = false;
        duration = -1;
        applyImmediately = true;
        checkAtStart = true;
        return new Character.EffectClass(statusEffectType, isBuff, duration, applyImmediately, checkAtStart, true);
    }
    private void HobbledBuffStatus(Character character)
    {
        character.speedMod -= .5f;
    }

    private void ScaredStatus(Character character)
    {
        character.attackMod -= .3f; // 70% damage output
    }
    private void RemoveScared(Character character)
    {
        character.attackMod += .3f;
    }
    private Character.EffectClass ApplyScared()
    {
        statusEffectType = StatusEffectType.Scared;
        isBuff = true;
        duration = 2;
        applyImmediately = true;
        checkAtStart = true;
        return new Character.EffectClass(statusEffectType, isBuff, duration, applyImmediately, checkAtStart);
    }

    private void LegalizeGayWeed(Character character)
    {
        character.evadeBonus += 1000;
    }
    private void Rehab(Character character)
    {
        character.evadeBonus -= 1000;
    }
    private Character.EffectClass InjectDank()
    {
        smokeTurnCounter = CombatManager.Instance.roundCounter;
        statusEffectType = StatusEffectType.Smoke;
        isBuff = true;
        duration = 1;
        applyImmediately = true;
        checkAtStart = true;
        return new Character.EffectClass(statusEffectType, isBuff, duration, applyImmediately, checkAtStart);
    }

    private void StunStatus(Character character)
    {
        Debug.Log("Talk shit get hit");
    }
    private Character.EffectClass ApplyStun()
    {
        statusEffectType = StatusEffectType.Stun;
        isBuff = false;
        duration = 1;
        applyImmediately = false;
        checkAtStart = true;
        return new Character.EffectClass(statusEffectType, isBuff, duration, applyImmediately, checkAtStart);
    }
}
