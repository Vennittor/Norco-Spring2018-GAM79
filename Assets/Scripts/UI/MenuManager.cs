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

    private void Start()
    {
       // DontDestroyOnLoad(gameObject); 
    }

    public void PlayGame()
    {
        Debug.Log("enable transition", this); 
        fMan.StartCoroutine(fMan.FadeIn());
        // Does this > <^^
        Debug.Log("fading in", this);
      //Does this >  fMan.StartCoroutine(fMan.TransitionOutFromMenu()); 
        Debug.Log("fading out", this); 
       // Then > 
        TransitionToScene();
       
        // lvlMan.BeginTransitionSequence(); 
        //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TransitionToScene()
    { 
        fMan.StartCoroutine(fMan.DoneWithTransition());
        fMan.StartCoroutine(fMan.LoadSceneAsync());
        Debug.Log("Done"); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
