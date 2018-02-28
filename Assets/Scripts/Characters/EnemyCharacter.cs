using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    //public float[] attacks;
    private float min = 0;
    private float max = 3;
	
	void Start () {
		
	}
	
	void Update ()
    {
        //attacks = new float[2];
        
        //if its my turn, then call BeginEnemyTurn
        
	}

    public void BeginEnemyTurn()
    {
        float selection = Random.Range(min, max);
        if (selection <= 1)
        {
            Attack1();
        }
        else if (selection > 1 && selection < 2)
        {
            Attack2();
        }
        else if (selection >= 2)
        {
            Attack3();
        }
    }

    void Attack1()
    {
        Debug.Log("enemy used 1-key attack");
        //end turn
    }
    void Attack2()
    {
        Debug.Log("enemy used 2-key attack");
        //end turn
    }
    void Attack3()
    {
        Debug.Log("enemy used 3-key attack");
        //end turn
    }
}
