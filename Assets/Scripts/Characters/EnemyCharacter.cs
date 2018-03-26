﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private new void Start()
    {
        base.Start();
    }

	public override void BeginTurn()
	{
		base.BeginTurn ();
		if (canAct) 
		{
			ChooseAbility();
		}
    }

	protected override void ChooseAbility()
	{
		//TODO establish a seperate AI that will handle Ability choice

		Ability abilityToUse = null;

		float selection = Random.Range(0, abilities.Count);

		if (selection <= 1)
		{
			abilityToUse = ReadyAbilityOne ();

			if (abilityToUse != null) 
			{
				GetTargets (abilityToUse);
			}
		}
		else if (selection > 1 && selection < 2)
		{
			ReadyAbilityTwo();
		}
		else if (selection >= 2)
		{
			ReadyAbilityThree();
		}
	}

	void GetTargets(Ability ability)
	{
		//TODO establish a seperate AI that will handle Target decisions
		ability.SetTarget(RandPlayerTarget() as Character);
		ability.UseAbility ();
	}

	public override void AbilityComplete(CombatState newState = CombatState.ABLE)
	{
		combatState = newState;
		EndTurn ();
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
