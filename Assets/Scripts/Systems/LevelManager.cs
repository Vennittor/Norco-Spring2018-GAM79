using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

		_instance = this;

		this.gameObject.transform.SetParent (this.gameObject.transform);

		DontDestroyOnLoad(gameObject);

        //SceneManager.LoadScene("OtherSceneName", LoadSceneMode.Additive);
    }

    void Start()
    {
        combatManager = CombatManager.Instance;

		if (combatUI == null) 
		{
			combatUI = GameObject.Find ("Canvas Combat UI");
		}
		if (combatUI == null) 
		{
			Debug.LogError ("LevelManager could not find reference to the Canvas Combat UI");
		}


    }

	void LateStart()
	{

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

			combatUI.SetActive(true);

			combatManager.AddCharactersToCombat(player.partyMembers);
			combatManager.AddCharactersToCombat(enemy.partyMembers);
			combatManager.HeatValueTaker(partyHeatIntensity);

			combatManager.StartCombat();
        }
        else
        {
            Debug.LogError("CombatManger is currently running combat, collision and Initiate Combat calls should no longer be called.");
        }
    }

	public void ReturnFromCombat(bool playersWin = true)
    {
		if (!playersWin)
		{
			Debug.LogError ("Player Lost");
			//TODO go to gameover screen
		}


		UIManager.Instance.ReturnToNormalMode ();									//return the UIManager to normal mode

        foreach (Character character in pParty.partyMembers)						//Turn off the playerParty members renderers off
        {
			if (character.GetComponent<MeshRenderer> () != null) 
			{
				character.GetComponent<MeshRenderer>().enabled = false;
			}
			if (character.GetComponent<SpriteRenderer> () != null) 
			{
				character.GetComponent<SpriteRenderer>().enabled = false;
			}

			character.GetComponent<Collider>().enabled = true;
        }

		if (playerParty.GetComponent<MeshRenderer> () != null)						//Turn on the player Party's renderers on
		{
			playerParty.GetComponent<MeshRenderer>().enabled = true;
		}
		if (playerParty.GetComponent<SpriteRenderer> () != null)
		{
			playerParty.GetComponent<SpriteRenderer>().enabled = true;
		}
		playerParty.GetComponent<Collider>().enabled = true;

		Destroy (eParty.gameObject);												//remove the Enemy Party

        combatUI.SetActive(false);													//disable the CombatUI
        
    }

    public void LoadScene(int sceneANumber)
    {
        Debug.Log("sceneBuildIndex to load: " + sceneANumber);
        SceneManager.LoadScene(sceneANumber);
    }

    public void LoadSceneAsync()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scene2");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    
}
