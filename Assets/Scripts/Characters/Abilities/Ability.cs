using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class Ability : ScriptableObject {
    
	// DESCRIPTIVE DATA
    public string abilityName;
    public string description;
    //public Sprite image;

    // FUNCTIONAL DATA
	[SerializeField] private Vector2 damageRange = Vector2.zero;
	[SerializeField] private int cooldown = 0;
    [SerializeField] private int cooldownTime;

	//PERIPHIAL DATA
	public string attackerName;
	public string targetName;
	public string output;

    public void SetCooldown()
    {
        if (cooldown == 0)
        {
            cooldown = cooldownTime;
        }
    }
    public void Cooldown()
    {
        if (cooldown != 0)
        {
            cooldown--;
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
