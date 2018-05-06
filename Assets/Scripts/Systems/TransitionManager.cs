using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    private float loadingTime;
    public Image transitionImage;
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
        player = FindObjectOfType<GameObject>();
        levelMan = FindObjectOfType<LevelManager>();
        DontDestroyOnLoad(playerParty); 
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
                DontDestroyOnLoad(this.gameObject);
            }
        }

        playerParty.transform.position = entrancePos; 
        entrancePos = new Vector3(11.5f, 9, -12);
        exitPos = new Vector3(58, 9, -12); 
    }

    public void StopIAnim()
    {
        iAnim.gameObject.GetComponentInChildren<Image>().enabled = false;
        iAnim.StopPlayback();
        Debug.Log(transitionImage);
    }

    public void StartIAnim()
    {
        iAnim.gameObject.GetComponentInChildren<Image>().enabled = true;
        iAnim.StartPlayback();
        In = true;
        Out = false;
        settingUp = true; 
        StartCoroutine(TransitionIn());
        Debug.Log(transitionImage);
    }

    private void OnLevelWasLoaded(int index)
    {
        level++;
        StartCoroutine(TransitionIn());
        levelMan.SetEntrancePosition(playerParty); 
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
        In = true;
        Out = false; 
    }
    
    public IEnumerator TransitionIn()
    {
        Initialize();
        transitionImage.GetComponentInChildren<Image>().enabled = true;
        transitionImage.enabled = true; 

        if (In == true && Out == false)
        {
            iAnim.Play("fadeIn1");
            iAnim.SetBool("In", true);
            iAnim.SetBool("Out", false);
        }

        yield return null;
    }

    public IEnumerator TransitionOut()
    {
        In = false;
        Out = true; 

        if (In == false && Out == true)
        {
            iAnim.Play("fadeOut1");
            iAnim.SetBool("In", false);
            iAnim.SetBool("Out", true);
        }

        transitionImage.GetComponentInChildren<Image>().enabled = false;
        transitionImage.enabled = false;

        yield return null; 
    }
}