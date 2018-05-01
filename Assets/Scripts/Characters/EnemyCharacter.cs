using System.Collections;
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

	protected override void ChooseAbility()
	{
		//TODO establish a seperate AI that will handle Ability choice

		Ability abilityToUse = null;

		float selection = Random.Range(0, abilities.Count);
        Debug.LogError("Enemy using selection" + selection);
		selection = selection == (float)abilities.Count ? selection - 1f : selection;	// if the selecton is equal to 
        Debug.LogError("Selection 2 is " + selection);

		abilityToUse = ReadyAbility ((int)selection);		//Readies the selected Ability

		if (abilityToUse != null) 							//Then tell the AI to GetTargets for the Ability
		{
			this.GetTargets (abilityToUse);
		}
	}

	public override void GetNewTargets()
	{
		Ability abilityToUseAgain = ReadyAbility (selectedAbilityIndex);		//Ready the previsouly selected ability

		this.GetTargets (abilityToUseAgain);										//Then get Targets for it
	}


	#region AI Functions.		//TODO These FUnctions should be externalized for more robust and customizable AI's
	void GetTargets(Ability ability)
	{	Debug.Log ("Enemy GetTargets");
		combatManager.AssignTargets(RandPlayerTarget() as Character);
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
	#endregion
}
