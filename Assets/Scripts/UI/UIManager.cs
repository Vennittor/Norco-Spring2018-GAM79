using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables
    public CombatManager combatManager;
    public PlayerCharacter playerCharacter;
    public EnemyCharacter enemyCharacter;
    #endregion   

    #region Functions
    void Start ()
    {
        combatManager = CombatManager.Instance;
    }
	
    public void OutputAttack_01()
    {      
            //playerCharacter.Attack1();  PlayerCharacter.cs needs Attack funtioncs set to public
            print("Ability 1 Used");
            //combatManager.NextTurn();      CombatManager.cs inherits from Monobehaviour and thus needs to exist on a GameObject(perhaps a GameManager (it is now))
    }

    public void OutputAttack_02()
    {        
            //playerCharacter.Attack2();  
            print("Ability 2 Used");
            //combatManager.NextTurn();        
    }

    public void OutputAttack_03()
    {       
            //playerCharacter.Attack3();  
            print("Ability 3 Used");
            //combatManager.NextTurn();       
    }
    #endregion
}
