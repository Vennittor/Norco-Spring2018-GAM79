using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    //public float[] attacks;
    private float min = 0;
    private float max = 3;
    

    private new void Start()
    {
        base.Start();
    }
	
	void Update ()
    {
        
	}

	public override void BeginTurn()
    {
		Debug.Log ("Enemy " + name + " begins their turn.");
        // status check
        if (combatState == CombatState.DISABLED || combatState == CombatState.EXHAUSTED)
        {
            Debug.Log("Enemy " + name + " cannot act this turn");
            combatManager.NextTurn();
        }
        else
        {
            float selection = Random.Range(min, max);
            if (selection <= 1)
            {
                AttackOne();
            }
            else if (selection > 1 && selection < 2)
            {
                AttackTwo();
            }
            else if (selection >= 2)
            {
                AttackThree();
            }
        }
    }

    void AttackOne()
    {
		Debug.Log(name + " used AttackOne");
        combatManager.NextTurn();
    }
    void AttackTwo()
    {
		Debug.Log(name + " used AttackTwo");
        combatManager.NextTurn();
    }
    void AttackThree()
    {
		Debug.Log(name + " used AttackThree");
        combatManager.NextTurn();
    }

    private PlayerCharacter RandPlayerTarget()
    {
        List<PlayerCharacter> players = new List<PlayerCharacter>();
        foreach (Character character in combatManager.charactersInCombat)
        {
            if (character is PlayerCharacter)
            {
                players.Add(character as PlayerCharacter);
            }
        }
        PlayerCharacter playerCharacter = players[Random.Range(0, players.Count)];
        return playerCharacter;
    }
}
