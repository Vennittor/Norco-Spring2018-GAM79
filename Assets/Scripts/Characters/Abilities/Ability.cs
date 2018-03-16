using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class Ability : ScriptableObject {
    
	// DESCRIPTIVE DATA
    public string abilityName;
    public string attackText;
    public string description;
    //public Sprite image; // for effects

    // COMBATANT DATA
	public string attackerName;
	public string targetName {
        get {
            if(targets.Count == 1) {
                return targets[0].name;
            }
            else if(targets.Count > 1) {
                string targetString = targets[0].name + ", "; // first target name
                for(int i = 1; i < targets.Count - 1; i++) {
                    targetString += targets[i].name + ", "; // every target name between first and last
                }
                targetString += ", and " + targets[targets.Count]; // last target name
                return targetString; // Example: "EnemyA, EnemyB, and EnemyC"
            }
            else {
                return "Error: 0 targets!";
            }
        }
    }

    // FUNCTIONAL DATA
    [SerializeField] private int cooldown;
    [SerializeField] private int cooldownTimer = 0;

    //Water Related(Robert)
    [SerializeField] private int waterUses = 3;

    // targets (single, multiple, ally)
    [SerializeField] private List<Character> targets = new List<Character>();

    [SerializeField] private List<Vector2> damageRanges = new List<Vector2>();

    // damage types? Incorporate this into a damage type Class
    [SerializeField] private List<DamageType> damageTypes;

    // list types of effects (statuses)
    [SerializeField] private List<Status> statuses;

    public bool Usable {
        get {
            if (cooldownTimer == 0) {
                return true;
            }
        return false;
        }
    }

    public void ReadyAbility(string attackerName) {
        if(Usable) {
            this.attackerName = attackerName;

            // get all target(s) info - transfer control to UI:
                // Switch targeting mode (single, multiple, allies, enemies, all, etc.)
                // Perform target selection with mode
            // Return target(s) info to Ability
            // *after this function* -> UseAbility(); // Ability resolves with target(s)
        }
    }

    public void UseAbility() {
        foreach(Character target in targets) { // Target all applicable targets
            foreach(DamageType damage in damageTypes) { // Deal all types of Damage in List
                // target.TakeDamage() -- with damage
            } 
            foreach(Status status in statuses) { // Apply all Status affects
                target.ApplyStatus(status); // pass all Status effects to target Character
            } 
        }
        StartCooldown();
        AnnounceAbility(attackerName, targetName, abilityName, attackerName);
    }

    private void StartCooldown() {
        cooldownTimer = cooldown;
    }

    public void ProgressCooldown() {
        // Tandy: changed from cooldownTimer == 0 just in case of error if it's negative
        if (cooldownTimer > 0) {
            cooldownTimer--;
        }
    }

    public void AnnounceAbility(string attackerName, string targetName, string skillName, string attackText) {
        // Tandy: This just announces that it occurs (how much damage, and who it targets)
        Announcer.UseAbility(attackerName, targetName, skillName, attackText);
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
