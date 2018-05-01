using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static LevelManager _instance;

    public AudioClip levelMusic;

    public CombatManager combatManager;

	public GameObject combatUI;
	public GameObject levelUI;

	public Image swipeImage;

    public GameObject playerParty;
    private Party pParty;
    private Party eParty;
    public uint partyHeatIntensity;

    public Transform cameraTarget;
    public Transform playerStartTransform;
    public Transform playerCombatTransform;
    public Transform enemyCombatTransform;
    public GameObject heatWavePrefab; 

    [SerializeField]
    DopeCamSys camDock; 

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

        camDock = FindObjectOfType<DopeCamSys>(); 

        if (playerParty == null)
        {
            playerParty = GameObject.FindGameObjectWithTag("Player");
        }

		_instance = this;

		if (combatUI == null)
		{
			combatUI = FindObjectOfType<UIManager>().gameObject;
		}
		if (combatUI == null) 
		{
			combatUI = GameObject.Find ("Canvas Combat UI");
		}
		if (combatUI == null) 
		{
			Debug.LogError ("LevelManager could not find reference to the Canvas Combat UI");
		}
			
		this.gameObject.transform.SetParent(null);

		DontDestroyOnLoad(gameObject);

		if (heatWavePrefab != null)
		{
			heatWavePrefab.SetActive(false); 
		}

        //SceneManager.LoadScene("OtherSceneName", LoadSceneMode.Additive);
    }

    void Start()
    {
		combatManager = CombatManager.Instance;

		if (combatManager == null)
		{
			Debug.LogError (this.gameObject.name + " could not find reference to LevelManager");
		}

        playerCombatTransform = transform;
        enemyCombatTransform = transform;

		if (combatUI == null)
		{
			combatUI = FindObjectOfType<UIManager>().gameObject;
		}
		if (combatUI == null) 
		{
			combatUI = GameObject.Find ("Canvas Combat UI");
		}
		if (combatUI == null) 
		{
			Debug.LogError ("LevelManager could not find reference to the Canvas Combat UI");
		}

		if (levelUI == null) 
		{
			levelUI = GameObject.Find ("Canvas Level UI");
		}
		if (levelUI == null) 
		{
			Debug.LogError ("LevelManager could not find reference to the Canvas Level UI");
		}
        
        SoundManager.instance.Play(levelMusic, "mxL"); //play the level music

		if (swipeImage == null)
		{
			if (levelUI != null)
			{
				swipeImage = levelUI.transform.Find ("Swipe Image").GetComponent<Image>();
			}
			else
			{
				swipeImage = GameObject.Find ("Swipe Image").GetComponent<Image>();
			}

			if (swipeImage == null)
			{
				Debug.LogError ("LevelManager could not find reference to the Swipe Image");
			}
		}

		if (swipeImage != null)
		{
			swipeImage.enabled = false;
		}
    }

	void LateStart()
	{

	}


    void Update()
    {
        camDock.cameraDock.LookAt(cameraTarget);
    }

    public void GetHeat(uint heat)
    {
        partyHeatIntensity = heat;
		if (heatWavePrefab != null)
		{
			heatWavePrefab.SetActive (true); 
		}
		else
		{
			Debug.LogError("Cannot find reference to heatWavePrefab");
		}

    }

    public void SetCombatPoint(Party enemyParty, Party playerParty)
    {
        camDock.Reposition(); 
        Transform partyPos = playerParty.transform.GetComponent<Transform>();
        enemyCombatTransform.position = new Vector3(partyPos.position.x + 5, partyPos.position.y, partyPos.position.z);
        enemyParty.GetComponent<Transform>().position = enemyCombatTransform.position;
    }

	public IEnumerator InitiateCombat(Party player, Party enemy)
	{
        SoundManager.instance.LevelToCombat();//transition to NoLevel snapshot
        pParty = player;
        eParty = enemy;

		player.GetComponent<KeysToMove> ().movementAllowed = false;

        if(!combatManager.inCombat)
		{
			//Perform Enter 'swipe'
			Vector2 startingAnchorMin = Vector2.zero;
			float i = 0f;
			if (swipeImage != null)
			{
				swipeImage.enabled = true;
				startingAnchorMin = swipeImage.rectTransform.anchorMin;

				i = startingAnchorMin.x;

				while (i <= startingAnchorMin.x + 1)
				{
					i += Time.deltaTime * 2.0f;

					swipeImage.rectTransform.anchorMin = new Vector2 (i, swipeImage.rectTransform.anchorMin.y);
					swipeImage.rectTransform.anchorMax = new Vector2 (i + 1, swipeImage.rectTransform.anchorMax.y);


                    yield return null;
				}

				swipeImage.rectTransform.anchorMin = new Vector2 (startingAnchorMin.x + 1, swipeImage.rectTransform.anchorMin.y);
				swipeImage.rectTransform.anchorMax = new Vector2 (startingAnchorMin.x + 2, swipeImage.rectTransform.anchorMax.y);

				i = startingAnchorMin.x + 1;
			}
            //End Enter Swipe

            //TODO should the Swipe pause for a moment?

            //TODO set all Character in combat stage

            SetCombatPoint(enemy, player); // set combat point

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
			foreach (Character character in enemy.partyMembers)						//Turn on the Enemy members renderers and Colliders on
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

			//Perform Exit 'swipe'
			if (swipeImage != null)
			{
				while (i <= startingAnchorMin.x + 2)
				{
					i += Time.deltaTime * 2.0f;

					swipeImage.rectTransform.anchorMin = new Vector2 (i, swipeImage.rectTransform.anchorMin.y);
					swipeImage.rectTransform.anchorMax = new Vector2 (i + 1, swipeImage.rectTransform.anchorMax.y);

					yield return null;
				}

				swipeImage.rectTransform.anchorMin = new Vector2 (startingAnchorMin.x, swipeImage.rectTransform.anchorMin.y);
				swipeImage.rectTransform.anchorMax = new Vector2 (startingAnchorMin.x + 1, swipeImage.rectTransform.anchorMax.y);

				swipeImage.enabled = false;
			}
			//Swipe Finished and reset

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
        SoundManager.instance.CombatToLevel();//transition to the NoCombat audio snapshot

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

		pParty.GetComponent<KeysToMove> ().movementAllowed = true;

        camDock.RepositionCameraToOriginalPosition(); 
        camDock.targetPosition -= cameraTarget.transform.position; 
        
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
