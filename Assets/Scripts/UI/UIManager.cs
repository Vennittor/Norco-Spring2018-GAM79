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

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1))
		{
			OutputAttack_01 (); //Scott, James thinks this can be done in a different way 
		}
		if (Input.GetKeyDown (KeyCode.Alpha2))
		{
			OutputAttack_02 ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha3))
		{
			OutputAttack_03 ();
		}
	}
	
    public void OutputAttack_01()
    {
		(combatManager.activeCharacter as PlayerCharacter).Skill1();
    }

    public void OutputAttack_02()
    {
        (combatManager.activeCharacter as PlayerCharacter).Skill2();  
    }

    public void OutputAttack_03()
    {
		(combatManager.activeCharacter as PlayerCharacter).Skill3(); 
    }
		
    #endregion
}
