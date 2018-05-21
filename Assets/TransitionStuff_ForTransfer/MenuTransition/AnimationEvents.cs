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

    private void OnSceneWasLoaded() 
    {
        fadeMan.StartCoroutine(fadeMan.FadeIn());
        fadeMan.TransitionOutFromMenu();
        fadeMan.StartCoroutine(fadeMan.FadeOut());
        return;  
    }
}
