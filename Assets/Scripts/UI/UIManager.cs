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

    public enum InputMode { NORMAL, TARGETING }
    public InputMode state;
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
            if(state == InputMode.TARGETING)
            {
                if(targets.Count > 0)
                {
                    ability.SetTargets(targets);
                    ability.UseAbility();
                    state = InputMode.NORMAL;
                    TurnWhite();
                }
            }
        }
    }

    public void OutputAttackOne() // Ability1 , Basic Attack
    {
        if(state == InputMode.NORMAL)
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
        if (state == InputMode.NORMAL)
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
        if (state == InputMode.NORMAL)
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

    //water use (Robert)
    public void OutputWaterUse()
    {
        if (state == InputMode.NORMAL)
        {
            //(combatManager.activeCharacter as PlayerCharacter).SkillWater();
        }
    }


    public void SetMode_Normal() 
    {
        state = InputMode.NORMAL; 
    }

    public void SetMode_Targeting()
    {
        state = InputMode.TARGETING;
    }

    public void AssignTarget()
    {
        //eventSystemManager.target = 
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
