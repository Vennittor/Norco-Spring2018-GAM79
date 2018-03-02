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

    public int turnCounter;


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

        //announcer = new Announcer();
        Announcer.AnnounceSelf();
    }

    void Start()
    {
        characters = new List<Character>();
        currentRound = new List<Character>();
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
    }

    private int SortBySpeed(Character c1, Character c2)
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

    public void QueueSort()
    {
        foreach (Character character in characters)
        {
            if (character.combatState == Character.CombatState.ACTIVE)
            {
                currentRound.Add(character);
            }
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

}
