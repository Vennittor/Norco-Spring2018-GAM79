using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class Ability : ScriptableObject {
    
	// DESCRIPTIVE DATA
    public string abilityName;
    public string description;
    //public Sprite image; for effects

    // FUNCTIONAL DATA
    [SerializeField] private List<Vector2> damageRanges = new List<Vector2>();
    //[SerializeField] private List<Status> statuses;
	[SerializeField] private int cooldown;
    [SerializeField] private int cooldownTimer = 0;
    [SerializeField] private List<Character> targets = new List<Character>();

	//PERIPHIAL DATA
	public string attackerName;
	public string targetName;
	public string output;

    // List types of effects (statuses)
    // damage types? Incorporate this into a damage type Class
    // targets (single, multiple, ally)

    public void AnnounceAbility()
    {
        // Tandy: This just announces that it occurs (how much damage, and who it targets)
        /*
        When Character calls Ability to start, it enters USEABILITY State, and calls ReadyAbility() to initialize the Ability.
        Example of this in Character:
        if(state != USEABILITY) {
            character.state = USEABILITY; // now, state is set to USEABILITY, so that initialization only happens once
            ability.StartCooldown();
        }
       
        once Ability starts:
            ability.UseAbility();

        *later when Ability is done, Character state goes back to ABLE default state
        
        combatmanager does this:
            ProgressCooldown();
        */
    }

    public void UseAbility()
    {

    }

    public void StartCooldown()
    {
        if (cooldownTimer == 0)
        {
            cooldownTimer = cooldown;
        }
    }
    public void ProgressCooldown()
    {
        if (cooldownTimer > 0) // Tandy: changed from cooldownTimer == 0 just in case of error, if cooldownTimer is negative
        {
            cooldownTimer--;
        }
    }

    /*
    // This function is called when the scriptable object goes out of scope.
    // https://docs.unity3d.com/ScriptReference/ScriptableObject.html
    private void OnDisable() {
        // leaving scope, like destructor, should output when all data is set
        Announcer.UseSkill(attackerName, targetName, abilityName, output);
    }
    */
}
