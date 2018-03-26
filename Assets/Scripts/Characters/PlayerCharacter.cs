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
            //Get Targets - currently waits on input from UIManager for ability calls
        }
    }

    protected override void ChooseAbility()
    {
		//waits for UI to select an Ability, and the call Use AbilityOne,Two,Three or UseWater
		combatManager.uiManager.SetMode_Select ();
    }

    //call to water class   
    public void ReadyUseWater()
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

	public void UseWater()
	{
		water.StartAbility ();
	}

    public override void AbilityComplete(CombatState newState = CombatState.ABLE)
	{	
		//UIManager.BlockAbilitySelection();
		combatState = newState;
        EndTurn();
    }
}