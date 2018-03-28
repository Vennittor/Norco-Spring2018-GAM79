using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
	[SerializeField] private Water water;
    [SerializeField] private int waterUses = 3;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private new void Start()
    {
        base.Start();
    }

    public override void BeginTurn()
    {
        base.BeginTurn();

        if (canAct)
        {
            //TODO swap UI graphics into to match PlayerCharacter
            ChooseAbility();
        }
    }

	protected override void ChooseAbility()					//tells UIManger to enter into an ability selection mode. It returns true if it switched modes.That script will then call ReadyAbility() based on the input given
    {
		if (!combatManager.uiManager.AllowAbilitySelection ()) 
		{
			Debug.LogWarning ("UIManager cannot enter into AbilitySelect mode at the moment");
		}
    }

    //call to water class   
    public Ability ReadyUseWater()
    {
		if (water == null) 
		{
			Debug.Log ("There is no Water Ability for " + this.characterName);
			return null;
		}

        if (waterUses > 0 & currentHeat > 0)
        {
			return water;
        }
        else if (waterUses == 0)
        {
            Debug.Log("you lean back for a swig, but only drink in disappointment");
        }
        else if (currentHeat == 0)
        {
            Debug.Log("your thirst does not require quenching at this time");
        }
		return null;
    }

	public void UseWater()
	{
		Announcer.UseItem(this.gameObject.name, "water");

		water.StartAbility ();
	}
		
}