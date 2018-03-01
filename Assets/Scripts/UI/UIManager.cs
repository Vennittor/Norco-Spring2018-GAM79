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
        (combatManager.activeCharacter as PlayerCharacter).Attack1();
        print("Ability 1 Used");
        combatManager.NextTurn();
    }

    public void OutputAttack_02()
    {
        (combatManager.activeCharacter as PlayerCharacter).Attack2();  
        print("Ability 2 Used");
        combatManager.NextTurn();        
    }

    public void OutputAttack_03()
    {
        (combatManager.activeCharacter as PlayerCharacter).Attack3();  
        print("Ability 3 Used");
        combatManager.NextTurn();       
    }
    #endregion
}
