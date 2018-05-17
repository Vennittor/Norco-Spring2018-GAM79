using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events; 
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private FadeManager fadeMan;

    void Awake()
    {
        fadeMan = FindObjectOfType<FadeManager>();
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject); 
    }

    private void OnSceneWasLoaded() 
    {
        fadeMan.StartCoroutine(fadeMan.FadeIn());
        fadeMan.TransitionOutFromMenu();
        fadeMan.StartCoroutine(fadeMan.FadeOut());
        return;  
        // fade out, animation fade out, 
        // load intro text, 
        // load new scene
        // fade in, animation fade in; 
    }
}
