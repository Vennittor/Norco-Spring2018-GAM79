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

	public void AddCharactersToCombat(List<Character> charactersToAdd)
	{
		characters.AddRange (charactersToAdd);
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
        ////TEST
        //if (characters.Count == 0)
        //{
        //    Debug.LogWarning("characters List is empty, finding all Characters in scene");
        //    characters.AddRange(FindObjectsOfType<Character>());
        //}
        //TEST StartCombat() should be called by either the GameManger, an Event, or encountering an enemy.
        //StartCombat();
    }

    void Update()
    {
//        //TEST
//        if (Input.GetKeyDown(KeyCode.S))
//        {
//            activeCharacter.TakeDamage(4);
//        }
    }

	//
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


	void StartRound()							//Anything that needs to be handled at the start of the round should be placed in this function.
	{	Debug.Log ("New Round!");
		roundCounter++;
		//Check time left on status effects
		//Check cooldowns on Abilities
		SortRoundQueue();
	}

	void EndRound()								//Anything that needs to be handled at the end of the round, should be placed in this function.

	{
        //Checks and adjustments to heat should be in seperate function (called here)
        //increase heat by set amount to characters (if combat is in a heat zone)
        //check if group is in a heat zone before entering combat (passed to here from game manager, not implemented yet)
        if (activeCharacter is PlayerCharacter)
        {
            if ((activeCharacter as PlayerCharacter).heatState == PlayerCharacter.HeatZone.InHeat)
            {
                foreach (Character player in activePlayers)
                {
                    player.currentHeat += 10; //or whatever value gets settled on, just picked 10 for easy math //use heat level of current heat zone
                    Debug.Log(player.currentHeat + " < this is the current heat");
                }
            }
        }
        //TEST to keep things going until proper EndCombat checks are in place.
        StartRound();
	}

	//This ends combat, cleanup, return level/field movement, and handling player victory/defeat should be performed or started here
	void EndCombat(bool playerVictory)
	{
		if (playerVictory == true) // party wins
		{
			Debug.Log ("Party Wins");

			inCombat = false;
			//TODO return control to LevelManager
		}
		else if (playerVictory == false) // party loses
		{
			Debug.Log ("Party Loses");
			//Goto Defeat or Gameover GameState
		}
	}

    void EnemyCheck()
    {
        if (activeCharacter is EnemyCharacter)
        {
            (activeCharacter as EnemyCharacter).BeginTurn();
        }
    }

    void SortRoundQueue() // clears round/active characters, repopulates round from actives, sorts round
    {
        activePlayers.Clear();
        foreach (Character character in characters)
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
        foreach (Character character in characters)
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
        foreach (Character character in activePlayers) // adds both previous lists to the round
        {
            currentRoundCharacters.Add(character);
        }
        foreach (Character character in activeEnemies) // ^
        {
            currentRoundCharacters.Add(character);
        }
        //Sort the currentRoundCharacterss characters by speed hi/lo
        currentRoundCharacters.Sort(SortBySpeed);
        activeCharacter = currentRoundCharacters[0];

        EnemyCheck();
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
            if (c1 is EnemyCharacter && c2 is PlayerCharacter)
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

    public void NextTurn() // active player finishing their turn calls this
    {
		currentRoundCharacters.Remove(activeCharacter); // The activeCharacter removes themself from the current round
        if (currentRoundCharacters.Count == 0) // if they were the last one to leave
        {
			EndRound ();
        }
        else
        {	//The contents of this Coroutine should be moved back into this Function once it is no longer needed.
			StartCoroutine ("WaitForNextTurn");
        }
    }

	IEnumerator WaitForNextTurn()	//TEST  This is here to create a visible delay between turns.  It also prevents a lock when only enemies are acting in a round
	{
		yield return new WaitForSeconds(0.5f);

		activeCharacter = currentRoundCharacters[0];
		Debug.Log(activeCharacter + "'s turn.");
		EnemyCheck();
	}

    public void Enable(Character enabled)
    {
        if (!currentRoundCharacters.Contains(enabled))
        {
            if (enabled is PlayerCharacter)
            {
                activePlayers.Add(enabled as PlayerCharacter);
//                inactivePlayers.Remove(enabled as PlayerCharacter);
            }
            else if (enabled is EnemyCharacter)
            {
                activeEnemies.Add(enabled as EnemyCharacter);
//                inactiveEnemies.Remove(enabled as EnemyCharacter);
            }
        }
    }

    public void Disable(Character disabled) // remove character from active list, add to inactive
    {
//        if (currentRoundCharacters.Contains(disabled))
//        {
//            if (disabled != activeCharacter)
//            {
//                currentRoundCharacters.Remove(disabled);
//            }
//            if (disabled is PlayerCharacter)
//            {
//                inactivePlayers.Add(disabled as PlayerCharacter);
//                activePlayers.Remove(disabled as PlayerCharacter);
//            }
//            else if (disabled is EnemyCharacter)
//            {
//                inactiveEnemies.Add(disabled as EnemyCharacter);
//                activeEnemies.Remove(disabled as EnemyCharacter);
//            }
//        }
    }



}
