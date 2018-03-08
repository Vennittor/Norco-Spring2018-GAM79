using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Announcer {
	
	public static void AnnounceSelf() {
		Debug.Log("I hereby Announce that I have been statically compiled! \nI exist purely as an abstract utility class with no instance, only static functions.");
	}

	// Begin Turn
	public static void BeginTurn(string characterName) {
		Debug.Log("It's " + characterName + "'s turn!");
	}

	// Use Skill
	public static void UseSkill(string attackerName, string targetName, string skillName, string attackText) {
		Debug.Log(attackerName + " used " + skillName + " on " + targetName + "!");
		Debug.Log(attackText);
	}

	// Use Item
	public static void UseItem(string characterName, string itemName) {
		Debug.Log(characterName + " used " + itemName + "!");
	}

	// Take Damage
	public static void TakeDamage(string targetName, float damage) {
		Debug.Log(targetName + " takes " + damage + " damage!");
	}

	// End Turn
	public static void EndTurn(string characterName) {
		Debug.Log("End of " + characterName + "'s turn!");
	}
}