using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectClass
{
    public StatusEffectType statusEffectType;
    public bool isBuff;
    public int initDuration;
    public int duration;
    public bool applyImmediately;
    public bool checkAtStart;
    public bool isBoth;
    public bool buffTicked;
    public int poisonDuration;

    public EffectClass(StatusEffectType statusEffectType, bool isBuff, int initDuration, int duration, bool applyImmediately, bool checkAtStart, bool isBoth = false, bool buffTicked = false, int poisonDuration = 0)
    {
        this.statusEffectType = statusEffectType;
        this.isBuff = isBuff;
        this.initDuration = initDuration;
        this.duration = duration;
        this.applyImmediately = applyImmediately;
        this.checkAtStart = checkAtStart;
        this.isBoth = isBoth;
        this.buffTicked = buffTicked;
        this.poisonDuration = poisonDuration;
    }

    public void DecDuration()
    {
        duration--;
    }
}
