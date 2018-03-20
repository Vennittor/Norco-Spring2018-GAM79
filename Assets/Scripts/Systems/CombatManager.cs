using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager combatInstance;
    private static Announcer announcer;

	public bool inCombat = false;

	[SerializeField] private List<Character> characters;

    public Character activeCharacter;
	public List<PlayerCharacter> activePlayers;
	public List<EnemyCharacter> activeEnemies;

    public List<Character> currentRoundCharacters;

	public uint roundCounter = 0;


    public static CombatManager Instance
    {
        get
		{
            return combatInstance;
        }
    }

	public List<Character> charactersInCombat 
	{
		get{ return characters; }

		set
		{
			characters.Clear ();
			characters.AddRange (value); 
		}
	}

    void Awake()
    {
        if (combatInstance != null && combatInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        combatInstance = this;
        Debug.Log("Awake: CombatManager created!");
        DontDestroyOnLoad(gameObject);
        
        Announcer.AnnounceSelf();
    }

    void Start()
    {
        characters = new List<Character>();
        currentRoundCharacters = new List<Character>();
        activePlayers = new List<PlayerCharacter>();
        activeEnemies = new List<EnemyCharacter>();
    }
		
	public void StartCombat()
	{
		if(!inCombat) 
		{
			inCombat = true;
		}
		else
		{
			return;
		}

		roundCounter = 0;
		if (characters.Count != 0)
		{
			StartRound ();
		}
		else
		{
			Debug.LogError("There are no Characters in the scene");
		}
	}

    public void AddCharactersToCombat(List<Character> charactersToAdd)
    {
        characters.AddRange(charactersToAdd);
    }

    void StartRound()							//Anything that needs to be handled at the start of the round should be placed in this function.
	{	Debug.Log ("New Round!");
		roundCounter++;
		//Check time left on status effects
		//Check cooldowns on Abilities
		SortRoundQueue();

		activeCharacter.BeginTurn ();
	}

	void EndRound()								//Anything that needs to be handled at the end of the round, should be placed in this function.
	{
        //Checks and adjustments to heat should be in seperate function (called here)
        //increase heat by set amount to characters (if combat is in a heat zone)
        //check if group is in a heat zone before entering combat (passed to here from game manager, not implemented yet)
        if (activeCharacter is PlayerCharacter)
        {
			if ((activeCharacter as PlayerCharacter).heatState == PlayerCharacter.HeatZone.INHEAT)
            {
                foreach (Character player in activePlayers)
                {
                    player.currentHeat += 10; //or whatever value gets settled on, just picked 10 for easy math //use heat level of current heat zone
                    Debug.Log(player.currentHeat + " < this is the current heat");
                }
            }
        }
        
		if ( !VictoryCheck () ) 
		{
			StartRound();
		}

	}

	public void NextTurn() // active player finishing their turn calls this
	{
		if (!VictoryCheck())
		{
			currentRoundCharacters.Remove(activeCharacter); // The activeCharacter is removed from the current round
			if (currentRoundCharacters.Count == 0) // if they were the last one to leave, then end the round
			{
				EndRound ();
			}
			else
			{
				activeCharacter = currentRoundCharacters[0];
				Debug.Log(activeCharacter.gameObject.name + " is next.");
				activeCharacter.BeginTurn ();
			}
		}
	}

	void EndCombat(bool playerVictory)
	{	Debug.Log ("End Combat");
		if (playerVictory == true) // party wins
		{
			Debug.Log ("Party Wins");

			inCombat = false;
            //Combat rewards?
            LevelManager.Instance.ReturnFromCombat();
		}
		else if (playerVictory == false) // party loses
		{
			Debug.Log ("Party Loses");

            inCombat = false;
            //Goto Defeat or Gameover GameState
            LevelManager.Instance.ReturnFromCombat();
		}
	}

	//This ends combat, cleanup, return level/field movement, and handling player victory/defeat should be performed or started here
	public bool VictoryCheck()
	{
		int ablePlayers = 0;
		foreach (Character player in characters)
		{
			if (player is PlayerCharacter && player.combatState != Character.CombatState.EXHAUSTED)
			{
				ablePlayers++;
			}
		}
		int ableEnemies = 0;
		foreach (Character enemy in characters)
		{
			if (enemy is EnemyCharacter && enemy.combatState != Character.CombatState.EXHAUSTED)
			{
				ableEnemies++;
			}
		}
		if (ablePlayers == 0 && ableEnemies == 0)
		{
			EndCombat(false);
			return true;
		}
		else if (ablePlayers == 0)
		{
			EndCombat(false);
			return true;
		}
		else if (ableEnemies == 0)
		{
			EndCombat(true);
			return true;
		}

		return false;
	}

    void SortRoundQueue() // clears round/active characters, repopulates round from actives, sorts round
    {
        activePlayers.Clear();
        foreach (Character character in characters)					//Populate activePlayers List
        {
            if (character is PlayerCharacter)
            {
				if (character.combatState == Character.CombatState.ABLE) // finds only active players
                {
                    activePlayers.Add(character as PlayerCharacter);
                }
            }
        }

        activeEnemies.Clear();
        foreach (Character character in characters)					//populate activeEnemies List
        {
            if (character is EnemyCharacter)
            {
				if (character.combatState == Character.CombatState.ABLE) // finds only active enemies
                {
                    activeEnemies.Add(character as EnemyCharacter);
                }
            }
        }

        currentRoundCharacters.Clear();
        foreach (PlayerCharacter character in activePlayers) 		// adds both previous lists to the round
        {
            currentRoundCharacters.Add(character as Character);
        }
        foreach (EnemyCharacter character in activeEnemies)
        {
            currentRoundCharacters.Add(character as Character);
        }

		currentRoundCharacters.Sort(SortBySpeed);					//Sort the currentRoundCharacterss characters by speed hi/lo
        activeCharacter = currentRoundCharacters[0];
    }

    private int SortBySpeed(Character c1, Character c2) // sorts by highest speed, player first
    {
        int char1 = c1.speed;
        int char2 = c2.speed;
        if (char1 == char2)
        {
            if (c1 is PlayerCharacter && c2 is EnemyCharacter)
            {
                return -1; // prioritizes player
            }
            else if (c1 is EnemyCharacter && c2 is PlayerCharacter)
            {
                return 1; // prioritizes player
            }
            else
            {
                return 1;
            }
        }
        return -char1.CompareTo(char2);
    }

}
