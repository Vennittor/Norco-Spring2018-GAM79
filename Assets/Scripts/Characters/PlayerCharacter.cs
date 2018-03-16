using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public HeatZone heatState;
    public int heatIntensity;
    private UIManager uIManager;

	private Ability activeAbility = null;

    private new void Start()
    {
        base.Start();
        uIManager = UIManager.Instance;
        //heatState = HeatZone.OutofHeat;
        // TODO set up ability attachments
        foreach(Ability ability in abilities)
        {
            ability.EquipAbility(this);
        }
    }

    void Update()
    {

    }

	public override void BeginTurn()
	{
		Debug.Log ("Player " + name + " begins their turn.");
		if (combatState == CombatState.DISABLED || combatState == CombatState.EXHAUSTED) 
		{
			Debug.Log("Player " + name + " cannont act this turn");
			combatManager.NextTurn ();
		}
	}

    public void AbilityComplete()
    {
        combatState = CombatState.ABLE;
        EndTurn();
    }

    public Ability SkillOne() // Basic Attack
    {
        if (abilities[0].Usable)
        {           // if cooldown can start, do rest  of Ability
            combatState = CombatState.USEABILITY;

            // In Ability
            return abilities[0];
        }
        return null;
    }
	public Ability SkillTwo()
	{
        if (abilities[1].Usable)
        {           // if cooldown can start, do rest  of Ability
            combatState = CombatState.USEABILITY;

            // In Ability
            return abilities[1];
        }
        return null;
    }
	public Ability SkillThree()
	{
        if (abilities[2].Usable)
        {           // if cooldown can start, do rest  of Ability
            combatState = CombatState.USEABILITY;

            // In Ability
            return abilities[2];
        }
        return null;
    }
    //Use water (Robert)
    public void SkillWater()
    {
        combatState = CombatState.USEABILITY;
        Announcer.UseAbility(name, name, "water", "gotta hydrate my dude");
        
        combatState = CombatState.ABLE;
        combatManager.NextTurn();
    }

	private EnemyCharacter RandEnemyTarget()
    {
        List<EnemyCharacter> enemies = new List<EnemyCharacter>();
		foreach(Character character in combatManager.charactersInCombat)
        {
			if (character is EnemyCharacter) 
			{
				enemies.Add (character as EnemyCharacter);
			}
        }
        EnemyCharacter enemyCharacter = enemies[Random.Range(0, enemies.Count)];
        return enemyCharacter;
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
        OutofHeat,
        InHeat
    }

    public void SetHeatRate(int heat)
    {
        heatIntensity += heat;
    }
}