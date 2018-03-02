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
    /*
    // Basic Attack
    public static void BasicAttack(Character attacker, Character target) {
        Debug.Log(attacker.name + " attacks " + target.name + "!");
    }
    */
    // Use Skill
    public static void UseSkill(Character attacker, Character target, int skillNumber, string attackText) {
        Debug.Log(attacker.name + " used Skill #" + skillNumber + " on " + target.name + "!");
    }

    // Use Item
    public static void UseItem(Character character, string item) {
        Debug.Log(character.name + " used " + item + "!");
    }

    // Take Damage
    public static void TakeDamage(Character target, float damage) {
        Debug.Log(target.name + " takes " + damage + " damage!");
    }

    // End Turn
    public static void EndTurn(Character character) {
        Debug.Log("End of " + character.name + "'s turn!");
    }
}
