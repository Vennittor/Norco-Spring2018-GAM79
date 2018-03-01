using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Announcer {
    /*
    public Announcer() {
        Debug.Log("I hereby Announce that I have been created!");
    }
    */
    public static void AnnounceSelf() {
        Debug.Log("I hereby Announce that I have been statically compiled! I exist purely as an abstract utility class with no instance.");
    }

    // Begin Turn
    public static void BeginTurn(Character character) {
        Debug.Log("It's " + character.name + "'s turn!");
    }

    // Attack
    public static void Attack(Character attacker, Character defender) {
        Debug.Log(attacker.name + " attacks " + defender.name + "!");
    }

    // Use Skill
    public static void UseSkill(Character character, int skill) {
        Debug.Log(character.name + " used Skill #" + skill + "!");
    }

    // Use Item
    public static void UseItem(Character character, string item) {
        Debug.Log(character.name + " used " + item + "!");
    }

    // Take Damage
    public static void TakeDamage(Character defender, float damage) {
        Debug.Log(defender.name + " takes " + damage + " damage!");
    }

    // End Turn
    public static void EndTurn(Character character) {
        Debug.Log("End of " + character.name + "'s turn!");
    }
}
