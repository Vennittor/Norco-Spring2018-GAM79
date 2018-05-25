using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    #region Variables
    //  private FadeManager fMan;
    private TransitionManager tMAn;
    [SerializeField]
    public LevelManager levelMan;
    #endregion

    #region Functions

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void OnBeforeSceneLoadRuntimeMethod()
    {
        Debug.Log("Before scene loaded");

        if (TransitionManager.FindObjectOfType<TransitionManager>() == null)
        {
            ReferenceEquals(null, true);
            TransitionManager.FindObjectOfType<TransitionManager>().enabled = false;
            Debug.LogError("Transition Manager is not enabled. ");
        }
        else
        {
            TransitionManager.FindObjectOfType<TransitionManager>().enabled = true;
        }
    }

    private void Awake()
    {
        tMAn = FindObjectOfType<TransitionManager>(); 
    }

    void Start()
    {
        tMAn = TransitionManager.Instance;
        levelMan = FindObjectOfType<LevelManager>();
    }

    public void PlayGame()
    {
        Go(); 
       // levelMan.StartCoroutine(levelMan.Transition());  
    }

    public void Go()
    {
        SceneManager.LoadSceneAsync(1);
        tMAn.StartCoroutine(tMAn.Fade()); 
    }

    /*
    public void TransitionToScene()
    { 
        fMan.StartCoroutine(fMan.DoneWithTransition());
        fMan.StartCoroutine(fMan.LoadSceneAsync());
        // fadeIn to next scene
        Debug.Log("Done");
        fMan.StartCoroutine(fMan.FadeIn()); 
    }*/

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
