using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASyncTargeting : MonoBehaviour 
{
	public bool targetingEnabled = false;

	public Character enemyTarget = null;


	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

	public void AssignTarget(Character character)
	{
		if (character is EnemyCharacter)
		{
			enemyTarget = character;
		}
	}
}
