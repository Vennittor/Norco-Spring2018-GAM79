using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public int hp;
    public int atk;
    public int def;
    public int spd;
    public int heatEffectiveness;
    public int waterEffectiveness;
    public int numberOfCanteens;

    public void SetTo(PlayerStats _stats)
    {
        hp = _stats.hp;
        atk = _stats.atk;
        def = _stats.def;
        spd = _stats.spd;
        heatEffectiveness = _stats.heatEffectiveness;
        waterEffectiveness = _stats.waterEffectiveness;
        numberOfCanteens = _stats.numberOfCanteens;
    }

    public void Add(PlayerStats baseStats, List<PlayerStats> _newStats)
    {
        SetTo(baseStats);
        foreach (PlayerStats _ringStats in _newStats)
        {
            hp += _ringStats.hp;
            atk += _ringStats.atk;
            def += _ringStats.def;
            spd += _ringStats.spd;
            heatEffectiveness += _ringStats.heatEffectiveness;
            waterEffectiveness += _ringStats.waterEffectiveness;
            numberOfCanteens += _ringStats.numberOfCanteens;
        }
    }
}
