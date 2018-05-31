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
				uIInstance = new UIManager();	Debug.Log ("new UIManager created");
            }
            return uIInstance;
        }
    }

    public CombatManager combatManager;
    public EventSystemManager eventSystemManager;
    //private Character character;

	public bool disableUIOnStart = true;

	public GameObject splashMessagePanel;
	public Text splashMessageText;
	public float splashLifeTime = 1.0f;

	public GameObject actionSlider;
    //public Image healthBar;

	public List<Button> skillButtons = new List<Button> ();

    public LayerMask targetable;
	public List<Character> collectedTargets;
    [SerializeField] private Ability ability;

    public float infoDelayTime = 0.5f;

    public enum InputMode { NORMAL, ABILITYSELECT, TARGETING, BLOCKED }
    public InputMode inputMode;
    #endregion


	public Character previousHitCharacter = null;
	TargetType searchingTargetType;

    #region Unity Functions
	public void Awake()
    {
        if (uIInstance != null && uIInstance != this)
        {
			//Debug.LogError ("more than one UIManager in scene.  Removing this one");
            Destroy(gameObject);
            return;
        }

        uIInstance = this;

		this.gameObject.transform.SetParent(null);

        DontDestroyOnLoad(gameObject);

		Debug.Log ("UIManager Awake");
    }

	public void Start()
	{
		SetMode_Normal ();
		collectedTargets = new List<Character>();

		Announcer.combatUIManager = UIManager.Instance;
		Announcer.announcementDestination = splashMessageText;
		combatManager = CombatManager.Instance;
		eventSystemManager = EventSystemManager.Instance;

		actionSlider = this.gameObject.transform.Find ("Action Slider").gameObject;

		if (disableUIOnStart)
		{
			this.gameObject.SetActive (false);
		}
	}

    public void Update()
	{
		//TODO TESTING		//TODO Change to inputMode may be coming from outside.
		if (Input.GetKeyDown (KeyCode.U))
		{
			Debug.Log ("input = " + inputMode.ToString ());
		}

		HighlightTargets ();
        
        if (Input.GetMouseButtonDown (0))                //when left click is performed, set tat abilites targets dna use the ability, then go back into Ability Select
		{            
            if (inputMode == InputMode.TARGETING)
            {
                if (collectedTargets.Count > 0)
                {
                    SendTargets();
                }
                else
                {
                    Debug.Log("No targets were collected, continuing to target");
                }
            }            
		}
		if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
        {
			CancelInput ();
        }
    }
	#endregion

	public void SplashAnnouncement(string splashMessage, Text destination)
	{
		splashMessagePanel.SetActive (true);

		destination.text = splashMessage;

		StartCoroutine ( DisplaySplash () );
	}

	public IEnumerator DisplaySplash()
	{
		yield return new WaitForSeconds (splashLifeTime);

		splashMessagePanel.SetActive (false);
	}

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

	public void InputAbility(int abilityIndex) 					//This should be called by a button or other user input.  the index of the Ability to be called in the related Character class should be used
	{
        
		if (inputMode == InputMode.ABILITYSELECT)
		{
			bool stopAbility = false;
			foreach(EffectClass effect in combatManager.activeCharacter.effectClassList)
			{
				if(effect.statusEffectType == StatusEffectType.Berserk) //TODO move out of this level
				{
					stopAbility = true;                
				}
			}

			if (!stopAbility)
			{	
				if (abilityIndex >= 0 && abilityIndex < combatManager.activeCharacter.abilityCount)
				{
					ability = (combatManager.activeCharacter as PlayerCharacter).ReadyAbility (abilityIndex);
                    if (combatManager.activeCharacter.name == "Crusader" && ability.abilityName == "Warrior Spirit")
                    {
                        combatManager.WarriorSwapLeader();
                    }

					if (ability == null)
					{
						Debug.LogWarning ("UIManager: OutputAttackOne(): activeCharacter has no AbilityOne");
					}
					else
					{
						GetTargets (ability.targetType);
					}
				}
				else
				{
					Debug.LogError ("InputAbility(): abilityIndex is out of range of activeCharacters Ability list");
				}
			}
			else
			{
                if (abilityIndex == 0 || abilityIndex == 3)
                {
                    ability = (combatManager.activeCharacter as PlayerCharacter).ReadyAbility(abilityIndex);
                    if (ability == null)
                    {
                        Debug.LogWarning("Mad cuz bad");
                    }
                    else
                    {
                        GetTargets(ability.targetType);
                    }
                }
                else
                {
                    Debug.LogWarning("Still mad, try another ability");
                }
			}
		}
    }

    public void InputLeader()
    {
        if (inputMode == InputMode.ABILITYSELECT)
        {
            combatManager.UpdateLeader();
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

	#region Internal Mode Switches
	private void SetMode_Normal() 
	{
		inputMode = InputMode.NORMAL;		Debug.Log ("input = " + inputMode.ToString ());
	}

	private void SetMode_Select ()
	{
		inputMode = InputMode.ABILITYSELECT; 		Debug.Log ("input = " + inputMode.ToString ());
	}

	private void SetMode_Targeting()
	{
		inputMode = InputMode.TARGETING;		Debug.Log ("input = " + inputMode.ToString ());
	}

	private void SetMode_Blocked()
	{
		inputMode = InputMode.BLOCKED;		Debug.Log ("input = " + inputMode.ToString ());
	}

	public bool BlockInput()
	{
		SetMode_Blocked ();

		return true;
	}
	#endregion

	#region Exposed Mode Switches
	public void ReturnToNormalMode()
	{
		SetMode_Normal ();
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
	public void CancelInput()
	{
		if (inputMode == InputMode.TARGETING) 
		{
			SetMode_Select ();
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
		combatManager.AssignTargets(collectedTargets, ability);

		TurnWhite ();

		collectedTargets.Clear ();

		searchingTargetType = null;

    }

  //  public void UpdateHealthBar(Character character)
  //  {
		//if (healthBar != null)
		//{
  //          // healthBar is a Dias, but doesn't seem to be attached to anyone or doing anything?
  //          healthBar.fillAmount = character.currentHealth / character.maxhealth;
  //      }
		//else
		//{
		//	Debug.LogError ("No refrence to Health Bar");
		//}
  //  }

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

				if (hitInfo.collider.gameObject.GetComponent<Character> () == null || hitInfo.collider.gameObject.GetComponent<Character>().combatState == Character.CombatState.EXHAUSTED) 			//if we did not hit a Character then the previousCharater becomes null, and we don't do anything
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
