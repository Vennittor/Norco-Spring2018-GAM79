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

    public void ReadyAbility()
    {

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
        if (cooldownTimer != 0)
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
