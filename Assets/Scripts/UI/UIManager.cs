using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables
    public CombatManager combatManager;
    public EventSystemManager eventSystemManager;
    public PlayerCharacter playerCharacter;
    public EnemyCharacter enemyCharacter;

    public delegate void MyDelegate();
    MyDelegate myDelegate;

    public float infoDelayTime = 0.5f;

    public enum ActiveState { NORMAL, TARGETING }
    public ActiveState state;
    #endregion


    #region Functions
    public void Start ()
    {
        combatManager = CombatManager.Instance;
    }

    public void Update()
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
        //Water use(Robert)
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            OutputWaterUse();
        }
    }

    public void OutputAttackOne()
    {
        if(state == ActiveState.NORMAL)
        {
            (combatManager.activeCharacter as PlayerCharacter).SkillOne();
            SetMode_Targeting();
        }
    }

	public void OutputAttackTwo()
    {
        if (state == ActiveState.NORMAL)
        {
            (combatManager.activeCharacter as PlayerCharacter).SkillTwo();
            SetMode_Targeting();
        }
    }

	public void OutputAttackThree()
    {
        if (state == ActiveState.NORMAL)
        {
            (combatManager.activeCharacter as PlayerCharacter).SkillThree();
            SetMode_Targeting();
        }
    }

    //water use (Robert)
    public void OutputWaterUse()
    {
        if (state == ActiveState.NORMAL)
        {
            (combatManager.activeCharacter as PlayerCharacter).SkillWater();
        }
    }


    public void SetMode_Normal() 
    {
        state = ActiveState.NORMAL; 
    }

    public void SetMode_Targeting()
    {
        state = ActiveState.TARGETING;
    }

    public void CallBack()
    {
        //remains to be determined
    }

    public void AssignTarget()
    {
        //eventSystemManager.target = 
    }

    #endregion
}
