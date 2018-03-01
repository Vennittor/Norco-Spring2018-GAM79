using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Announcer : MonoBehaviour {
    // Attack
    public void Attack(Character Attacker, Character Defender) {
        Debug.Log(Attacker.name + " attacks " + Defender.name + "!");
    }

    // Take Damage
    public void TakeDamage(Character Defender, float damage) {
        Debug.Log(Defender.name + " takes " + damage + " damage!");
    }

    // Use Skill
    public void UseSkill(PlayerCharacter Player, int skill) {
        Debug.Log(Player.name + " used Skill #" + skill + "!");
    }

    // Use Item
    public void UseItem(PlayerCharacter Player, string item) {
        Debug.Log(Player.name + " used " + item + "!");
    }
}
