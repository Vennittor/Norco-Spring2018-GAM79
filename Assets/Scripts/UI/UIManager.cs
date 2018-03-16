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

    public enum ActiveState { NORMAL, TARGETING }
    public ActiveState state;
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
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("LMB");
            if(state == ActiveState.TARGETING)
            {
                Debug.Log("state targeting");
                //Debug.Log("targets.Count: " + targets.Count);
                if(targets.Count > 0)
                {
                    ability.SetTargets(targets);
                    ability.UseAbility();
                    // Call function in Ability that would accept target info and start using Ability
                }
            }
        }
    }

    public void OutputAttackOne() // Ability1 , Basic Attack
    {
        if(state == ActiveState.NORMAL)
        {
            ability = (combatManager.activeCharacter as PlayerCharacter).SkillOne();
            if(ability == null)
            {
                Debug.Log("UIManager: OutputAttackOne(): ERROR");
            }
            else // working
            {
                SetMode_Targeting();
                eventSystemManager.AcceptTargetType(ability.targetType);
            }
        }
    }

	public void OutputAttackTwo() // Ability2, Skill1
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
        foreach (Character character in combatManager.characters)
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
