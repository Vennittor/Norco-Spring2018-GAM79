﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Damage
{
	[SerializeField] private List<Vector2> damageRanges = new List<Vector2>();

	[SerializeField] private DamageType elementType = DamageType.PHYSICAL;

	public DamageType element
	{
		get 
		{
			return elementType;
		}
	}

	public int RollDamage()
	{
		int totalDamage = 0;

		foreach (Vector2 damageRange in damageRanges) 
		{
			if (damageRange.x > damageRange.y) 
			{
				totalDamage += Random.Range ((int)damageRange.y, (int)damageRange.x + 1);
			} 
			else 
			{
				totalDamage += Random.Range ((int)damageRange.x, (int)damageRange.y + 1);
			}
		}

		return totalDamage;
	}

	public int DamageMin
	{
		get
		{ 
			int totalMinDamage = 0;

			foreach (Vector2 damageRange in damageRanges) 
			{
				totalMinDamage += (int)damageRange.x;
			}

			return totalMinDamage;
		}

	}

	public int DamageMax()
	{
		int totalMaxDamage = 0;

		foreach (Vector2 damageRange in damageRanges) 
		{
			totalMaxDamage += (int)damageRange.y;
		}

		return totalMaxDamage;
	}


}