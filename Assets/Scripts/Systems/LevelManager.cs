using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _instance;

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

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    void Start()
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
