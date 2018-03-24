using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables
    private static UIManager uIInstance;
    public static UIManager Instance
    {
        get
        {
            if (uIInstance == null)
            {
                uIInstance = new UIManager();
            }
            return uIInstance;
        }
    }

    public CombatManager combatManager;
    public EventSystemManager eventSystemManager;
    public PlayerCharacter playerCharacter;
    public EnemyCharacter enemyCharacter;
    public List<Character> targets;
    [SerializeField] private Ability ability;

    public delegate void MyDelegate();
    MyDelegate myDelegate;

    public float infoDelayTime = 0.5f;

    public enum InputMode { NORMAL, ABILITYSELECT, TARGETING, BLOCKED }
    public InputMode inputState;
    #endregion


    #region Functions
    void Awake()
    {
        if (uIInstance != null && uIInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        uIInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Start ()
    {
        combatManager = CombatManager.Instance;
        eventSystemManager = EventSystemManager.Instance;
    }

    public void Update()
	{
		//RETARGET
        if (Input.GetMouseButtonDown(0))
        {
			if(inputState == InputMode.TARGETING)
            {
                if(targets.Count > 0)
                {
                    ability.SetTargets(targets);
                    ability.UseAbility();
					inputState = InputMode.ABILITYSELECT;
                    TurnWhite();
                }
            }
        }
    }

    public void OutputAttackOne() // Ability1 , Basic Attack
    {
		if(inputState == InputMode.ABILITYSELECT)
        {
			ability = (combatManager.activeCharacter as PlayerCharacter).AbilityOne();
            if(ability == null)
            {
                Debug.Log("UIManager: OutputAttackOne(): activeCharacter has no AbilityOne");
            }
            else // working
            {
                if (ability.Usable)
                {
                    SetMode_Targeting();
                    eventSystemManager.AcceptTargetType(ability.targetType);
                }

				else
				{
					Debug.Log ("Ability on cooldown");
				}
            }
        }
    }

	public void OutputAttackTwo() // Ability2, Skill1
    {
		if (inputState == InputMode.ABILITYSELECT)
        {
			ability = (combatManager.activeCharacter as PlayerCharacter).AbilityTwo();
            if (ability == null)
            {
				Debug.Log("UIManager: OutputAttackTwo(): activeCharacter has no AbilityTwo");
            }
            else // working
            {
                if (ability.Usable)
                {
                    SetMode_Targeting();
                    eventSystemManager.AcceptTargetType(ability.targetType);
                }
            }
        }
    }

	public void OutputAttackThree()
    {
		if (inputState == InputMode.ABILITYSELECT)
        {
			ability = (combatManager.activeCharacter as PlayerCharacter).AbilityThree();
            if (ability == null)
            {
				Debug.Log("UIManager: OutputAttackThree(): activeCharacter has no AbilityThree");
            }
            else // working
            {
                if (ability.Usable)
                {
                    SetMode_Targeting();
                    eventSystemManager.AcceptTargetType(ability.targetType);
                }
            }
        }
    }
		
    public void OutputWaterUse()
    {
		if (inputState == InputMode.ABILITYSELECT)
        {
			if (combatManager.activeCharacter is PlayerCharacter) 
			{
				PlayerCharacter pChar = combatManager.activeCharacter as PlayerCharacter;
				pChar.UseWater ();
			}
        }
    }


    public void SetMode_Normal() 
    {
		inputState = InputMode.NORMAL; 
    }

	public void SetMode_Select ()
	{
		inputState = InputMode.ABILITYSELECT; 
	}

    public void SetMode_Targeting()
    {
		inputState = InputMode.TARGETING;
    }

	public void SetMode_Blocked()
	{
		inputState = InputMode.BLOCKED;
	}

	public void GetTargets(TargetType targetType)
	{
		//Set top Targeting Mode

		//Go look for Targets in EventSystem
	}

    public void AssignTarget()
    {
        //Assign Targets back to activeCharacter.
    }

    public void TurnRed(List<Character> targets) // highlight in Red on Mouse-over
    {
        foreach(Character target in targets)
        {
            target.transform.GetComponentInChildren<SpriteRenderer>().material.color = Color.red;
            if (!this.targets.Contains(target))
            {
                this.targets.Add(target);
            }
        }

    }

    public void TurnWhite() // de-highlight red, return to white after not moused-over
    {
        foreach (Character character in combatManager.charactersInCombat)
        {
            character.transform.GetComponentInChildren<SpriteRenderer>().material.color = Color.white;
            targets.Clear();
        }
    }

    public void AcceptTargets(List<Character> targets)
    {
        this.targets = targets;
        SetMode_Targeting();
    }
    #endregion
}
