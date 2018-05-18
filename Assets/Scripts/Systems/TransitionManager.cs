﻿using System.Collections;
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
    private FadeManager fadeMan; 
    private Party playerParty;
    public bool InT = false;
    public bool OutT = false;
    public Animation anim; 
    public GameObject Load;
    public GameObject transitionOpen;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(playerParty);
     
        transitionImage.enabled = true;
  
        if(transitionOpen != null)
        {
            DontDestroyOnLoad(transitionOpen); 
            transitionOpen.gameObject.SetActive(false); 
        }
    }

    void Start()
    {
        if (iAnim == null)
        {
           iAnim = GameObject.Find("Transition Image").GetComponentInChildren<Animator>();
        }
        if (levelMan == null)
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
        else
        {
            transitionOpen.gameObject.SetActive(true);
        }

        anim = GetComponentInChildren<Animation>();
        foreach(AnimationState state in anim)
        {
            state.speed = 0.5f;
            state.wrapMode = WrapMode.Once;
            state.clip.name = "fadeIn1"; 
        }
    }

    public void TransitionOpen()
    {
        InT = false; OutT = false;
        StartCoroutine(TransitionComplete()); // was In(); 
        transitionOpen.gameObject.SetActive(true);  
    }

    public void TransitionTo(int sceneIndex)
    {
      //  levelMan.LoadScene(sceneIndex);
    }

    public IEnumerator In()
    {
        yield return new WaitForSeconds(3.0f); 

        Debug.Log("Go"); 
        iAnim.Play("fadeIn1");
        iAnim.SetBool("In", true);
        iAnim.SetBool("Out", false);
        anim["fadeIn1"].wrapMode = WrapMode.Once; 
        yield return new WaitForSeconds(3.0f);

        yield return null; 
    }

    public IEnumerator Out()
    {
        InT = false; 
        OutT = true; 
        if (InT == false && OutT == true)
        {
            iAnim.GetComponentInChildren<Animator>().Play("fadeOut1");
            iAnim.GetComponentInChildren<Animator>().SetBool("In", false);
            

            yield return new WaitForEndOfFrame();
            iAnim.GetComponentInChildren<Animator>().SetBool("Out", true);
            anim.Play(PlayMode.StopAll);
            anim["fadeOut1"].wrapMode = WrapMode.Once;
        }      
        yield return null; 
    }

    public IEnumerator TransitionComplete()
    {
        // Fade Out();
        StartCoroutine(Out()); 
        Debug.Log("TransitionOut");
        yield return null; 
    }

    public void BeginTransitionSequence()
    {
        fadeMan.TransitionOutFromMenu(); 
    }
}