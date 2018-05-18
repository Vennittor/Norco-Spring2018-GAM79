using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class Ability : ScriptableObject 
{
	// DESCRIPTIVE DATA
    public string abilityName;
    [SerializeField] protected string description;
    [SerializeField] protected string callOutText;

	// COMBAT DATA
	[SerializeField] private int heatCost = 0;  //TODO impement heat damage to user on Ability use.
	public TargetType targetType;
	[SerializeField] protected List<Damage> damage = new List<Damage>();
	[SerializeField] protected List<StatusEffectType> statuses = new List<StatusEffectType>();
    [SerializeField] private float statusChance;
    [SerializeField] private uint stunDuration;

	[SerializeField] protected uint numberOfActions = 1;			//When the Ability is done, instead of telling the player AbilityHasCompleted, it can accept another targeting input
	protected uint actionsUsed = 0;
	[SerializeField] protected uint hitsPerAction = 1;				//This is the number of times the Ability will hit each time it is used.  This it's only the assigned targets

	[SerializeField] protected uint _cooldown;

	public Character characterUser = null;

    protected List<Character> targets = new List<Character>();

    public AudioClip abilitySound;


    // EFFECTS DATA
    //public Sprite image; // for effects
    //Animators & Parameters

    

	/*public uint Cooldown 
	{
		get
		{
			return _cooldown;
		}
	}*/

	public string targetName 
	{
		get
		{
			if (targets.Count == 1)
			{
				return targets[0].gameObject.name;
			}
			else if (targets.Count > 1)
			{
                string targetString = "";
                foreach (Character character in targets)
                {
                    targetString += character.name + ", ";
                }
                return targetString;
            }
            else
			{
				return "Error: 0 targets!";
			}
		}
	}

	void OnEnable()
	{
		if (abilityName == "")
		{
			abilityName = this.name;
		}
	}

	public void PrepAbility(Character user)
	{
		characterUser = user;
        Debug.Log (abilityName + "'s user is " + characterUser.gameObject.name);

		if (numberOfActions < 1) 
		{
			numberOfActions = 1;
		}
		if (hitsPerAction < 1) 
		{
			hitsPerAction = 1;
		}
			
	}
		

	public void SetTargets(List<Character> targetsToSet)	
	{
		targets.Clear ();
		targets.AddRange(targetsToSet);
	}
	public void SetTargets(Character targetToSet)			//Overload method for SetTargets to accept only a single Character instead of needing a list
	{
		targets.Clear ();
		targets.Add (targetToSet);
	}

    public void StartAbility()
    {
		//TODO Ready an special effects that may happen when a Character is preparing to use the Ability
    }

	public void UseAbility()
	{
        float randAccuracy;
        if (targets.Count == 0)
		{
			Debug.LogWarning ("There are no targets for Abiility " + name);
		} 
		else 
		{
			if (characterUser.animator != null) 
			{
				characterUser.animator.SetTrigger ("Strike");		//Tells animator to go into the Strike animation
				characterUser.animator.SetBool ("Ready", false);	//When the Strike animation goes to REcover, with "Ready" being false, it should transition back to Idle
				characterUser.animator.SetBool("Idle", true);
			}
			else 
			{
				Debug.LogWarning ("Ability " + this.abilityName + " on " + characterUser.gameObject.name + " is trying to reference " 
					+ characterUser.gameObject.name + "'s animator within UseAbility(), and " + characterUser.gameObject.name + " does not have reference to an Animator.");
			}

            AnnounceAbility();
            SoundCaller();
            for (int hitsDone = 0; hitsDone < hitsPerAction; hitsDone++)
            {
                foreach (Character target in targets)                   // Target all applicable targets
                {
                    if(target.evade >= characterUser.accuracy)
                    {
                        Debug.Log("you suck");
                    }
                    else
                    {
                        randAccuracy = Random.Range(0, 100);
                        if (randAccuracy <= characterUser.accuracy - target.evade)
                        {
                            foreach (Damage range in damage)                    // Deal all types of Damage in List
                            {
                                target.ApplyDamage((uint)range.RollDamage(), range.element);
                            }

					foreach (StatusEffectType status in statuses)					// Apply all Status affects
					{
                        float rand = Random.Range(0f, 100f);
                        if (rand < statusChance)
                        {
                            target.ApplyStatus(status, stunDuration);
                        }
                        else
                        {
                            Debug.Log("Status missed");
                        }
                    } 
				}
                        else
                        {
                            Debug.Log("they could dodge a wrench");
                        }
                    }
                }
                //TODO Wait Between hits
            }
		}

		actionsUsed++;
		EndAbility ();
    }

	protected void EndAbility()
	{
		if (actionsUsed < numberOfActions)
		{
			targets.Clear ();
			StartAbility ();
			characterUser.GetNewTargets ();
		}
		else
		{
			targets.Clear ();

			if (characterUser.animator != null)
			{
				characterUser.animator.SetBool ("Idle", true);
				characterUser.animator.SetBool ("Ready", false);
			}
			else
			{
				Debug.LogWarning ("Ability " + this.abilityName + " on " + characterUser.gameObject.name + " is trying to reference " 
					+ characterUser.gameObject.name + "'s animator within EndAbility(), and " + characterUser.gameObject.name + " does not have reference to an Animator.");
			}
				
			actionsUsed = 0;
            ApplyAbilityHeatCost(); //applies the heat cost of the ability(positive or negative)
            characterUser.AbilityHasCompleted();
			characterUser = null;
		}
	}

    public void AnnounceAbility() 
	{
        //TODO This will need more logic to determine which Announcer message to call based on the properties of the Ability
		Announcer.UseAbility(characterUser.gameObject.name, targetName, abilityName, callOutText);
    }

    public void ApplyAbilityHeatCost()
    {
        Debug.Log(characterUser.name + " has aquired " + heatCost + " after using their ability");
        //characterUser.currentHeat += (uint)Mathf.Clamp(heatCost, 0, (characterUser.maxHeat - characterUser.currentHeat));
        if(heatCost > 0)
        {
            characterUser.DealHeatDamage(heatCost);  //uses dealheatdamage to apply the heat cost of the ability, while still being clamped
        }
        else if(heatCost < 0)
        {
            characterUser.ReduceHeat((uint)heatCost);  //for an ability that can "cool off" the player (reduce their heat level)
        }
    }

    public void SoundCaller()
    {
        SoundManager.instance.Play(abilitySound, "sfx");
    }
}