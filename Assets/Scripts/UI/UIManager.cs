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

	public List<Button> skillButtons = new List<Button> ();

    public LayerMask targetable;
	public List<Character> collectedTargets;
    [SerializeField] private Ability ability;

    public delegate void MyDelegate();
    MyDelegate myDelegate;

    public float infoDelayTime = 0.5f;

    public enum InputMode { NORMAL, ABILITYSELECT, TARGETING, BLOCKED }
    public InputMode inputMode;
    #endregion


	public Character previousHitCharacter = null;
	TargetType searchingTargetType;

    #region Unity Functions
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
        collectedTargets = new List<Character>();

    }

    public void Update()
	{
		HighlightTargets ();
        //TEST Debug.Log ("collectedTargets.Count = " + collectedTargets.Count.ToString() );
        if (Input.GetMouseButtonDown (0)) 						//when left click is performed, set tat abilites targets dna use the ability, then go back into Ability Select
		{
            if (collectedTargets.Count > 0)
            {
                if (inputMode == InputMode.TARGETING)
			    {
				    SendTargets ();
			    }
            }
			else
            {
                Debug.Log("No targets were collected, continuing to target");
            }
		}
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Hello");
        }
    }
	#endregion

	public void UpdateAbilityButtons(List<Ability> activeAbilities)
	{
		for (int i = 0; i < activeAbilities.Count; i++) 
		{
			if (i >= skillButtons.Count) 
			{
				//Disable buttons visual
			}
			else 
			{
				Text buttonText;
				buttonText = skillButtons [i].gameObject.GetComponentInChildren<Text> ();

				buttonText.text = activeAbilities [i].abilityName;
			}
		}
	}

	public void OutputAttack(int abilityIndex) 					//This should be called by a button or other user input.  the index of the Ability to be called in the related Character class should be used
	{
		if(inputMode == InputMode.ABILITYSELECT)				//
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

	#region ModeSwitches
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

	public bool BlockInput()
	{
		SetMode_Blocked ();

		return true;
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
	#endregion

	public bool GetTargets(TargetType targetType)
	{
		if (inputMode != InputMode.BLOCKED)
		{
			SetMode_Targeting ();

			searchingTargetType = targetType;

			return true;
		}
		else
		{
			return false;
		}
	}

	public void SendTargets()        						//Assign Targets back to activeCharacter.
	{
		combatManager.AssignTargets(collectedTargets);

		TurnWhite ();

		collectedTargets.Clear ();

		searchingTargetType = null;
    }


	#region HighlightTargets
    public void TurnRed(List<Character> targets) 			// highlight in Red on Mouse-over
    {
        foreach(Character target in targets)
        {
			if (target.transform.GetComponentInChildren<SpriteRenderer> () != null) 
			{
				target.transform.GetComponentInChildren<SpriteRenderer> ().material.color = Color.red;
			}
        }
    }

    public void TurnWhite() // de-highlight red, return to white after not moused-over
    {
        foreach (Character character in combatManager.charactersInCombat)
        {
			if (character.transform.GetComponentInChildren<SpriteRenderer> () != null) 
			{
				character.transform.GetComponentInChildren<SpriteRenderer> ().material.color = Color.white;
			}
        }
    }
		
	void HighlightTargets()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		Color debugColor = Color.blue;

		RaycastHit hitInfo;
		if (inputMode == InputMode.TARGETING && searchingTargetType != null)
		{
			debugColor = Color.green;

			if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, targetable))
			{
                Debug.LogError(hitInfo.collider.gameObject.name);

                if (hitInfo.collider.gameObject.GetComponent<Character> () == null) 						//if we did not hit a Character then the previousCharater becomes null, and we don't do anything
				{
					previousHitCharacter = null;
                    collectedTargets.Clear();
                    TurnWhite();
				}
				else 																							//else if we did, Start doing stuff
				{
					Character hitCharacter = hitInfo.collider.gameObject.GetComponent<Character>();		//is the hitCharacter the previously hit Charater,  if not TurnWhite

					if (hitCharacter != previousHitCharacter) 
					{
						collectedTargets.Clear ();
						TurnWhite ();
						previousHitCharacter = hitCharacter;
					}

					if (searchingTargetType.who == TargetType.Who.SELF && hitCharacter == combatManager.activeCharacter) 	// if targeting SELF
					{
						if (!collectedTargets.Contains (hitCharacter)) 
						{
							collectedTargets.Add(hitCharacter);
						}

						TurnRed(collectedTargets);
						debugColor = Color.red;

					}
					else if (searchingTargetType.who == TargetType.Who.ALLY && hitCharacter is PlayerCharacter) 				// if selecting ALLY
					{	
						if (searchingTargetType.formation == TargetType.Formation.SINGLE) 										// target ALLY
						{
							if (!collectedTargets.Contains (hitCharacter)) 
							{
								collectedTargets.Add(hitCharacter);
							}
							TurnRed(collectedTargets);
						}
						else if(searchingTargetType.formation == TargetType.Formation.GROUP) 									// target ALLIES
						{
							foreach(PlayerCharacter player in combatManager.activePlayers)
							{
								if (!collectedTargets.Contains (player)) 
								{
									collectedTargets.Add(player as Character);
								}
							}
							TurnRed(collectedTargets);
						}
					}
					else if(searchingTargetType.who == TargetType.Who.OPPONENT && hitCharacter is EnemyCharacter) 			// if selecting OPPONENT
					{
						if (searchingTargetType.formation == TargetType.Formation.SINGLE) 										// target OPPONENT
						{
							if (!collectedTargets.Contains (hitCharacter)) 
							{
								collectedTargets.Add(hitCharacter);
							}
							TurnRed(collectedTargets);
						}
						else if (searchingTargetType.formation == TargetType.Formation.GROUP) 									// target OPPONENTS
						{
							foreach (EnemyCharacter enemy in combatManager.activeEnemies)
							{
								if (!collectedTargets.Contains (enemy)) 
								{
									collectedTargets.Add(enemy as Character);
								}
							}
							TurnRed(collectedTargets);
						}
					}
					else if( searchingTargetType.who == TargetType.Who.EVERYONE && (hitCharacter is PlayerCharacter || hitCharacter is EnemyCharacter) ) // if selecting EVERYONE (may be redundant)
					{
						foreach (PlayerCharacter player in combatManager.activePlayers)
						{
							if (!collectedTargets.Contains (player)) 
							{
								collectedTargets.Add(player as Character);
							}
						}
						foreach (EnemyCharacter enemy in combatManager.activeEnemies)
						{
							if (!collectedTargets.Contains (enemy)) 
							{
								collectedTargets.Add(enemy as Character);
							}
						}
						TurnRed(collectedTargets);
					}
						
				}

			}
			else
			{
				collectedTargets.Clear ();
				TurnWhite();
			}

		}

		Debug.DrawRay(ray.origin, ray.direction, debugColor);
	}
    #endregion
}
