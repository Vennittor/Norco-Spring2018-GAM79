using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public HeatZone heatState;
    public int heatIntensity;

	//private Ability activeAbility = null;

    private new void Start()
    {
        base.Start();
        //heatState = HeatZone.OutofHeat;
    }

	public override void BeginTurn()
	{
		base.BeginTurn ();

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

	public override void AbilityComplete(CombatState newState = CombatState.ABLE)
	{	
		//UIManager.BlockAbilitySelection();
		combatState = newState;
        EndTurn();
    }
    
    /*public void EnterHeat()
    {
        heatState = HeatZone.InHeat;
    }

    public void ExitHeat()
    {
        heatState = HeatZone.OutofHeat;
    }*/

    public enum HeatZone
    {
        OUTOFHEAT,
        INHEAT
    }

    public void SetHeatRate(int heat)
    {
        heatIntensity += heat;
    }
}