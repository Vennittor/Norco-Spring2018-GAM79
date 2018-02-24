using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnQueue : MonoBehaviour {

    public List<Character> characters;

	void Start ()
    {
        foreach (Character character in characters)
        {
            character.lastTurn = 0;
        }
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
        int char1 = c1.speed + c1.lastTurn;
        int char2 = c2.speed + c2.lastTurn;
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

    public void QueueStart()
    {
        characters.Sort(SortBySpeed);
    }

    public void NextTurn()
    {
        characters[0].lastTurn = 0;
        characters.Sort(SortBySpeed);
        foreach (Character character in characters)
        {
            character.lastTurn += 1;
        }
        Debug.Log(characters[0] + "'s turn.");
        for (int i = 0; i < characters.Count; i++)
        {
            Debug.Log(characters[i] + "Combined: " + (characters[i].speed + characters[i].lastTurn) + ", Speed = " + characters[i].speed + ", lastTurn = " + characters[i].lastTurn);
        }
    }

}
