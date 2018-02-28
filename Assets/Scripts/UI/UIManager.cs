using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables
    public TurnQueue turnQueue;
    public PlayerCharacter playerCharacter;
    public EnemyCharacter enemyCharacter;
    #endregion   

    #region Functions
    void Start ()
    {
        turnQueue = FindObjectOfType<TurnQueue>();
    }
	
    public void OutputAttack_01()
    {      
            //playerCharacter.Attack1();  PlayerCharacter.cs needs Attack funtioncs set to public
            print("Ability 1 Used");
            //turnQueue.NextTurn();      TurnQueue.cs inherits from Monobehaviour and thus needs to exist on a GameObject(perhaps a GameManager)
    }

    public void OutputAttack_02()
    {        
            //playerCharacter.Attack2();  
            print("Ability 2 Used");
            //turnQueue.NextTurn();        
    }

    public void OutputAttack_03()
    {       
            //playerCharacter.Attack3();  
            print("Ability 3 Used");
            //turnQueue.NextTurn();       
    }
    #endregion
}
