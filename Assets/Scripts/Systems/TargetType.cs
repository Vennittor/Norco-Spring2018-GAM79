using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetType
{
    public enum Who
    {
        ALLY,
        OPPONENT,
        SELF,
        EVERYONE
    }

	[SerializeField] private Who _who = Who.OPPONENT;
	[SerializeField] private int _numberOfTargets = 1;

    public Who who
	{
		get { return _who; }
	}

	public int numberOfTargets 
	{
		get { return _numberOfTargets; }
	}
}