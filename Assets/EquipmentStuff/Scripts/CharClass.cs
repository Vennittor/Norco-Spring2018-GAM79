using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharClass : MonoBehaviour {
    public PlayerStats myStats;
    public PlayerStats myBaseStats;

    public Ring[] ringSlots = new Ring[2];

    public void Awake()
    {
        myStats.SetTo(myBaseStats);
    }

    public void RefreshStats()
    {
        List<PlayerStats> statsFromRings = new List<PlayerStats>();
        foreach (Ring r in ringSlots)
        {
            if (r != null)
            {
                statsFromRings.Add(r.statChanges);
            }
        }
        myStats.Add(myBaseStats, statsFromRings);
    }
}
