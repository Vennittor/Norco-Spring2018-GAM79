using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Announcer 
{
	public static UIManager combatUIManager;

	public static Text announcementDestination;

	// Begin Turn
	public static void BeginTurn(string characterName)
	{
		string compiledMessage = "It's " + characterName + "'s turn!";

		if (combatUIManager != null && combatUIManager.enabled) 
		{
			combatUIManager.SplashAnnouncement (compiledMessage, announcementDestination);
		}
		else 
		{
			Debug.Log(compiledMessage);
		}

	}

	// Use Skill
	public static void UseAbility(string attackerName, string targetName, string skillName, string attackText) 
	{
		Debug.Log(attackerName + " used " + skillName + " on " + targetName + "!");
		string compiledMessage = "Skill Text: " + attackText;

		if (combatUIManager != null && combatUIManager.enabled) 
		{
			combatUIManager.SplashAnnouncement (compiledMessage, announcementDestination);
		}
		else 
		{
			Debug.Log(compiledMessage);
		}
	}

	// Use Item
	public static void UseItem(string characterName, string itemName) 
	{
		string compiledMessage = characterName + " used " + itemName + "!";

		if (combatUIManager != null && combatUIManager.enabled) 
		{
			combatUIManager.SplashAnnouncement (compiledMessage, announcementDestination);
		}
		else 
		{
			Debug.Log(compiledMessage);
		}
	}

	// Take Damage
	public static void TakeDamage(string targetName, float damage) 
	{
		string compiledMessage = targetName + " takes " + damage + " damage!";

		if (combatUIManager != null && combatUIManager.enabled)
		{
			combatUIManager.SplashAnnouncement (compiledMessage, announcementDestination);
		}
		else 
		{
			Debug.Log(compiledMessage);
		}
	}

	// End Turn
	public static void EndTurn(string characterName) 
	{
		string compiledMessage = "End of " + characterName + "'s turn!";

		if (combatUIManager != null && combatUIManager.enabled) 
		{
			combatUIManager.SplashAnnouncement (compiledMessage, announcementDestination);
		}
		else 
		{
			Debug.Log(compiledMessage);
		}
	}
}