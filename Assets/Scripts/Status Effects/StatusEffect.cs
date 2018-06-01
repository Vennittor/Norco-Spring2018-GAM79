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

    public EffectClass AddStatus(StatusEffectType status, uint stunDuration = 0, uint dotDamage = 0)
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
        else if (status == StatusEffectType.Blind)
        {
            return ApplyBlind();
        }
        else if (status == StatusEffectType.Bloodthirst)
        {
            return ApplyBloodthirst();
        }
        else if (status == StatusEffectType.Fear)
        {
            return ApplyFear();
        }
        else if (status == StatusEffectType.Hamstring)
        {
            return ApplyHamstring();
        }
        else if (status == StatusEffectType.Mash)
        {
            return ApplyMash();
        }
        else if (status == StatusEffectType.Poison)
        {
            return ApplyPoison();
        }
        else if (status == StatusEffectType.Slow)
        {
            return ApplySlow();
        }
        else if (status == StatusEffectType.Smoke)
        {
            return InjectDank();
        }
        else if (status == StatusEffectType.Stun)
        {
            return ApplyStun(stunDuration);
        }
        else if (status == StatusEffectType.WarriorSpirit)
        {
            return ApplyWarriorSpirit();
        }
        else
        {
            Debug.LogError("No status type was given");
            return null;
        }
    }
    public void Apply(Character character, EffectClass status)
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
        else if (status.statusEffectType == StatusEffectType.Blind)
        {
            BlindStatus(character);
        }
        else if (status.statusEffectType == StatusEffectType.Bloodthirst)
        {
            BloodthirstStatus(character);
        }
        else if (status.statusEffectType == StatusEffectType.Fear)
        {
            FearStatus(character);
        }
        else if (status.statusEffectType == StatusEffectType.Hamstring)
        {
            HamstringStatus(character);
        }
        else if (status.statusEffectType == StatusEffectType.Mash)
        {
            MashStatus(character);
        }
        else if (status.statusEffectType == StatusEffectType.Poison)
        {
            PoisonStatus(character, status);
        }
        else if (status.statusEffectType == StatusEffectType.Slow)
        {
            SlowStatus(character);
        }
        else if (status.statusEffectType == StatusEffectType.Smoke)
        {
            LegalizeGayWeed(character);
        }
        else if (status.statusEffectType == StatusEffectType.Stun)
        {
            StunStatus(character);
        }
        else if (status.statusEffectType == StatusEffectType.WarriorSpirit)
        {
            WarriorSpiritStatus(character);
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
        else if (statusEffectType == StatusEffectType.Blind)
        {
            RemoveBlind(character);
        }
        else if (statusEffectType == StatusEffectType.Bloodthirst)
        {
            RemoveBloodthirst(character);
        }
        else if (statusEffectType == StatusEffectType.Berserk)
        {
            RemoveBerserk(character);
        }
        else if (statusEffectType == StatusEffectType.Fear)
        {
            RemoveFear(character);
        }
        else if (statusEffectType == StatusEffectType.Hamstring)
        {
            RemoveHamstring(character);
        }
        else if (statusEffectType == StatusEffectType.Mash)
        {
            RemoveMash(character);
        }
        else if (statusEffectType == StatusEffectType.Slow)
        {
            RemoveSlow(character);
        }
        else if (statusEffectType == StatusEffectType.Smoke)
        {
            Rehab(character);
        }
        else if (statusEffectType == StatusEffectType.WarriorSpirit)
        {
            RemoveWarriorSpirit(character);
        }
        else
        {
            Debug.LogError(status + " doesn't have a remove function");
            //Debug.Log("Didn't give me a status you dingo");
        }
    }

    private void BattlecryStatus(Character character)
    {
        character.defenseMod += .1f;
    }
    private void RemoveBattlecry(Character character)
    {
        character.defenseMod -= .1f;
    }
    private EffectClass ApplyBattlecry()
    {
        statusEffectType = StatusEffectType.Battlecry;
        isBuff = true;
        duration = 2;
        applyImmediately = true;
        checkAtStart = false;
        return new EffectClass(statusEffectType, isBuff, duration, duration, applyImmediately, checkAtStart);
    }

    private void BerserkStatus(Character character)
    {
        character.speedBonus += 2;
        character.attackBonus += 2;
        character.evadeMod += .3f;
        character.defenseMod += .1f;
    }
    private void RemoveBerserk(Character character)
    {
        character.speedBonus -= 2;
        character.attackBonus -= 2;
        character.evadeMod -= .3f;
        character.defenseMod -= .1f;
    }
    private EffectClass ApplyBerserk()
    {
        statusEffectType = StatusEffectType.Berserk;
        isBuff = true;
        duration = -1;
        applyImmediately = true;
        checkAtStart = true;
        return new EffectClass(statusEffectType, isBuff, duration, duration, applyImmediately, checkAtStart);
    }

    private void BleedStatus(Character character)
    {
        uint dot = 2;
        character.ApplyDamage(dot, ElementType.BLEED);
    }
    private EffectClass ApplyBleed()
    {
        statusEffectType = StatusEffectType.Bleed;
        isBuff = false;
        duration = 3;
        applyImmediately = false;
        checkAtStart = false;
        return new EffectClass(statusEffectType, isBuff, duration, duration, applyImmediately, checkAtStart);
    }

    private void BlindStatus(Character character)
    {
        character.accuracyMod -= .4f;
    }
    private void RemoveBlind(Character character)
    {
        character.accuracyMod += .4f;
    }
    private EffectClass ApplyBlind()
    {
        statusEffectType = StatusEffectType.Blind;
        isBuff = true;
        duration = 2;
        applyImmediately = true;
        checkAtStart = true;
        return new EffectClass(statusEffectType, isBuff, duration, duration, applyImmediately, checkAtStart);
    }

    private void BloodthirstStatus(Character character)
    {
        character.attackBonus += 3;
    }
    private void RemoveBloodthirst(Character character)
    {
        character.attackBonus -= 3;
    }
    private EffectClass ApplyBloodthirst()
    {
        statusEffectType = StatusEffectType.Bloodthirst;
        isBuff = true;
        duration = 3;
        applyImmediately = true;
        checkAtStart = true;
        return new EffectClass(statusEffectType, isBuff, duration, duration, applyImmediately, checkAtStart);
    }

    private void ExoskeletonStatus(Character character)
    {
        character.defenseMod += .5f;
    }
    private void RemoveExoskeleton(Character character)
    {
        character.defenseMod -= .5f;
    }
    private EffectClass ApplyExoskeleton()
    {
        statusEffectType = StatusEffectType.Exoskeleton;
        isBuff = true;
        duration = 3;
        applyImmediately = true;
        checkAtStart = true;
        return new EffectClass(statusEffectType, isBuff, duration, duration, applyImmediately, checkAtStart);
    }

    private void FearStatus(Character character)
    {
        character.ApplyDamage(character.currentHealth / 2);
    }
    private void RemoveFear(Character character)
    {
        character.ApplyDamage(15, ElementType.HEALING);
    }
    private EffectClass ApplyFear()
    {
        statusEffectType = StatusEffectType.Fear;
        isBuff = true;
        duration = 3;
        applyImmediately = true;
        checkAtStart = true;
        return new EffectClass(statusEffectType, isBuff, duration, duration, applyImmediately, checkAtStart);
    }

    private void HamstringStatus(Character character)
    {
        character.evadeMod -= .4f;
    }
    private void RemoveHamstring(Character character)
    {
        character.evadeMod += .4f;
    }
    private EffectClass ApplyHamstring()
    {
        statusEffectType = StatusEffectType.Hamstring;
        isBuff = true;
        duration = 2;
        applyImmediately = true;
        checkAtStart = false;
        return new EffectClass(statusEffectType, isBuff, duration, duration, applyImmediately, checkAtStart);
    }

    private void MashStatus(Character character)
    {
        character.speedBonus += 5;
    }
    private void RemoveMash(Character character)
    {
        character.speedBonus -= 5;
    }
    private EffectClass ApplyMash()
    {
        statusEffectType = StatusEffectType.Mash;
        isBuff = true;
        duration = 2;
        applyImmediately = true;
        checkAtStart = true;
        return new EffectClass(statusEffectType, isBuff, duration, duration, applyImmediately, checkAtStart);
    }

    private void PoisonStatus(Character character, EffectClass status)
    {
        status.poisonDuration++;
        uint dot = (uint)status.poisonDuration;
        Debug.LogWarning("Poison: duration = " + status.poisonDuration + " damage = " + dot);
        character.ApplyDamage(dot, ElementType.POISON);
    }
    private EffectClass ApplyPoison()
    {
        statusEffectType = StatusEffectType.Poison;
        isBuff = false;
        duration = 4;
        applyImmediately = false;
        checkAtStart = false;
        return new EffectClass(statusEffectType, isBuff, duration, duration, applyImmediately, checkAtStart);
    }

    private void SlowStatus(Character character)
    {
        character.speedBonus -= 2;
    }
    private void RemoveSlow(Character character)
    {
        character.speedBonus += 2;
    }
    private EffectClass ApplySlow()
    {
        statusEffectType = StatusEffectType.Slow;
        isBuff = true;
        duration = 2;
        applyImmediately = true;
        checkAtStart = false;
        return new EffectClass(statusEffectType, isBuff, duration, duration, applyImmediately, checkAtStart);
    }

    private void LegalizeGayWeed(Character character)
    {
        character.evadeMod += .7f;
    }
    private void Rehab(Character character)
    {
        character.evadeMod -= -.7f;
    }
    private EffectClass InjectDank()
    {
        smokeTurnCounter = CombatManager.Instance.roundCounter;
        statusEffectType = StatusEffectType.Smoke;
        isBuff = true;
        duration = 1;
        applyImmediately = true;
        checkAtStart = true;
        return new EffectClass(statusEffectType, isBuff, duration, duration, applyImmediately, checkAtStart);
    }

    private void StunStatus(Character character)
    {
        Debug.Log("Talk shit get hit");
    }
    private EffectClass ApplyStun(uint stunDuration)
    {
        statusEffectType = StatusEffectType.Stun;
        isBuff = false;
        duration = (int)stunDuration;
        applyImmediately = false;
        checkAtStart = true;
        return new EffectClass(statusEffectType, isBuff, duration, duration, applyImmediately, checkAtStart);
    }

    private void SuperiorStatus(Character character)
    {
        character.speedBonus += 2;
        character.defenseMod += .3f;
        character.attackMod -= .5f;
    }
    private void RemoveSuperior(Character character)
    {
        character.speedBonus -= 2;
        character.defenseMod -= .3f;
        character.attackMod += .5f;
    }
    private EffectClass ApplySuperior()
    {
        statusEffectType = StatusEffectType.Superior;
        isBuff = true;
        duration = 20;
        applyImmediately = true;
        checkAtStart = false;
        return new EffectClass(statusEffectType, isBuff, duration, duration, applyImmediately, checkAtStart);
    }

    private void WarriorSpiritStatus(Character character)
    {
        character.defenseMod += 1;
    }
    private void RemoveWarriorSpirit(Character character)
    {
        character.defenseMod -= 1;
    }
    private EffectClass ApplyWarriorSpirit()
    {
        statusEffectType = StatusEffectType.WarriorSpirit;
        isBuff = true;
        duration = 1;
        applyImmediately = true;
        checkAtStart = false;
        return new EffectClass(statusEffectType, isBuff, duration, duration, applyImmediately, checkAtStart);
    }
}
