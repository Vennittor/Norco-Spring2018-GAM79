using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    // TODO ability variables
    public HeatZone heatState;
    public int heatRate;
    

    private new void Start()
    {
        base.Start();
        //heatState = HeatZone.OutofHeat;
        // TODO set up ability attachments
    }

    void Update()
    {

    }

	public override void BeginTurn()
	{

	}

	public void Skill1()
	{
        combatState = CombatState.ABILITYUSE;

        //TODO wait for call from announcer before going to next turn

		combatManager.NextTurn();
		Announcer.UseSkill(this.name, RandEnemyTarget().name, "Skill #1", "It's over 9000!");
	}
	public void Skill2()
	{
		combatManager.NextTurn();
		Announcer.UseSkill(this.name, RandEnemyTarget().name, "Skill #2", "How do I turn this thing on?");
	}
	public void Skill3()
	{
		combatManager.NextTurn();
		Announcer.UseSkill(this.name, RandEnemyTarget().name, "Skill #3", "I wish I had more Skills to use.");
	}

	private EnemyCharacter RandEnemyTarget()
    {
        List<EnemyCharacter> enemies = new List<EnemyCharacter>();
		foreach(Character character in combatManager.characters)
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
    }

    public enum HeatZone
    {
        OutofHeat,
        InHeat
    }*/

    public void SetHeatRate(int heat)
    {
        heatRate += heat;        
    }
}