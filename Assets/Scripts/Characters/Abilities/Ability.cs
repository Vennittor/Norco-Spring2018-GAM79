using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class Ability : ScriptableObject 
{
	// DESCRIPTIVE DATA
    [SerializeField] protected string abilityName;
    [SerializeField] protected string description;
    [SerializeField] protected string callOutText;

	// COMBAT DATA
	public TargetType targetType;
	[SerializeField] protected List<Damage> damage = new List<Damage>();
	[SerializeField] protected List<Status> statuses;

	[SerializeField] private uint numberOfActions = 1;
	private uint actionsUsed = 0;

	[SerializeField] private uint cooldown;
	[SerializeField] private uint cooldownTimer = 0;

	public Character characterUser;

    protected List<Character> targets = new List<Character>();

    // EFFECTS DATA
    //public Sprite image; // for effects
    //Animators & Parameters

    

	public bool Usable 
	{
		get {
			if (cooldownTimer <= 0) 
			{
				return true;
			}
			return false;
		}
	}

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
                //string targetstring = targets[0].name + ", "; // first target name
                //for (int i = 1; i < targets.Count - 1; i++)
                //{
                //    targetstring += targets[i].name + ", "; // every target name between first and last
                //}
                //targetstring += ", and " + targets[targets.Count].name; // last target name
                //return targetstring; // example: "enemya, enemyb, and enemyc"

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

	public void EquipAbility(Character user)
	{
		characterUser = user;
	}

	private void StartCooldown() 
	{
		cooldownTimer = cooldown;
	}
	public void ProgressCooldown()
	{
		if (cooldownTimer > 0) {
			cooldownTimer--;
		}
	}

	public void SetTarget(Character targetToSet)
	{
		targets.Clear ();
		targets.Add (targetToSet);
	}

	public void SetTargets(List<Character> targetsToSet)	
	{
		targets = targetsToSet;
	}

    public void StartAbility()
    {
        if (Usable)
        {
            //enter ready animation
            // transfer control to UI or AI for targeting:
            // tell it the TargetType (single, multiple; allies, opponents, everyone)
            //THis is compared against the class of the caller (Player or Enemy) to determine who are allies and who are enemies
            // Perform target selection with UI targeting mode
            // Return target(s) info to teh caller Ability from UI Manager
            // *after this function* -> UseAbility(); // Ability resolves with target(s)
            // call back to character that used Ability, tell them Ability has been used.
        }
    }

    public void UseAbility()
    {
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
					+ characterUser.gameObject.name + "'s animator, which does not have reference to an Animator.");
			}

            AnnounceAbility();
	        foreach (Character target in targets)					// Target all applicable targets
	        { 
				foreach (Damage range in damage)					// Deal all types of Damage in List
	            { 
					target.ApplyDamage ( (uint)range.RollDamage(), range.element);
	            } 

	            foreach (Status status in statuses)					// Apply all Status affects
	            { 
	                target.ApplyStatus(status);						// pass all Status effects to target Character
	            } 
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
			//StartAbility ();
		}
		else
		{
			targets.Clear ();
			StartCooldown();
            (characterUser).AbilityComplete();
		}
	}

    public void AnnounceAbility() 
	{
        //TODO This will need more logic to determine which Announcer message to call based on the properties of the Ability
		Announcer.UseAbility(characterUser.gameObject.name, targetName, abilityName, callOutText);
    }

    
}
