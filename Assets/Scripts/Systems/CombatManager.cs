using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {

    public enum ActiveState
    {
        ACTIVE, INACTIVE
    }

    public List<Character> characters;
    public Character activeCharacter;

    public static CombatManager combatInstance;
    public static CombatManager Instance
    {
        get
        {
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
        DontDestroyOnLoad(gameObject);
    }

    void Start ()
    {
		if (characters.Count != 0) 
		{
			QueueStart ();
		}
    }
   
	void Update ()
    {
		//TEST
		if (Input.GetKeyDown(KeyCode.Space))
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
            NextTurn();
        }
    }

    public void QueueStart()
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
