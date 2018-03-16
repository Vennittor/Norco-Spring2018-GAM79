using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class Ability : ScriptableObject 
{
	// DESCRIPTIVE DATA
    [SerializeField] private string abilityName;
    [SerializeField] private string description;
    [SerializeField] private string callOutText;

	// COMBAT DATA
	public TargetType targetType;
	[SerializeField] private List<Damage> damage = new List<Damage>();
	[SerializeField] private List<Status> statuses;

	[SerializeField] private uint numberOfActions = 1;
	private uint actionsUsed = 0;

	[SerializeField] private uint cooldown;
	[SerializeField] private uint cooldownTimer = 0;

	public Character characterUser;
	[SerializeField] private List<Character> targets = new List<Character>();

	// EFFECTS DATA
	//public Sprite image; // for effects
	//Animators & Parameters

    //Water Related(Robert)
    [SerializeField] private int waterUses = 3;

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
				return targets[0].name;
			}
			else if (targets.Count > 1)
			{
				string targetString = targets[0].name + ", "; // first target name
				for (int i = 1; i < targets.Count - 1; i++)
				{
					targetString += targets[i].name + ", "; // every target name between first and last
				}
				targetString += ", and " + targets[targets.Count]; // last target name
				return targetString; // Example: "EnemyA, EnemyB, and EnemyC"
			}
			else
			{
				return "Error: 0 targets!";
			}
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
		targets = targetsToSet;
	}

	public void EquipAbility(EnemyCharacter enemyUser)		//TODO These need to be called from Character when they are intially set up.  They also register if the user is Player or Enemy
	{
		characterUser = enemyUser as Character;
	}
	public void EquipAbility(PlayerCharacter playerUser)
	{
		characterUser = playerUser as Character;
	}

	public void StartAbility()
	{
        if (Usable)
        {
			//enter ready animation
            // transfer control to UI for targeting:
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
			Debug.LogWarning ("There are no targets for Abiility " + this.name);
		} 
		else 
		{
	        foreach (Character target in targets)					// Target all applicable targets
	        { 
				foreach (Damage range in damage)					// Deal all types of Damage in List
	            { 
					target.TakeDamage ( (uint)range.RollDamage(), range.element);
	            } 

	            foreach (Status status in statuses)					// Apply all Status affects
	            { 
	                target.ApplyStatus(status);						// pass all Status effects to target Character
	            } 
	        }
		}

		AnnounceAbility();

		actionsUsed++;
		EndAbility ();
    }

	void EndAbility()
	{
		if (actionsUsed < numberOfActions)
		{
			StartAbility ();
		}
		else
		{
			StartCooldown();
			//tell characterUser Ability is complete
		}
	}

    public void AnnounceAbility() 
	{
        //TODO This will need more logic to determine which Announcer message to call based on the properties of the Ability
		Announcer.UseAbility(characterUser.name, targetName, abilityName, callOutText);
    }

    //for water ability(robert)
    public void UseWater()
    {
        if (waterUses > 0)
        {
            waterUses--;
        }
        else if (waterUses == 0)
        {
            Debug.Log("you have no water");
        }
    }
}
