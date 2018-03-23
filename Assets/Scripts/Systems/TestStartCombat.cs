using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStartCombat : MonoBehaviour
{
	public LevelManager levelManager;
	public CombatManager combatManager;

	public Party playerParty;
	public Party enemyParty;

	void Start () 
	{
		if (levelManager == null)
		{
			levelManager = GameObject.FindObjectOfType<LevelManager> ();
		}

		if (combatManager == null) 
		{
			combatManager = GameObject.FindObjectOfType<CombatManager> ();
		}
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			levelManager.InitiateCombat (playerParty, enemyParty);
		}
	}

}
