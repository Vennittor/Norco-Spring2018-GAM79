using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Announcer {
    // Begin Turn
    public void BeginTurn(Character character) {
        Debug.Log("It's " + character.name + "'s turn!");
    }

    // Attack
    public void Attack(Character attacker, Character defender) {
        Debug.Log(attacker.name + " attacks " + defender.name + "!");
    }

    // Use Skill
    public void UseSkill(Character character, int skill) {
        Debug.Log(character.name + " used Skill #" + skill + "!");
    }

    // Use Item
    public void UseItem(Character character, string item) {
        Debug.Log(character.name + " used " + item + "!");
    }

    // Take Damage
    public void TakeDamage(Character defender, float damage) {
        Debug.Log(defender.name + " takes " + damage + " damage!");
    }

    // End Turn
    public void EndTurn(Character character) {
        Debug.Log("End of " + character.name + "'s turn!");
    }
}
