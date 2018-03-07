using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    //public float[] attacks;
    private float min = 0;
    private float max = 3;
    

    private new void Start()
    {
        base.Start();
    }
	
	void Update ()
    {
        
	}

	public override void BeginTurn()
    {
		Debug.Log ("Enemy " + this.name + " begins thier turn.");

        float selection = Random.Range(min, max);
        if (selection <= 1)
        {
            AttackOne();
        }
        else if (selection > 1 && selection < 2)
        {
            AttackTwo();
        }
        else if (selection >= 2)
        {
            AttackThree();
        }
    }

    void AttackOne()
    {
        Debug.Log("enemy used 1-key attack");
        combatManager.NextTurn();
    }
    void AttackTwo()
    {
        Debug.Log("enemy used 2-key attack");
        combatManager.NextTurn();
    }
    void AttackThree()
    {
        Debug.Log("enemy used 3-key attack");
        combatManager.NextTurn();
    }
}
