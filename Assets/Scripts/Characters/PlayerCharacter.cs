using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public HeatZone heatState;
    public int heatIntensity;

	//private Ability activeAbility = null;

    private new void Start()
    {
        base.Start();
        //heatState = HeatZone.OutofHeat;

        foreach(Ability ability in abilities)
        {
            ability.EquipAbility(this);
        }
    }

	public new void BeginTurn()
	{
		if (base.BeginTurn ()) 
		{
			//Choose Ability and Get Targets
		}
	}

    public void AbilityComplete()
    {
        combatState = CombatState.ABLE;
        EndTurn();
    }

    //Use water (Robert)
    public void SkillWater()
    {
        combatState = CombatState.USEABILITY;
        Announcer.UseAbility(name, name, "water", "gotta hydrate my dude");
        
        combatState = CombatState.ABLE;
        combatManager.NextTurn();
    }

	private EnemyCharacter RandEnemyTarget()
    {
        List<EnemyCharacter> enemies = new List<EnemyCharacter>();
		foreach(Character character in combatManager.charactersInCombat)
        {
			if (character is EnemyCharacter) 
			{
				enemies.Add (character as EnemyCharacter);
			}
        }
        EnemyCharacter enemyCharacter = enemies[Random.Range(0, enemies.Count)];
        return enemyCharacter;
    }

    /*public void EnterHeat()
    {
        heatState = HeatZone.InHeat;
    }

    public void ExitHeat()
    {
        heatState = HeatZone.OutofHeat;
    }*/

    public enum HeatZone
    {
        OUTOFHEAT,
        INHEAT
    }

    public void SetHeatRate(int heat)
    {
        heatIntensity += heat;
    }
}