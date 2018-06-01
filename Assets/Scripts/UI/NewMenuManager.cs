using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMenuManager : MonoBehaviour
{
    #region Variables
    private NewTransitionManager tMAn;
    [SerializeField]
    public LevelManager levelMan;
    #endregion

    #region Functions

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void OnBeforeSceneLoadRuntimeMethod()
    {
        Debug.Log("Before scene loaded");

        if (NewTransitionManager.FindObjectOfType<NewTransitionManager>() == null)
        {
            ReferenceEquals(null, true);
            NewTransitionManager.FindObjectOfType<NewTransitionManager>().enabled = false;
            Debug.LogError("Transition Manager is not enabled. ");
        }
        else
        {
            NewTransitionManager.FindObjectOfType<NewTransitionManager>().enabled = true;
        }
    }

    private void Awake()
    {
        tMAn = FindObjectOfType<NewTransitionManager>(); 
    }

    void Start()
    {
        tMAn = NewTransitionManager.Instance;
        levelMan = FindObjectOfType<LevelManager>();
    }

    public void PlayGame()
    {
        Go(); 
        levelMan.StartCoroutine(levelMan.Transition());  
    }

    public void Go()
    {
        SceneManager.LoadSceneAsync(2);
        tMAn.StartCoroutine(tMAn.Fade()); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}