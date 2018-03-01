using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    
	void Start ()
    {
        
	}
	
	void Update ()
    {

    }

    public void BeginPlayerTurn()
    {
        if (Input.GetKeyDown("1")) //and check for if its your turn
        {
            Attack1();
        }
        else if (Input.GetKeyDown("2"))
        {
            Attack2();
        }
        if (Input.GetKeyDown("3"))
        {
            Attack3();
        }
    }

    public void Attack1()
    {
        Debug.Log("player used 1-key attack");
        combatManager.NextTurn();
    }
    public void Attack2()
    {
        Debug.Log("player used 2-key attack");
        combatManager.NextTurn();
    }
    public void Attack3()
    {
        Debug.Log("player used 3-key attack");
        combatManager.NextTurn();
    }
}
