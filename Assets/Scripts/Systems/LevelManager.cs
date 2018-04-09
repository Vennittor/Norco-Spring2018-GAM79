using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _instance;

    public CombatManager combatManager;

	public GameObject combatUI;
	public GameObject levelUI;

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
			combatUI.SetActive (false);
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
            //remove control from LevelManagement and Player.

            foreach (Character character in player.partyMembers)					//Turn on the player Party members renderers and Colliders on
			{
				if (character.GetComponent<MeshRenderer> () != null) 
				{
					character.GetComponent<MeshRenderer>().enabled = true;
				}
				if (character.GetComponent<SpriteRenderer> () != null) 
				{
					character.GetComponent<SpriteRenderer>().enabled = true;
				}

                character.GetComponent<Collider>().enabled = true;
            }
			foreach (Character character in enemy.partyMembers)						//Turn on the player Enemy members renderers and Colliders on
            {
				if (character.GetComponent<MeshRenderer> () != null) 
				{
					character.GetComponent<MeshRenderer>().enabled = true;
				}
				if (character.GetComponent<SpriteRenderer> () != null) 
				{
					character.GetComponent<SpriteRenderer>().enabled = true;
				}
                character.GetComponent<Collider>().enabled = true;
            }

			if (player.GetComponent<MeshRenderer> () != null)						//Turn off the player and enemy Party's renderers off
			{
				player.GetComponent<MeshRenderer>().enabled = false;
			}
			if (player.GetComponent<SpriteRenderer> () != null)
			{
				player.GetComponent<SpriteRenderer>().enabled = false;
			}
			player.GetComponent<Collider>().enabled = false;

			if (enemy.GetComponent<MeshRenderer> () != null)
			{
				enemy.GetComponent<MeshRenderer>().enabled = false;
			}
			if (enemy.GetComponent<SpriteRenderer> () != null)
			{
				enemy.GetComponent<SpriteRenderer>().enabled = false;
			}
			enemy.GetComponent<Collider>().enabled = false;

			combatManager.AddCharactersToCombat(player.partyMembers);
			combatManager.AddCharactersToCombat(enemy.partyMembers);
			combatManager.HeatValueTaker(partyHeatIntensity);

            combatUI.SetActive(true);

			combatManager.StartCombat();
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

		playerParty.gameObject.GetComponent<Collider>().enabled = true;

        combatUI.SetActive(false);
        
    }
}
