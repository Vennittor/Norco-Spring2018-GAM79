﻿using System.Collections;
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

	[SerializeField] protected uint numberOfActions = 1;			//When the Ability is done, instead of telling the player AbilityHasCompleted, it can accept another targeting input
	protected uint actionsUsed = 0;
	[SerializeField] protected uint hitsPerAction = 1;				//This is the number of times the Ability will hit each time it is used.  This it's only the assigned targets

	[SerializeField] protected uint cooldown;
	[SerializeField] protected uint cooldownTimer = 0;

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

		if (numberOfActions < 1) 
		{
			numberOfActions = 1;
		}
		if (hitsPerAction < 1) 
		{
			hitsPerAction = 1;
		}
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

	public void SetTargets(List<Character> targetsToSet)	
	{
		targets.Clear ();
		targets = targetsToSet;
	}
	public void SetTargets(Character targetToSet)			//Overload method for SetTargets to accept only a single Character instead of needing a list
	{
		targets.Clear ();
		targets.Add (targetToSet);
	}

    public void StartAbility()
    {
        if (Usable)
        {
			//Ready an special effects that may happen when a Character is preparing to use the Ability
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
					+ characterUser.gameObject.name + "'s animator within UseAbility(), and " + characterUser.gameObject.name + " does not have reference to an Animator.");
			}

            AnnounceAbility();

			for(int hitsDone = 0; hitsDone < hitsPerAction; hitsDone ++)
			{
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
			StartCooldown();

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

			characterUser.AbilityHasCompleted();
		}
	}

    public void AnnounceAbility() 
	{
        //TODO This will need more logic to determine which Announcer message to call based on the properties of the Ability
		Announcer.UseAbility(characterUser.gameObject.name, targetName, abilityName, callOutText);
    }

    
}
