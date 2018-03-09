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
			OutputAttackOne (); 
		}
		if (Input.GetKeyDown (KeyCode.Alpha2))
		{
			OutputAttackTwo ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha3))
		{
			OutputAttackThree ();
		}
	}

    public void OutputAttackOne()
    {
		(combatManager.activeCharacter as PlayerCharacter).SkillOne();
    }

	public void OutputAttackTwo()
    {
		(combatManager.activeCharacter as PlayerCharacter).SkillTwo();  
    }

	public void OutputAttackThree()
    {
		(combatManager.activeCharacter as PlayerCharacter).SkillThree(); 
    }
    #endregion
}
