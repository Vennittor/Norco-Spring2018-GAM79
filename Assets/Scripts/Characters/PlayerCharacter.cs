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

    public void BeginPlayerTurn()
    {
        if (Input.GetKeyDown("1")) //and check for if its your turn
        {
            Skill1();
        }
        else if (Input.GetKeyDown("2"))
        {
            Skill2();
        }
        if (Input.GetKeyDown("3"))
        {
            Skill3();
        }
    }

    public void Skill1()
    {
        Announcer.UseSkill(this, RandEnemy(), 1, "Placeholder Attack message 1.");
        combatManager.NextTurn();
    }
    public void Skill2()
    {
        Announcer.UseSkill(this, RandEnemy(), 2, "Placeholder Attack message 2.");
        combatManager.NextTurn();
    }
    public void Skill3()
    {
        Announcer.UseSkill(this, RandEnemy(), 3, "Placeholder Attack message 3.");
        combatManager.NextTurn();
    }

    private EnemyCharacter RandEnemy()
    {
        List<EnemyCharacter> enemies = new List<EnemyCharacter>();
        foreach(EnemyCharacter enemy in combatManager.characters)
        {
            enemies.Add(enemy);
        }
        EnemyCharacter enemyCharacter = enemies[Random.Range(0, enemies.Count)];
        return enemyCharacter;
    }
}