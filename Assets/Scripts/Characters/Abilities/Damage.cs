using System.Collections;
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
			if (damageRange == null) 
			{
				Debug.LogError ("undeclared value in damageRanges");
				continue;
			}

			if (damageRange.x > damageRange.y) 
			{
				damageRange = new Vector2 (damageRange.y, damageRange.x);
			}

			totalDamage += Random.Range ((int)damageRange.x, (int)damageRange.y + 1);
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
				if (damageRange == null) 
				{
					Debug.LogError ("undeclared value in damageRanges");
					continue;
				}

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
			if (damageRange == null) 
			{
				Debug.LogError ("undeclared value in damageRanges");
				continue;
			}

			totalMaxDamage += (int)damageRange.y;
		}

		return totalMaxDamage;
	}


}
