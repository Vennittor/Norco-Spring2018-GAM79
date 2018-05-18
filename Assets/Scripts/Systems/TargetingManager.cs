using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetingManager : MonoBehaviour
{
    public static TargetingManager targetingInstance;
    public UIManager uIManager;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))                //when left click is performed, set the abilites targets and use the ability, then go back into Ability Select
        {
            if (uIManager.inputMode == UIManager.InputMode.TARGETING)
            {
                if (uIManager.collectedTargets.Count > 0)
                {
                    uIManager.SendTargets(); //
                }
                else
                {
                    Debug.Log("No targets were collected, continuing to target");
                }
            }
        }
        if (Input.GetMouseButtonDown(1))  //Cycle targets
        {
            uIManager.CancelInput();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uIManager.CancelInput();
        }

        HighlightTargets();
    }

    public static TargetingManager Instance
    {
        get
        {
            return targetingInstance;
        }
    }

    public void TurnRed(List<Character> targets) 			// highlight in Red on Mouse-over
    {
        foreach (Character target in targets)
        {
            if (target.transform.GetComponentInChildren<SpriteRenderer>() != null)
            {
                target.transform.GetComponentInChildren<SpriteRenderer>().material.color = Color.red;
            }
        }
    }

    public void TurnWhite() // de-highlight red, return to white after not moused-over
    {
        foreach (Character character in uIManager.combatManager.charactersInCombat)
        {
            if (character.transform.GetComponentInChildren<SpriteRenderer>() != null)
            {
                character.transform.GetComponentInChildren<SpriteRenderer>().material.color = Color.white;
            }
        }
    }

    public void HighlightTargets()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Color debugColor = Color.blue;

        RaycastHit hitInfo;
        if (uIManager.inputMode == UIManager.InputMode.TARGETING && uIManager.searchingTargetType != null)
        {
            debugColor = Color.green;

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, uIManager.targetable))
            {
                
                if (hitInfo.collider.gameObject.GetComponent<Character>() == null || hitInfo.collider.gameObject.GetComponent<Character>().combatState == Character.CombatState.EXHAUSTED)          //if we did not hit a Character then the previousCharater becomes null, and we don't do anything
                {
                    uIManager.previousHitCharacter = null;
                    uIManager.collectedTargets.Clear();
                    TurnWhite();
                }
                else                                                                                            //else if we did, Start doing stuff
                {
                    Character hitCharacter = hitInfo.collider.gameObject.GetComponent<Character>();     //is the hitCharacter the previously hit Charater,  if not TurnWhite

                    if (hitCharacter != uIManager.previousHitCharacter)
                    {
                        uIManager.collectedTargets.Clear();
                        TurnWhite();
                        uIManager.previousHitCharacter = hitCharacter;
                    }                   

                    if (uIManager.searchingTargetType.who == TargetType.Who.SELF && hitCharacter == uIManager.combatManager.activeCharacter)    // if targeting SELF
                    {
                        if (!uIManager.collectedTargets.Contains(hitCharacter))
                        {
                            uIManager.collectedTargets.Add(hitCharacter);
                        }

                        TurnRed(uIManager.collectedTargets);
                        debugColor = Color.red;

                    }
                    else if (uIManager.searchingTargetType.who == TargetType.Who.ALLY && hitCharacter is PlayerCharacter)                 // if selecting ALLY
                    {
                        if (uIManager.searchingTargetType.formation == TargetType.Formation.SINGLE)                                       // target ALLY
                        {
                            if (!uIManager.collectedTargets.Contains(hitCharacter))
                            {
                                uIManager.collectedTargets.Add(hitCharacter);
                            }
                            TurnRed(uIManager.collectedTargets);
                        }
                        else if (uIManager.searchingTargetType.formation == TargetType.Formation.GROUP)                                   // target ALLIES
                        {
                            foreach (PlayerCharacter player in uIManager.combatManager.activePlayers)
                            {
                                if (!uIManager.collectedTargets.Contains(player))
                                {
                                    uIManager.collectedTargets.Add(player as Character);
                                }
                            }
                            TurnRed(uIManager.collectedTargets);
                        }
                    }
                    else if (uIManager.searchingTargetType.who == TargetType.Who.OPPONENT && hitCharacter is EnemyCharacter)          // if selecting OPPONENT
                    {
                        if (uIManager.searchingTargetType.formation == TargetType.Formation.SINGLE)                                       // target OPPONENT
                        {
                            if (!uIManager.collectedTargets.Contains(hitCharacter))
                            {
                                uIManager.collectedTargets.Add(hitCharacter);
                            }
                            TurnRed(uIManager.collectedTargets);
                        }
                        else if (uIManager.searchingTargetType.formation == TargetType.Formation.GROUP)                                   // target OPPONENTS
                        {
                            foreach (EnemyCharacter enemy in uIManager.combatManager.activeEnemies)
                            {
                                if (!uIManager.collectedTargets.Contains(enemy))
                                {
                                    uIManager.collectedTargets.Add(enemy as Character);
                                }
                            }
                            TurnRed(uIManager.collectedTargets);
                        }
                    }
                    else if (uIManager.searchingTargetType.who == TargetType.Who.EVERYONE && (hitCharacter is PlayerCharacter || hitCharacter is EnemyCharacter)) // if selecting EVERYONE (may be redundant)
                    {
                        foreach (PlayerCharacter player in uIManager.combatManager.activePlayers)
                        {
                            if (!uIManager.collectedTargets.Contains(player))
                            {
                                uIManager.collectedTargets.Add(player as Character);
                            }
                        }
                        foreach (EnemyCharacter enemy in uIManager.combatManager.activeEnemies)
                        {
                            if (!uIManager.collectedTargets.Contains(enemy))
                            {
                                uIManager.collectedTargets.Add(enemy as Character);
                            }
                        }
                        TurnRed(uIManager.collectedTargets);
                    }

                }

            }
            else
            {
                uIManager.collectedTargets.Clear();
                TurnWhite();
            }

        }

        Debug.DrawRay(ray.origin, ray.direction, debugColor);
    }
}
