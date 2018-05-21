using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    #region Variables
    private FadeManager fMan; 
    #endregion 

    #region Functions

    private void Awake()
    {
        fMan = FindObjectOfType<FadeManager>(); 
    }

    public void PlayGame()
    {
        TransitionToScene();   
    }

    public void TransitionToScene()
    { 
        fMan.StartCoroutine(fMan.DoneWithTransition());
        fMan.StartCoroutine(fMan.LoadSceneAsync());
        // fadeIn to next scene
        Debug.Log("Done");
        fMan.StartCoroutine(fMan.FadeIn()); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
