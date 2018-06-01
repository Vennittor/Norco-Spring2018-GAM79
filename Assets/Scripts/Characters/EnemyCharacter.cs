using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
	public MonoBehaviour externalAI = null;

    private new void Awake()
    {
		base.Awake ();
    }

    protected override void Start()
    {
        base.Start();

		if (baseStats.externalAI != null)
		{
			externalAI = baseStats.externalAI;
		}
    }

	protected override void ChooseAbility()
	{
		//TODO establish a seperate AI that will handle Ability choice

		Ability abilityToUse = null;

        float selection;
        if (characterName == "Croc")
        {
            selection = Random.Range(0, currentHealth <= maxHealth / 10 ? abilities.Count - 2 : abilities.Count);
        }
        if (characterName == "Amun-Ra")
        {
            selection = Random.Range(0, currentHealth <= maxHealth / 25 ? abilities.Count - 2 : abilities.Count);
        }
        else
        {
            selection = abilities.Count;
        }

        selection = selection >= (float)abilities.Count ? abilities.Count - 1f : selection;	// if the selecton is equal to 
        Debug.LogError("Enemy chooses ability" + selection);

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

    public void RemoveAbility(Ability bye)
    {
        abilities.Remove(bye);
    }


	#region AI Functions.		//TODO These FUnctions should be externalized for more robust and customizable AI's
	void GetTargets(Ability ability)
	{	Debug.Log ("Enemy GetTargets");
        if (ability.targetType.who == TargetType.Who.OPPONENT && ability.targetType.formation == TargetType.Formation.SINGLE)
        {
            combatManager.AssignTargets(combatManager.activeLeader, ability);
        }
        else if (ability.targetType.who == TargetType.Who.OPPONENT && ability.targetType.formation == TargetType.Formation.GROUP)
        {
            List<Character> characters = new List<Character>();
            foreach (Character character in combatManager.activePlayers)
            {
                if (character.combatState != CombatState.EXHAUSTED)
                {
                    characters.Add(character);
                }
            }
            combatManager.AssignTargets(characters, ability);
        }
        else if (ability.targetType.who == TargetType.Who.SELF)
        {
            combatManager.AssignTargets(this, ability);
        }
        else if (ability.targetType.who == TargetType.Who.ALLY && ability.targetType.formation == TargetType.Formation.SINGLE)
        {
            combatManager.AssignTargets(RandEnemyTarget(), ability);
        }
        else if (ability.targetType.who == TargetType.Who.ALLY && ability.targetType.formation == TargetType.Formation.GROUP)
        {
            List<Character> characters = new List<Character>();
            foreach (Character character in combatManager.activeEnemies)
            {
                if (character.combatState != CombatState.EXHAUSTED)
                {
                    characters.Add(character);
                }
            }
            combatManager.AssignTargets(characters, ability);
        }
        else if (ability.targetType.who == TargetType.Who.EVERYONE)
        {
            List<Character> characters = new List<Character>();
            foreach (Character character in combatManager.charactersInCombat)
            {
                if (character.combatState != CombatState.EXHAUSTED)
                {
                    characters.Add(character);
                }
            }
            combatManager.AssignTargets(characters, ability);
        }
    }

    private EnemyCharacter RandEnemyTarget()
    {
        List<EnemyCharacter> players = new List<EnemyCharacter>();
        foreach (Character character in combatManager.charactersInCombat)
        {
            if (character is EnemyCharacter)
            {
                players.Add(character as EnemyCharacter);
            }
        }
        EnemyCharacter enemyCharacter = players[Random.Range(0, players.Count)];
        return enemyCharacter;
    }
    #endregion
}
