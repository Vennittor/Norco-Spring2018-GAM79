using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemManager : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
	#region Variables
	public EventSystem eventSystem;
    private static EventSystemManager eventInstance;
    public UIManager uIManager;
    public CombatManager combatManager;

    public Camera main;

    public TargetType targetType;

    private Character history;

    //public List<Character> targets; //tie in to combat system

    #endregion

    #region Functions
    public static EventSystemManager Instance
    {
        get
        {
            if (eventInstance == null)
            {
                eventInstance = new EventSystemManager();
            }
            return eventInstance;
        }
    }

    void Awake()
    {
        if (eventInstance != null && eventInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        eventInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        uIManager = UIManager.Instance;
        combatManager = CombatManager.Instance;
    }

    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Vector3 rayVector = ray.direction - ray.origin;

        Color debugColor = Color.blue;

        RaycastHit hitInfo;
        if (uIManager.state == UIManager.ActiveState.TARGETING)
        {
            debugColor = Color.green;

            if (Physics.Raycast(ray, out hitInfo))
            {
                Character input = hitInfo.transform.gameObject.GetComponent<Character>();
                if (input != history)
                {
                    uIManager.TurnWhite();
                }
                history = hitInfo.transform.gameObject.GetComponent<Character>();
                List<Character> outputs = new List<Character>();
                //Debug.Log(hitInfo.transform.gameObject.name);
                //Debug.Log("TargetType:" + targetType.who);
                if (input == combatManager.activeCharacter && targetType.who == TargetType.Who.SELF) // if targeting SELF
                {
                    outputs.Add(input);
                    uIManager.TurnRed(outputs); //target type, how many
                    debugColor = Color.red;

                }
                else if (input is PlayerCharacter && targetType.who == TargetType.Who.ALLY) // if selecting ALLY
                {
                    if (targetType.formation == TargetType.Formation.SINGLE) // target ALLY
                    {
                        outputs.Add(input);
                        uIManager.TurnRed(outputs); //target type, how many
                    }
                    else if(targetType.formation == TargetType.Formation.GROUP) // target ALLIES
                    {
                        //outputs = combatManager.activePlayers;
                        foreach(PlayerCharacter player in combatManager.activePlayers)
                        {
                            outputs.Add(player as Character);
                        }
                        uIManager.TurnRed(outputs); //target type, how many
                    }
                }
                else if(input is EnemyCharacter && targetType.who == TargetType.Who.OPPONENT) // if selecting OPPONENT
                {
                    if (targetType.formation == TargetType.Formation.SINGLE) // target OPPONENT
                    {
                        outputs.Add(input);
                        uIManager.TurnRed(outputs); //target type, how many
                    }
                    else if (targetType.formation == TargetType.Formation.GROUP) // target OPPONENTS
                    {
                        foreach (EnemyCharacter enemy in combatManager.activeEnemies)
                        {
                            outputs.Add(enemy as Character);
                        }
                        uIManager.TurnRed(outputs); //target type, how many
                    }
                }
                else if((input is PlayerCharacter || input is EnemyCharacter) && targetType.who == TargetType.Who.EVERYONE) // if selecting EVERYONE (may be redundant)
                {
                    foreach (PlayerCharacter player in combatManager.activePlayers)
                    {
                        outputs.Add(player as Character);
                    }
                    foreach (EnemyCharacter enemy in combatManager.activeEnemies)
                    {
                        outputs.Add(enemy as Character);
                    }
                    uIManager.TurnRed(outputs); //target type, how many
                }
            }

            else
            {
                uIManager.TurnWhite();
            }

        }

        Debug.DrawRay(ray.origin, ray.direction, debugColor);

    }

    public void AcceptTargetType(TargetType targetType)
    {
        this.targetType = targetType;
    }

    #endregion

}
