using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public HeatZone heatState;
    public int heatIntensity;

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
		Debug.Log ("Player " + name + " begins thier turn.");
		if (combatState == CombatState.DISABLED || combatState == CombatState.EXHAUSTED) 
		{
			Debug.Log("Player " + name + " cannont act this turn");
			combatManager.NextTurn ();
		}
	}

	public void SkillOne()
	{
		combatState = CombatState.USEABILITY;

		//Announcer.UseSkill(name, RandEnemyTarget().name, "Skill #1", "It's over 9000!");
        if(abilities[1].Usable) { // if cooldown can start, do rest  of Ability
            abilities[1].ReadyAbility(name); // pass in Character name, gets target(s)
            abilities[1].UseAbility(); // uses ability on target(s)
        }

		combatState = CombatState.ABLE;
		combatManager.NextTurn();
	}
	public void SkillTwo()
	{
		combatState = CombatState.USEABILITY;

		Announcer.UseAbility(name, RandEnemyTarget().name, "Skill #2", "How do I turn this thing on?");
		combatState = CombatState.ABLE;
		combatManager.NextTurn();
	}
	public void SkillThree()
	{
		combatState = CombatState.USEABILITY;

		Announcer.UseAbility(name, RandEnemyTarget().name, "Skill #3", "I wish I had more Skills to use.");
		combatState = CombatState.ABLE;
		combatManager.NextTurn();
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
    }*/

    public enum HeatZone
    {
        OutofHeat,
        InHeat
    }

    public void SetHeatRate(int heat)
    {
        heatIntensity += heat;
    }
}