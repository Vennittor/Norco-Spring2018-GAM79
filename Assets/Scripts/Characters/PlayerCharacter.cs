using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
	[SerializeField] private Water water;
    [SerializeField] private int waterUses = 3;
    public bool isLeader;

    private new void Awake()
    {
		base.Awake ();

        animator = gameObject.GetComponent<Animator>();
    }

    protected override void Start()
	{
        base.Start();

        if (baseStats == null)
        {
            Debug.LogError("No Base Stats on " + name);
        }
        else if (baseStats.leaderAbilities.Count == 0)
        {
            Debug.LogError("No LeaderAbilities on " + name + ". Assign them through the inspector in their BaseStats");
        }
        else if (baseStats.leaderAbilities.Count != 3)
        {
            Debug.LogWarning("There are " + baseStats.leaderAbilities.Count + " LeaderAbilities on " + name);
        }
    }

    private void UpdateAbilities()
    {
        if (isLeader)
        {
            abilities.Clear();
            abilities.AddRange(baseStats.leaderAbilities);
        }
        else
        {
            abilities.Clear();
            abilities.AddRange(baseStats.baseAbilities);
        }
    }

    public override void BeginTurn()
    {
        base.BeginTurn();

        UpdateAbilities();
        if (canAct)
        {
            combatManager.uiManager.UpdateAbilityButtons(abilities);
        }
    }

	protected override void ChooseAbility()					//tells UIManger to enter into an ability selection mode. It returns true if it switched modes.That script will then call ReadyAbility() based on the input given
	{	Debug.Log("PlayerChooseAbility");
		if (!combatManager.uiManager.AllowAbilitySelection ()) 
		{
			Debug.LogWarning ("UIManager cannot enter into AbilitySelect mode at the moment");
		}
    }

	public override void GetNewTargets()
	{
		ReadyAbility (selectedAbilityIndex);
	}

    //call to water class   
    public Ability ReadyUseWater()
    {
        Debug.Log("buttface");
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