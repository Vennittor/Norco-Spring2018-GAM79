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
        Announcer.UseSkill(this, RandEnemyTarget(), 1, "Placeholder Attack message 1.");
        combatManager.NextTurn();
    }
    public void Skill2()
    {
		Announcer.UseSkill(this, RandEnemyTarget(), 2, "Placeholder Attack message 2.");
        combatManager.NextTurn();
    }
    public void Skill3()
    {
		Announcer.UseSkill(this, RandEnemyTarget(), 3, "Placeholder Attack message 3.");
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
}