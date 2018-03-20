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

	public new void BeginTurn()
    {
		if (base.BeginTurn ()) 
		{
			ChooseAbility ();
		}
 
        
    }

	void ChooseAbility()
	{
		//TODO establish a seperate AI that will handle Ability choice

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

	void GetTargets(TargetType targetType)
	{
		//TODO establish a seperate AI that will handle Target decisions
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
