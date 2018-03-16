using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetType
{
    public enum Who
    {
        SELF,
        ALLY,
        OPPONENT,
        EVERYONE
    }

    public enum Formation
    {
        SINGLE,
        GROUP
    }

	[SerializeField] private Who _who = Who.OPPONENT;
    [SerializeField] private Formation _formation = Formation.SINGLE;

	[SerializeField] private int _numberOfTargets = 1;

    public Who who
	{
		get
        {
            return _who;
        }
	}

    public Formation formation
    {
        get
        {
            return _formation;
        }
    }

	public int numberOfTargets 
	{
		get { return _numberOfTargets; }
	}
}