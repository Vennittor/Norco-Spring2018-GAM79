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
			//TODO swap UI graphics into to match PlayerCharacter
			ChooseAbility();
			//Get Targets - currently waits on input from UIManager for ability calls
		}
	}

	protected override void ChooseAbility()
	{
		//UIManager.AllowAbilitySelection
	}

    public void AbilityComplete()
	{	
		//UIManager.BlockAbilitySelection();
        combatState = CombatState.ABLE;
        EndTurn();
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

	//Use water (Robert)
	public void SkillWater()
	{
		combatState = CombatState.USEABILITY;
		Announcer.UseAbility(name, name, "water", "gotta hydrate my dude");

		combatState = CombatState.ABLE;
		combatManager.NextTurn();
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