using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {

    public static CombatManager combatInstance;
    private static Announcer announcer;

    public enum ActiveState
    {
        ACTIVE, INACTIVE
    }

    public List<Character> characters;
    public Character activeCharacter;


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

    void Start ()
    {
        characters = new List<Character>();
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
  
    }
   
	void Update ()
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
                return 1;
            }
            if (c1 is EnemyCharacter && c2 is PlayerCharacter)
            {
                return -1;
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
            (activeCharacter as EnemyCharacter).BeginEnemyTurn();
        }
    }

    public void QueueSort()
    {
        characters.Sort(SortBySpeed);
        activeCharacter = characters[0];
        Debug.Log(activeCharacter + "'s turn.");
        EnemyCheck();
    }

    public void NextTurn()
    {
        Character current = activeCharacter;
        characters.RemoveAt(0);
        characters.Insert(characters.Count, current);
        activeCharacter = characters[0];
        Debug.Log(activeCharacter + "'s turn.");
        EnemyCheck();
    }

}
