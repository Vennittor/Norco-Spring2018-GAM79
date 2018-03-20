using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
	
    private new void Start()
    {
        base.Start();
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

		float selection = Random.Range(0, abilities.Count);

		if (selection <= 1)
		{
			SkillOne();
		}
		else if (selection > 1 && selection < 2)
		{
			SkillTwo();
		}
		else if (selection >= 2)
		{
			SkillThree();
		}
	}

	void GetTargets(TargetType targetType)
	{
		//TODO establish a seperate AI that will handle Target decisions

		RandPlayerTarget ();
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
