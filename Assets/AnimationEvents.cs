using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events; 
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private TransitionManager tManager; 

    void Awake()
    {
        tManager = FindObjectOfType<TransitionManager>(); 
    }

    void Start()
    {
        tManager.name.ToString();
    }

    public void OnAnimationEvent()
    {
        tManager.TransitionOpen(); 
    }
}
