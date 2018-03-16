using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target
{
    
    public enum Who
    {
        ALLY,
        OPPONENT,
        SELF,
        EVERYONE
    }
    public Who who = Who.OPPONENT;

    public int numberOfTargets = 1;
}