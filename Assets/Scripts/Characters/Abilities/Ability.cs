using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class Ability : ScriptableObject {
    
	// DESCRIPTIVE DATA
    [SerializeField] private string abilityName;
    [SerializeField] private string description;
    [SerializeField] private string attackText;
	public TargetType targetType;
    //public Sprite image; // for effects

    // COMBATANT DATA
    public Character characterUser;

    // FUNCTIONAL DATA
    [SerializeField] private int cooldown;
    [SerializeField] private int cooldownTimer = 0;

    //Water Related(Robert)
    [SerializeField] private int waterUses = 3;

    // targets (single, multiple, ally)
    [SerializeField] private List<Character> targets = new List<Character>();

	[SerializeField] private List<Damage> damage = new List<Damage>();

    // list types of effects (statuses)
    [SerializeField] private List<Status> statuses;

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

	public void ReadyAbility(EnemyCharacter enemyUser)
	{
		characterUser = enemyUser as Character;

		StartAbility ();
	}
	public void ReadyAbility(PlayerCharacter playerUser)
	{
		characterUser = playerUser as Character;

		StartAbility ();
	}

	void StartAbility()
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
				foreach (Damage range in damage)//TODO change to use new Damage class properties				// Deal all types of Damage in List
	            { 
					target.TakeDamage (range.RollDamage(), range.element);
	            } 

	            foreach (Status status in statuses)					// Apply all Status affects
	            { 
	                target.ApplyStatus(status);						// pass all Status effects to target Character
	            } 
	        }
		}
			
		AnnounceAbility();
    }

	void StopAbility()
	{
		//check against number of uses, if there are still uses left, go back to start ability.
		StartCooldown();
		//tell characterUser Ability is complete
	}

    public void AnnounceAbility() 
	{
        //TODO This will need more logic to determine which Announcer message to call based on the properties of the Ability
		Announcer.UseAbility(characterUser.name, targetName, abilityName, attackText);
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
