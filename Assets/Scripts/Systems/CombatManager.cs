using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager combatInstance;
	public UIManager uiManager;
    private static Announcer announcer;
    
    public GameObject levelManager;

    public AudioClip battleSong;

    public bool inCombat = false;

	[SerializeField] private List<Character> characters;

    public Character activeCharacter;
	public List<PlayerCharacter> activePlayers;
	public List<EnemyCharacter> activeEnemies;

    public List<Character> currentRoundCharacters;

	public uint roundCounter = 0;

    public uint partyHeatLevel;

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

		this.gameObject.transform.SetParent(null);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
		uiManager = UIManager.Instance;

        characters = new List<Character>();
        currentRoundCharacters = new List<Character>();
        activePlayers = new List<PlayerCharacter>();
        activeEnemies = new List<EnemyCharacter>();

        partyHeatLevel = 0;
    }
		
	public void StartCombat()
	{
		if(!inCombat) 
		{
			inCombat = true;
            SoundManager.instance.Play(battleSong, "mxC");
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
	{
        Debug.Log ("New Round!");
		roundCounter++;
		SortRoundQueue();

		activeCharacter.BeginTurn ();
	}

	void EndRound()								//Anything that needs to be handled at the end of the round, should be placed in this function.
	{
        //Checks and adjustments to heat should be in seperate function (called here)
        //increase heat by set amount to characters (if combat is in a heat zone)
        //check if group is in a heat zone before entering combat (passed to here from game manager, not implemented yet)
        //TODO set up so that checks heat state from level manager, which takes from party
        CombatHeatDealer();

		if ( !VictoryCheck () ) 
		{
			StartRound();
		}
	}
    
    public void HeatValueTaker(uint partyHeat)
    {
        partyHeatLevel = partyHeat;
    }

    void CombatHeatDealer()
    {
        Debug.LogError(partyHeatLevel);
        if (partyHeatLevel > 0)
        {
            foreach (PlayerCharacter player in activePlayers)
            {
                player.ApplyDamage(partyHeatLevel, ElementType.HEAT);
            }
            Debug.Log("player party took heat damage at end of round");
        }
    }

	public void NextTurn() // active player finishing their turn calls this
	{
		if (!VictoryCheck())
		{
            Debug.Log (activeCharacter.gameObject.name + " is removed from the queue");
			currentRoundCharacters.Remove(activeCharacter); // The activeCharacter is removed from the current round
            if (currentRoundCharacters.Count == 0) // if they were the last one to leave, then end the round
			{
				EndRound ();
			}
			else
			{	
				activeCharacter = currentRoundCharacters[0];
				Debug.Log (activeCharacter.gameObject.name + " is now the activeCharacter");
				//TEST
				StartCoroutine( "DelayNextTurn" );
			}
		}
	}

	//TEST This creates a visible delay between character turns.
	public IEnumerator DelayNextTurn()
	{
		Debug.Log(activeCharacter.gameObject.name + " is next.");

		yield return new WaitForSeconds (0.6f);

		activeCharacter.BeginTurn ();
	}

	void EndCombat(bool playerVictory)
	{
        Debug.Log ("End Combat");
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

	void SortRoundQueue() 									// clears round/active characters, repopulates round from actives, sorts round
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

    private int SortBySpeed(Character c1, Character c2) 			// sorts by highest speed, player first
    {
        float char1 = c1.speed;
        float char2 = c2.speed;
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


	#region Targeting and Ability Use
	public void AssignTargets(List<Character> targetsToAssign)
	{
		List<Character> finalizedTargets = new List<Character> ();
		//check if the character using the Ability (most likely activeCharacter) has any effect that would cause them to change targets, like StatusEffect confusion.
		//check if the intended targets have any re-direction effects, (like Cover, or Reflect)
		//Find new targets if needed.  This should be done within CombatManger and not UIManager

		finalizedTargets.AddRange (targetsToAssign);

		activeCharacter.UseAbility (finalizedTargets);
	}

	public void AssignTargets(Character targetToAssign)			//Overload to take in a single Character as opposed to a List
	{
		List<Character> target = new List<Character> ();

		target.Add (targetToAssign);

		AssignTargets (target);
	}

	//TODO
	//Function Get RandomTarget
	//Function Redirect Target

	#endregion

}
