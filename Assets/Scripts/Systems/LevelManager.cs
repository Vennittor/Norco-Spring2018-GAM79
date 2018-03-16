using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;

    public CombatManager combatManager;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LevelManager();
            }
            return _instance;
        }
    }

    void Start ()
    {
        combatManager = CombatManager.Instance;
	}
	

	void Update ()
    {
		
	}

    public void InitiateCombat(Party player, Party enemy)
    {
        Debug.Log("init Combat");
        combatManager.AddCharactersToCombat(player.partyMembers);
        combatManager.AddCharactersToCombat(enemy.partyMembers);

        combatManager.StartCombat();
    }
}
