using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager combatInstance;
    private static Announcer announcer;

    public enum ActiveState { ACTIVE, INACTIVE }

    public List<Character> characters;
    public Character activeCharacter;
    public List<Character> currentRound;
    public List<PlayerCharacter> activePlayers;
    public List<PlayerCharacter> inactivePlayers;
    public List<EnemyCharacter> activeEnemies;
    public List<EnemyCharacter> inactiveEnemies;

    public uint turnCounter;


    public static CombatManager Instance
    {
        get
        {
            if (combatInstance == null)
            {
                combatInstance = new CombatManager();
            }
            return combatInstance;
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
        currentRound = new List<Character>();
        activePlayers = new List<PlayerCharacter>();
        activeEnemies = new List<EnemyCharacter>();
        //TEST
        if (characters.Count == 0)
        {
            Debug.LogWarning("characters List is empty, finding all Characters in scene");
            characters.AddRange(FindObjectsOfType<Character>());
        }

        if (characters.Count != 0)
        {
            QueueSort();
        }
        else
        {
            Debug.LogWarning("There are no Characters in the scene");
        }
        turnCounter = 1;
    }

    void Update()
    {
        //TEST
        if (Input.GetButtonDown("Jump"))
        {
            NextTurn();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            activeCharacter.TakeDamage(4);
        }
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

    public void EnemyCheck()
    {
        if (activeCharacter is EnemyCharacter)
        {
            (activeCharacter as EnemyCharacter).BeginTurn();
        }
    }

    public void QueueSort() // clears round/active characters, repopulates round from actives, sorts round
    {
        activePlayers.Clear();
        foreach (Character character in characters)
        {
            if (character is PlayerCharacter)
            {
                if (character.combatState == Character.CombatState.ACTIVE) // finds only active players
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
                if (character.combatState == Character.CombatState.ACTIVE) // finds only active enemies
                {
                    activeEnemies.Add(character as EnemyCharacter);
                }
            }
            
        }
        currentRound.Clear();
        foreach (Character character in activePlayers) // adds both previous lists to the round
        {
            currentRound.Add(character);
        }
        foreach (Character character in activeEnemies) // ^
        {
            currentRound.Add(character);
        }
        
        currentRound.Sort(SortBySpeed);
        activeCharacter = currentRound[0];
        
        Debug.Log(activeCharacter + "'s turn.");
        EnemyCheck();
    }

    public void NextTurn() // active player finishing their turn calls this
    {
        currentRound.RemoveAt(0); // remove themself from the current round
        if (currentRound.Count == 0) // if they were the last one to leave
        {
            turnCounter++;
            QueueSort(); // sort again with new speeds in case of change
        }
        else
        {
            activeCharacter = currentRound[0];
            Debug.Log(activeCharacter + "'s turn.");
            EnemyCheck();
        }
    }

    public void Enable(Character enabled)
    {
        if (!currentRound.Contains(enabled))
        {
            if (enabled is PlayerCharacter)
            {
                activePlayers.Add(enabled as PlayerCharacter);
                inactivePlayers.Remove(enabled as PlayerCharacter);
            }
            else if (enabled is EnemyCharacter)
            {
                activeEnemies.Add(enabled as EnemyCharacter);
                inactiveEnemies.Remove(enabled as EnemyCharacter);
            }
        }
    }

    public void Disable(Character disabled) // remove character from active list, add to inactive
    {
        if (currentRound.Contains(disabled))
        {
            currentRound.Remove(disabled);
            if (disabled is PlayerCharacter)
            {
                inactivePlayers.Add(disabled as PlayerCharacter);
                activePlayers.Remove(disabled as PlayerCharacter);
            }
            else if (disabled is EnemyCharacter)
            {
                inactiveEnemies.Add(disabled as EnemyCharacter);
                activeEnemies.Remove(disabled as EnemyCharacter);
            }

            bool partyWipe = false; // check if last of either side is inactive to end combat
            if (activePlayers.Count == 0)
            {
                partyWipe = true;
            }
            if (partyWipe == true)
            {
                EndCombat(false);
            }

            if (partyWipe == false)
            {
                bool enemyWipe = true;
                if (activeEnemies.Count == 0)
                {
                    enemyWipe = false;
                }
                if (enemyWipe == true)
                {
                    EndCombat(true);
                }
            }
        }
    }

    public void EndCombat(bool playerVictory)
    {
        if (playerVictory == true) // party wins
        {

        }
        else if (playerVictory == false) // party loses
        {

        }
    }

}
