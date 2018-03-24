using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    [SerializeField]
    private int waterUses = 3;

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
            //Get Targets - currently waits on input from UIManager for ability calls
        }
    }

    protected override void ChooseAbility()
    {
        //UIManager.AllowAbilitySelection
    }

    //call to water class   
    public void UseWater()
    {
        if (waterUses > 0 & currentHeat > 0)
        {
            Announcer.UseItem(this.gameObject.name, "water");
            UseWater();

        }
        else if (waterUses == 0)
        {
            Debug.Log("you lean back for a swig, but only drink in disappointment");
        }
        else if (currentHeat == 0)
        {
            Debug.Log("your thirst does not require quenching at this time");
        }
    }

    public override void AbilityComplete(CombatState newState = CombatState.ABLE)
	{	
		//UIManager.BlockAbilitySelection();
		combatState = newState;
        EndTurn();
    }
}