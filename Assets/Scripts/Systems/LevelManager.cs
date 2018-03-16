using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _instance;

    public CombatManager combatManager;
    public Canvas combatUI;
    public Canvas levelUI;


    public GameObject playerParty;

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

        if(!combatManager.inCombat)
        {
            combatManager.AddCharactersToCombat(player.partyMembers);
            combatManager.AddCharactersToCombat(enemy.partyMembers);

            combatManager.StartCombat();

            //remove control from LevelManagement and Player.
            //TODO this is blunt force, change later to proper disable of control
            playerParty.gameObject.GetComponent<KeysToMove>().enabled = false;
            playerParty.gameObject.GetComponent<Collider>().enabled = false;
        }
        else
        {
            Debug.LogError("CombatManger is currently running combat, collision and Initiate Combat calls should no longer be called.");
        }
    }

    public void ReturnFromCombat()
    {
        //return control to Level and Player Party
        //TODO dirty blutn force change to better
        playerParty.gameObject.GetComponent<KeysToMove>().enabled = true;
        playerParty.gameObject.GetComponent<Collider>().enabled = true;

    }
}
