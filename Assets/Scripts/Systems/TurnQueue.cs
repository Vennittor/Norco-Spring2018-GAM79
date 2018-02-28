using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnQueue : MonoBehaviour {

    public List<Character> characters;
    public Character activeCharacter;

	void Start ()
    {
        QueueStart();
    }
   
	void Update ()
    {
		if (Input.GetButtonDown("Fire1"))
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
            Debug.Log(activeCharacter + " used random skill");
            NextTurn();
        }
    }

    public void QueueStart()
    {
        characters.Sort(SortBySpeed);
        activeCharacter = characters[0];
        Debug.Log(activeCharacter + "'s turn.");
        //EnemyCheck();
    }

    public void NextTurn()
    {
        Character current = activeCharacter;
        characters.RemoveAt(0);
        characters.Insert(characters.Count, current);
        activeCharacter = characters[0];
        Debug.Log(activeCharacter + "'s turn.");
        //EnemyCheck();
    }

}
