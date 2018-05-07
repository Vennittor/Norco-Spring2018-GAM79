using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public Image transitionImage;
    private Animator iAnim;
    public Transform entranceTransform;
    public Transform exitTransform;
    [SerializeField]
    private LevelManager levelMan;
    private Party playerParty;

    void Awake()
    {
        iAnim = FindObjectOfType<Animator>();
        levelMan = FindObjectOfType<LevelManager>();
        DontDestroyOnLoad(playerParty);
        transitionImage.enabled = true;
    }

    public void TransitionTo(int sceneIndex)
    {
        levelMan.LoadScene(sceneIndex);
    }

    public void In()
    { 
        iAnim.CrossFade("fadeIn1", 2.0f); 
    }

    public void Out()
    {
        iAnim.CrossFade("fadeOut1", 3.0f); 
    }

    public void TransitionComplete()
    {
        iAnim.enabled = false; 
    }
}