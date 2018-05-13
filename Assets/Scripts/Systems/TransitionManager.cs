using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public Image transitionImage;
    public Animator iAnim;
    public Transform entranceTransform;
    public Transform exitTransform;
    [SerializeField]
    private LevelManager levelMan;
    private Party playerParty;
    public GameObject Load;
    public GameObject transitionOpen;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(playerParty);
        transitionImage.enabled = true;
        if(transitionOpen != null)
        {
            transitionOpen.gameObject.SetActive(false); 
        }
    }

    void Start()
    {
        if(iAnim == null)
        {
           // iAnim = GameObject.Find("Transition Image").GetComponentInChildren<Animator>();
        }

        if(levelMan == null)
        {
            levelMan = GameObject.FindObjectOfType<LevelManager>(); 
        }

        if(transitionOpen.gameObject == null)
        {
            transitionOpen = GameObject.Find("AfterEffects_TransitionOpen");
            if (transitionOpen.GetComponentInChildren<GameObject>() != null)
            {
                Debug.Log("tO");
                transitionOpen.gameObject.SetActive(false);
            }
        }
    }

    public void TransitionOpen()
    {
        StartCoroutine(In()); 
       // transitionOpen.gameObject.SetActive(true);  
    }

    public void TransitionTo(int sceneIndex)
    {
        levelMan.LoadScene(sceneIndex);
    }

    public IEnumerator In()
    {
        yield return new WaitForSeconds(3.0f); 

        Debug.Log("Go"); 
        iAnim.Play("fadeIn1");
        iAnim.SetBool("In", true);
        iAnim.SetBool("Out", false);
        yield return new WaitForSeconds(3.0f);

        yield return null; 
    }

    public IEnumerator Out()
    {
        iAnim.Play("fadeOut1");

        yield return null; 
    }

    public void TransitionComplete()
    {
        // Out();
        Debug.Log("Complete"); 
    }
}