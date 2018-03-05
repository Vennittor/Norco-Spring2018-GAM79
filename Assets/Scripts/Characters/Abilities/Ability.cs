using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ability : MonoBehaviour {
    // PERIPHERAL DATA
    public string abilityName;
    public string attackerName;
    public string targetName;
    public string output;

    public string description;
    //public Sprite image;

    // FUNCTIONAL DATA
    public float cooldown;
    public int minValue;
    public int maxValue;
    
    /*
    // This function is called when the scriptable object goes out of scope.
    // https://docs.unity3d.com/ScriptReference/ScriptableObject.html
    private void OnDisable() {
        // leaving scope, like destructor, should output when all data is set
        Announcer.UseSkill(attackerName, targetName, abilityName, output);
    }
    */
}
