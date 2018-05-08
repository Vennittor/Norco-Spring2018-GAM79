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
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(playerParty); 
        transitionImage.enabled = true;
    }

    void Start()
    {
        if(iAnim == null)
        {
            iAnim = GameObject.FindObjectOfType<Animator>().GetComponent<Animator>();
        }

        if(levelMan == null)
        {
            levelMan = GameObject.FindObjectOfType<LevelManager>(); 
        }
    }

    public void TransitionTo(int sceneIndex)
    {
        levelMan.LoadScene(sceneIndex);
    }

    public void In()
    {
        iAnim.Play("fadeIn1");
    }

    public void Out()
    {
        iAnim.Play("fadeOut1");
    }

    public void TransitionComplete()
    {
        Out();
    }
}