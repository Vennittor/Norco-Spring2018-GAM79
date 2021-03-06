﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;

    public AudioClip levelMusic;

    public CombatManager combatManager;

    public GameObject combatUI;
    public GameObject levelUI;

    public Image swipeImage;
    public Image transitionImage;

    public GameObject playerParty;
    public Party pParty;
    private Party eParty;
    public uint partyHeatIntensity;

    public Transform cameraTarget;
    // public Transform playerCombatTransform = null;
    // public Transform enemyCombatTransform = null;
    public GameObject heatWavePrefab;

    public Vector3 partyPos;

    [SerializeField]
    private NewTransitionManager transitionMan;

    [SerializeField]
    DopeCamSys camDock;

    public GameObject entrance;
    public GameObject exit;

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

        if (transitionImage != null)
        {
            // transitionImage.transform.SetParent(null);
            // DontDestroyOnLoad(transitionImage);
        }
        else
        {
            transitionImage = GameObject.Find("Transition Image").GetComponentInChildren<Image>();
        }

        _instance = this;

        if (combatUI == null)
        {
            combatUI = FindObjectOfType<UIManager>().gameObject;
        }
        if (combatUI == null)
        {
            combatUI = GameObject.Find("Canvas Combat UI");
        }
        if (combatUI == null)
        {
            Debug.LogError("LevelManager could not find reference to the Canvas Combat UI");
        }

        this.gameObject.transform.SetParent(null);

        if (playerParty == null)
        {
            playerParty = GameObject.FindGameObjectWithTag("Player");

            if (playerParty == null)
            {
                Debug.LogError("Can't find player Party");
            }
        }

        if (playerParty != null)
        {
            partyPos = playerParty.transform.position;
        }


        transitionMan = FindObjectOfType<NewTransitionManager>();

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
            Debug.LogError(this.gameObject.name + " could not find reference to LevelManager");
        }

        if (combatUI == null)
        {
            combatUI = FindObjectOfType<UIManager>().gameObject;
        }
        if (combatUI == null)
        {
            combatUI = GameObject.Find("Canvas Combat UI");
        }
        if (combatUI == null)
        {
            Debug.LogError("LevelManager could not find reference to the Canvas Combat UI");
        }

        if (levelUI == null)
        {
            levelUI = GameObject.Find("Canvas Level UI");
        }
        if (levelUI == null)
        {
            Debug.LogError("LevelManager could not find reference to the Canvas Level UI");
        }

        if (camDock == null)
        {
            camDock = FindObjectOfType<DopeCamSys>();
            //   Debug.Log("Discovered CamDock" + camDock.ToString());
        }

        SoundManager.instance.Play(levelMusic, "mxL"); //play the level music
                                                       // check if null onload

        if (swipeImage == null)
        {
            if (levelUI != null)
            {
                Debug.Log("Hi");
                swipeImage = LevelUIManager.Instance.swipeImage;
            }
            else
            {
                swipeImage = GameObject.Find("Swipe Image").GetComponent<Image>();
            }

            if (swipeImage == null)
            {
                Debug.LogError("LevelManager could not find reference to the Swipe Image");
            }
        }

        if (swipeImage != null)
        {
            swipeImage.enabled = true;
        }

        if (transitionImage == null)
        {
            if (levelUI != null)
            {
                transitionImage = levelUI.transform.Find("Transition Image").GetComponent<Image>();
            }
            else
            {
                transitionImage = GameObject.Find("Transition Image").GetComponent<Image>();
            }

            if (levelUI == null)
            {
                Debug.LogError("Level UI is null");
            }
        }

        if (transitionImage != null && levelUI != null)
        {
            transitionImage.enabled = true;
        }
    }

    public void GetHeat(uint heat)
    {
        partyHeatIntensity = heat;
        if (heatWavePrefab != null)
        {
            heatWavePrefab.SetActive(true);
        }
        else
        {
            Debug.LogError("Cannot find reference to heatWavePrefab");
        }

    }

    public void SetCombatPoint(Party enemyParty, Party playerParty)
    {
        if (playerParty == null)
        {
            playerParty = FindObjectOfType<Party>();
            playerParty.GetComponent<Party>();
            ReferenceEquals(playerParty, null);
        }
        else
        {
            return;
        }

        camDock.Reposition();
        Transform partyPos = playerParty.transform.GetComponent<Transform>();
        enemyParty.transform.position = new Vector3(eParty.transform.position.x + 5, eParty.transform.position.y, eParty.transform.position.z);
        enemyParty.GetComponent<Transform>().position = eParty.transform.position;
    }

	public void StartCoInitiateCombat(Party party, Party enemy)
	{
		StartCoroutine (InitiateCombat (party, enemy));
	}

    public IEnumerator InitiateCombat(Party player, Party enemy)
    {
        if (!combatManager.inCombat)
        {
            SoundManager.instance.LevelToCombat();//transition to NoLevel snapshot
            pParty = player;
            eParty = enemy;

            player.GetComponent<KeysToMove>().movementAllowed = false;

            //Perform Enter 'swipe'
            Vector2 startingAnchorMin = Vector2.zero;
            float i = 0f;

            if (swipeImage == null)
            {
                Debug.Log("Find Swipe Image");
                swipeImage = GameObject.Find("Swipe Image").GetComponent<Image>();
            }

            if (swipeImage != null)
            {
                swipeImage.enabled = true;
                startingAnchorMin = swipeImage.rectTransform.anchorMin;

                i = startingAnchorMin.x;

                while (i <= startingAnchorMin.x + 1)
                {
                    i += Time.deltaTime * 2.0f;

                    swipeImage.rectTransform.anchorMin = new Vector2(i, swipeImage.rectTransform.anchorMin.y);
                    swipeImage.rectTransform.anchorMax = new Vector2(i + 1, swipeImage.rectTransform.anchorMax.y);


                    yield return null;
                }

                swipeImage.rectTransform.anchorMin = new Vector2(startingAnchorMin.x + 1, swipeImage.rectTransform.anchorMin.y);
                swipeImage.rectTransform.anchorMax = new Vector2(startingAnchorMin.x + 2, swipeImage.rectTransform.anchorMax.y);

                i = startingAnchorMin.x + 1;
            }
            //End Enter Swipe

            //TODO should the Swipe pause for a moment?

            //set all Character in combat stage/positions
            SetCombatPoint(enemy, player); // set combat point

            foreach (Character character in player.partyMembers)                    //Turn on the player Party members renderers and Colliders on
            {
                if (character.GetComponent<MeshRenderer>() != null)
                {
                    character.GetComponent<MeshRenderer>().enabled = true;
                }
                if (character.GetComponent<SpriteRenderer>() != null)
                {
                    character.GetComponent<SpriteRenderer>().enabled = true;
                }

                character.GetComponent<Collider>().enabled = true;
            }
            foreach (Character character in enemy.partyMembers)						//Turn on the Enemy members renderers and Colliders on
            {
                if (character.GetComponent<MeshRenderer>() != null)
                {
                    character.GetComponent<MeshRenderer>().enabled = true;
                }
                if (character.GetComponent<SpriteRenderer>() != null)
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
            player.GetComponent<Animator>().enabled = false;

            if (enemy.GetComponent<MeshRenderer>() != null)
            {
                enemy.GetComponent<MeshRenderer>().enabled = false;
            }
            if (enemy.GetComponent<SpriteRenderer>() != null)
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

                    swipeImage.rectTransform.anchorMin = new Vector2(i, swipeImage.rectTransform.anchorMin.y);
                    swipeImage.rectTransform.anchorMax = new Vector2(i + 1, swipeImage.rectTransform.anchorMax.y);

                    yield return null;
                }

                swipeImage.rectTransform.anchorMin = new Vector2(startingAnchorMin.x, swipeImage.rectTransform.anchorMin.y);
                swipeImage.rectTransform.anchorMax = new Vector2(startingAnchorMin.x + 1, swipeImage.rectTransform.anchorMax.y);

                swipeImage.enabled = false;
            }

            levelUI.SetActive(false);

            //Swipe Finished and reset

            combatManager.StartCombat();
        }
        else
        {
            //  Debug.LogError("CombatManger is currently running combat, collision and Initiate Combat calls should no longer be called.");
        }
    }

    public void ReturnFromCombat(bool playersWin = true)
    {
        Debug.Log("Return from combat");
        if (!playersWin)
        {
            Debug.LogError("Player Lost");
            //TODO go to gameover screen          
        }

        SoundManager.instance.CombatToLevel();//transition to the NoCombat audio snapshot

        UIManager.Instance.ReturnToNormalMode();                                    //return the UIManager to normal mode

        levelUI.SetActive(true);
        //TODO start return transition

        foreach (Character character in pParty.partyMembers)						//Turn off the playerParty members renderers off
        {
            if (character.GetComponent<MeshRenderer>() != null)
            {
                character.GetComponent<MeshRenderer>().enabled = false;
            }
            if (character.GetComponent<SpriteRenderer>() != null)
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
        playerParty.GetComponent<Animator>().enabled = true;

        Destroy(eParty.gameObject);												//remove the Enemy Party

        combatUI.SetActive(false);                                                  //disable the CombatUI

        pParty.GetComponent<KeysToMove>().movementAllowed = true;

        camDock.RepositionCameraToOriginalPosition();

        eParty = null;
        pParty = null;

        //TODO complete return transition

    }

    // Transition Levels

    public void SetEntrancePosition(Party playerParty)
    {
        partyPos = transitionMan.entranceTransform.position;

        playerParty.transform.position = partyPos;

        if (entrance.gameObject == null)
        {
            partyPos = transitionMan.entranceTransform.position;
            Instantiate(entrance.gameObject, entrance.transform.position, entrance.transform.rotation);
            entrance.transform.position = partyPos;
        }
    }

    public void SetExitPosition(Party playerParty)
    {
        partyPos = transitionMan.exitTransform.position;

        playerParty.transform.position = partyPos;

        partyPos = exit.gameObject.transform.position;
    }

    public IEnumerator Transition()
    {
        Debug.Log("Fade Out");

        if (transitionImage == null)
        {
            transitionImage = GameObject.Find("Transition Image").GetComponentInChildren<Image>();
        }

        if (transitionImage != null)
        {
            transitionImage = GetComponentInChildren<Image>();
        }

        SetUpNewScene();
        //  transitionImage.transform.SetParent(null);
        // DontDestroyOnLoad(transitionImage); // gets destroyed onLoad? 
        //  DontDestroyOnLoad(transitionMan.gameObject); // says transition manager is destroyed? 

        if (transitionImage == null)
        {
            transitionImage.enabled = true;
        }

        float i = 0;
        transitionImage.GetComponent<Image>().color = Color.black;
        var tempColor = transitionImage.color;
        tempColor.a = 1;

        i = transitionImage.color.a;
        transitionImage.enabled = true;

        // throwing error here
        while (i < tempColor.a)
        {
            i += Time.deltaTime * 0.1f;

            if (i == 1.0f)
            {
                transitionImage.CrossFadeColor(Color.black, 1.0f, false, true);
            }
            yield return null;
        }

        transitionImage.enabled = false;

        yield return null;

        if (transitionImage == null)
        {
            transitionImage = GameObject.Find("Transition Image").GetComponentInChildren<Image>();
            Debug.LogError("Inactive");
            transitionImage.enabled = false;
        }
        else if (transitionImage != null)
        {
            transitionMan.StopCoroutine(transitionMan.Fade());
            transitionMan.StartCoroutine(transitionMan.Nuetral());
            yield return new WaitForEndOfFrame();
            transitionImage.enabled = true;
            transitionMan.StopCoroutine(transitionMan.In());
        }

        yield return null;
    }

    public void LoadScene(int sceneANumber)
    {
        Debug.Log("sceneBuildIndex to load: " + sceneANumber);
        SceneManager.LoadScene(sceneANumber);
    }

	public void StartCoLoadSceneAsync()
	{
		StartCoroutine (LoadSceneAsync ());
	}

    public IEnumerator LoadSceneAsync()
    {
        testTransitionEffect p = FindObjectOfType<testTransitionEffect>();
        p.StartEmit();
      //  p.TransitionImageOut(0);

        StartCoroutine(LoadYourAsyncScene());

        yield return null;
    }

	public void StartCoDoneWithTransition(Party playerParty)
	{
		StartCoroutine (DoneWithTransition (playerParty));
	}

    public IEnumerator DoneWithTransition(Party playerParty)
    {
        if (transitionMan != null)
        {
            transitionMan.StartCoroutine(transitionMan.In());
            SetEntrancePosition(playerParty);
        }
        else
        {
            yield return null;
        }
    }

    public void SetUpNewScene()
    {
        if (combatUI == null)
        {
            combatUI = GetComponent<CombatManager>().gameObject;

        }
        if (camDock == null)
        {
            Debug.Log("Find Camdock");
            camDock = FindObjectOfType<DopeCamSys>();
        }
        if (swipeImage == null)
        {
            swipeImage = GameObject.Find("Swipe Image").GetComponent<Image>();
        }
        if (levelUI == null)
        {
            levelUI = GameObject.Find("Canvas Level UI");
        }

        if (transitionImage == null)
        {
            transitionImage = GameObject.Find("Transition Image").GetComponentInChildren<Image>();
        }

        if (_instance == null)
        {
            _instance = Instance;
            _instance = FindObjectOfType<LevelManager>();
            //  DontDestroyOnLoad(this); 
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSeconds(3.0f); 

        AsyncOperation target = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        // AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1); 

        while (!target.isDone)
        {
            yield return null;
        }

        SetUpNewScene();
    }

    // End Transition Levels 
}