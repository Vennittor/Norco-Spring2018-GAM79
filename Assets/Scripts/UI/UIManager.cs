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

    public float infoDelayTime = 0.5f;

    public enum ActiveState { ACTIVE, INACTIVE }//active = targeting
    public ActiveState state;
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
        if(state == ActiveState.ACTIVE)
        {
            (combatManager.activeCharacter as PlayerCharacter).SkillOne();
            SetInActive();
        }
    }

	public void OutputAttackTwo()
    {
        if (state == ActiveState.ACTIVE)
        {
            (combatManager.activeCharacter as PlayerCharacter).SkillTwo();
            SetInActive();
        }
    }

	public void OutputAttackThree()
    {
        if (state == ActiveState.ACTIVE)
        {
            (combatManager.activeCharacter as PlayerCharacter).SkillThree();
            SetInActive();
        }
    }

    public void SetActive() 
    {
        StartCoroutine(InfoDelay());
        state = ActiveState.ACTIVE; 
    }

    public void SetInActive()
    {
        state = ActiveState.INACTIVE;
    }

    public IEnumerator InfoDelay()
    {
        yield return new WaitForSeconds(infoDelayTime);
    }
    #endregion
}
