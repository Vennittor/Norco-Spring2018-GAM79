using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectClass
{
	public StatusEffectType statusEffectType;
	public bool isBuff;
	public int duration;
	public bool applyImmediately;
	public bool checkAtStart;

	public EffectClass(StatusEffectType statusType, bool isBuff, int duration, bool applyNow, bool checkStart)
	{
		statusEffectType = statusType;
		this.isBuff = isBuff;
		this.duration = duration;
		applyImmediately = applyNow;
		checkAtStart = checkStart;
	}

	public void DecDuration()
	{
		duration--;
	}
}
