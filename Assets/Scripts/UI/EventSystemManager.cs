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

    private Character previousHitCharacter;

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
		
//	private void FixedUpdate()
//	{	Debug.Log ("!");
//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//
//        //Vector3 rayVector = ray.direction - ray.origin;
//
//        Color debugColor = Color.blue;
//
//        RaycastHit hitInfo;
//		if (uIManager.inputMode == UIManager.InputMode.TARGETING)
//		{	Debug.Log ("targeting");
//			
//            debugColor = Color.green;
//
//            if (Physics.Raycast(ray, out hitInfo))
//			{	Debug.Log (hitInfo.collider.gameObject.name);
//				if (hitInfo.transform.gameObject.GetComponent<Character> () == null) 						//if we did not hit a Character then the previousCharater becomes null, and we don't do anything
//				{
//					previousHitCharacter = null;
//				}
//				else 																							//else if we did, Start doing stuff
//				{
//					Character hitCharacter = hitInfo.transform.gameObject.GetComponent<Character>();			//is the hitCharacter the previously hit Charater,  if not TurnWhite
//					if (hitCharacter != previousHitCharacter || previousHitCharacter == null) 
//					{
//						uIManager.TurnWhite ();
//						hitCharacter = previousHitCharacter;
//					}
//
//					List<Character> outputs = new List<Character>();
//
//					if (targetType.who == TargetType.Who.SELF && hitCharacter == combatManager.activeCharacter) 	// if targeting SELF
//					{
//						outputs.Add(hitCharacter);
//						uIManager.TurnRed(outputs);
//						debugColor = Color.red;
//
//					}
//					else if (targetType.who == TargetType.Who.ALLY && hitCharacter is PlayerCharacter) 				// if selecting ALLY
//					{
//						if (targetType.formation == TargetType.Formation.SINGLE) 										// target ALLY
//						{
//							outputs.Add(hitCharacter);
//							uIManager.TurnRed(outputs);
//						}
//						else if(targetType.formation == TargetType.Formation.GROUP) 									// target ALLIES
//						{
//							foreach(PlayerCharacter player in combatManager.activePlayers)
//							{
//								outputs.Add(player as Character);
//							}
//							uIManager.TurnRed(outputs);
//						}
//					}
//					else if(targetType.who == TargetType.Who.OPPONENT && hitCharacter is EnemyCharacter) 			// if selecting OPPONENT
//					{
//						if (targetType.formation == TargetType.Formation.SINGLE) 										// target OPPONENT
//						{
//							outputs.Add(hitCharacter);
//							uIManager.TurnRed(outputs);
//						}
//						else if (targetType.formation == TargetType.Formation.GROUP) 									// target OPPONENTS
//						{
//							foreach (EnemyCharacter enemy in combatManager.activeEnemies)
//							{
//								outputs.Add(enemy as Character);
//							}
//							uIManager.TurnRed(outputs);
//						}
//					}
//					else if( targetType.who == TargetType.Who.EVERYONE && (hitCharacter is PlayerCharacter || hitCharacter is EnemyCharacter) ) // if selecting EVERYONE (may be redundant)
//					{
//						foreach (PlayerCharacter player in combatManager.activePlayers)
//						{
//							outputs.Add(player as Character);
//						}
//						foreach (EnemyCharacter enemy in combatManager.activeEnemies)
//						{
//							outputs.Add(enemy as Character);
//						}
//						uIManager.TurnRed(outputs);
//					}
//					Debug.Log ("end");
//					uIManager.AssignTargets ();		//Tells UIManager to Assign collected targets to ability and use it.
//				}
//
//            }
//            else
//            {
//                uIManager.TurnWhite();
//            }
//
//        }
//
//        Debug.DrawRay(ray.origin, ray.direction, debugColor);
//    }

    public void FindTargets(TargetType targetType)
	{
        this.targetType = targetType;
    }

    #endregion

}
