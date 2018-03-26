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
    public InputMode inputMode;
    #endregion

	private void SetMode_Normal() 
	{
		inputMode = InputMode.NORMAL; 
	}

	private void SetMode_Select ()
	{
		inputMode = InputMode.ABILITYSELECT; 
	}

	private void SetMode_Targeting()
	{
		inputMode = InputMode.TARGETING;
	}

	private void SetMode_Blocked()
	{
		inputMode = InputMode.BLOCKED;
	}

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

		inputMode = InputMode.NORMAL;
    }

    public void Update()
	{

    }

	public void OutputAttack(int abilityIndex) 					//This should be called by a button or other user input.  the index of the Ability to be called in the related Character class should be used
    {
		if(inputMode == InputMode.ABILITYSELECT)
        {
			ability = (combatManager.activeCharacter as PlayerCharacter).ReadyAbility(abilityIndex);
            if(ability == null)
            {
                Debug.LogWarning("UIManager: OutputAttackOne(): activeCharacter has no AbilityOne");
            }
            else 
            {
				GetTargets (ability.targetType);
            }
        }
    }
		
    public void OutputWaterUse()
    {
		if (inputMode == InputMode.ABILITYSELECT)
        {
			if (combatManager.activeCharacter is PlayerCharacter) 
			{
				PlayerCharacter pChar = combatManager.activeCharacter as PlayerCharacter;
				pChar.UseWater ();
			}
        }
    }

	public bool AllowAbilitySelection()
	{
		if (inputMode != InputMode.BLOCKED)
		{
			SetMode_Select ();

			return true;
		}
		else
		{
			return false;
		}
	}

	public bool GetTargets(TargetType targetType)
	{
		if (inputMode != InputMode.BLOCKED)
		{
			SetMode_Targeting ();

			eventSystemManager.FindTargets (ability.targetType);

			return true;
		}
		else
		{
			return false;
		}
	}

	public bool BlockInput()
	{
		SetMode_Blocked ();

		return true;
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
