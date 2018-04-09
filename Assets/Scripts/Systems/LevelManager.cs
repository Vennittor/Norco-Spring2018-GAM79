using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _instance;

    public CombatManager combatManager;

	public GameObject combatUI;
    public Canvas levelUI;

    public GameObject playerParty;
    private Party pParty;
    private Party eParty;
    public uint partyHeatIntensity;

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

        if (playerParty == null)
        {
            playerParty = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Start()
    {
        combatManager = CombatManager.Instance;

		if (combatUI == null) 
		{
			combatUI = GameObject.Find ("Canvas Combat UI");
		}
		if (combatUI != null) 
		{
			combatUI.gameObject.SetActive (false);
		}
		else 
		{
			Debug.LogError ("LevelManager could not find reference to the Canvas Combat UI");
		}


    }


    void Update()
    {
    }

    public void GetHeat(uint heat)
    {
        partyHeatIntensity = heat;
    }

    public void InitiateCombat(Party player, Party enemy)
    {
        pParty = player;
        eParty = enemy;
        if(!combatManager.inCombat)
		{
            combatManager.AddCharactersToCombat(player.partyMembers);
            combatManager.AddCharactersToCombat(enemy.partyMembers);
            combatManager.HeatValueTaker(partyHeatIntensity);
            combatManager.StartCombat();

            //remove control from LevelManagement and Player.
            //TODO this is blunt force, change later to proper disable of control
            playerParty.GetComponent<Collider>().enabled = false;
            // potentially not mesh renderer when sprites are imported
            foreach (Character character in player.partyMembers)
            {
				if (character.GetComponent<MeshRenderer> () != null) 
				{
					character.GetComponent<MeshRenderer>().enabled = true;
				}

                character.GetComponent<Collider>().enabled = true;
            }
            foreach (Character character in enemy.partyMembers)
            {
				if (character.GetComponent<MeshRenderer> () != null) 
				{
					character.GetComponent<MeshRenderer>().enabled = true;
				}
                character.GetComponent<Collider>().enabled = true;
            }
			if (player.GetComponent<MeshRenderer> () != null)
			{
				player.GetComponent<MeshRenderer>().enabled = false;

			}
			if (enemy.GetComponent<MeshRenderer> () != null)
			{
				enemy.GetComponent<MeshRenderer>().enabled = false;
			}

            combatUI.SetActive(true);
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
        playerParty.gameObject.GetComponent<Collider>().enabled = true;

        foreach (Character character in pParty.partyMembers)
        {
            character.GetComponent<MeshRenderer>().enabled = false;
            character.GetComponent<Collider>().enabled = false;
        }
        foreach (Character character in eParty.partyMembers)
        {
            character.GetComponent<MeshRenderer>().enabled = false;
            character.GetComponent<Collider>().enabled = false;
        }
        pParty.GetComponent<MeshRenderer>().enabled = true;
        eParty.GetComponent<MeshRenderer>().enabled = true;

        combatUI.SetActive(false);
        
    }
}
