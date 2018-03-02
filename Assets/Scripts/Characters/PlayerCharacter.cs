using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{

    private new void Start()
    {
        base.Start();
    }

    void Update()
    {

    }

	public override void BeginTurn()
	{

	}

	public void Skill1()
	{
		//Announcer.UseSkill(this, RandEnemy(), 1, "Placeholder Attack message 1.");
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
}