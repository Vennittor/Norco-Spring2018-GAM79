using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    private float loadingTime;
    private Image transitionImage;
    public bool In = false;
    public bool Out = false;
    public bool settingUp = false; 
    private int level = 0;
    private Animator iAnim;
    private GameObject instance;
    public Vector3 entrancePos = new Vector3(0,0,0);
    public Vector3 exitPos = new Vector3(0,0,0);
    public Transform entranceTransform;
    public Transform exitTransform;
    [SerializeField]
    private LevelManager levelMan;
    public GameObject player;
    private Party playerParty;

    void Awake()
    {
        iAnim = FindObjectOfType<Animator>();
        transitionImage = FindObjectOfType<Image>();
        player = FindObjectOfType<GameObject>();
        levelMan = FindObjectOfType<LevelManager>(); 
    }

    void Start()
    {
        if(instance == null)
        {
            return; 
        }
        else
        {
            if (instance == this.gameObject)
            {
                DontDestroyOnLoad(this);
            }
        }

        playerParty.transform.position = entrancePos; 
        entrancePos = new Vector3(11.5f, 9, -12);
        exitPos = new Vector3(58, 9, -12); 
        levelMan.SetEntrancePosition(playerParty);
    }

    private void OnLevelWasLoaded(int index)
    {
        level++;

        StartCoroutine(TransitionIn());
        // *set entrance position
    }

    private void ExitLevel()
    {
        level--;
        StartCoroutine(TransitionOut()); 
    }

    private void Initialize()
    {
        settingUp = true; 
    }
    
    public IEnumerator TransitionIn()
    {
        Initialize(); 
        yield return null;
    }

    public IEnumerator TransitionOut()
    {
        yield return null; 
    }
}