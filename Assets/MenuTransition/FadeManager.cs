using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video; 
using UnityEngine;

public class FadeManager : AnimationEvents
{
    private Animator anim;
    [SerializeField]
    public Image transitionImage;
    public GameObject fadeUI;
    public GameObject transitionOpen;


    private void Awake()
    {
        anim = FindObjectOfType<Animator>();
        anim.StopPlayback();

        fadeUI = GameObject.Find("Canvas Main Menu");
        anim.SetBool("FadeIn", false);

        if (transitionOpen != null)
        {
            transitionOpen.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        transitionImage.enabled = false; 

        if(anim != null)
        {
            anim.GetAnimatorTransitionInfo(0);
        }

        if (anim != null)
        {
            anim = GameObject.Find("Fade Image").GetComponentInChildren<Animator>();
        }

        if (transitionImage == null)
        {
            if (fadeUI != null)
            {
                Debug.Log("Hi");
                transitionImage = FadeUIManager.Instance.fadeImage;
            }
            else
			{
				transitionImage = GameObject.Find ("Fade Image").GetComponentInChildren<Image>();
			}

            if (transitionImage == null)
            {
                Debug.LogError("FadeUIManager could not find reference to the Fade Image");
            }
        }


        if (transitionOpen.gameObject == null)
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
    }

    public IEnumerator FadeIn()
    {
        anim.GetComponentInChildren<Animator>().Play("fadeIn");
        anim.SetBool("FadeIn", true);
        Debug.Log("FadeIn");
        yield return null; 
    }

    public IEnumerator FadeOut()
    {
        anim.GetComponentInChildren<Animator>().Play("fadeOut");
        Debug.Log("FadeOut");
        anim.SetBool("FadeIn", false);
        anim.SetBool("FadeOut", true);
        yield return null; 
    }

    public IEnumerator TransitionOutFromMenu()
    {
        Debug.Log("TransitionOut"); 
        transitionImage.enabled = true;

        float i = 0;
        transitionImage.GetComponentInChildren<Image>().color = Color.black;
        var tempColor = transitionImage.color;
        tempColor.a = 0;

        i = transitionImage.color.a;

        while (i < transitionImage.color.a + 1)
        {
            i += Time.deltaTime * 0.1f;

           if (i == 1.0f)
            {
                transitionImage.enabled = false;
            }

            transitionImage.CrossFadeColor(Color.black, 1.0f, false, true);

            yield return null;
        }

        transitionImage.enabled = false;
    }

    public IEnumerator LoadSceneAsync()
    {
        StartCoroutine(LoadScene());
        yield return null; 
    }

    public IEnumerator LoadScene() 
    {
        StartCoroutine(TransitionOutFromMenu());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return null; 
    }

    public void TransitionOpen()
    {
        transitionOpen.gameObject.SetActive(true);
    }

    public void SetUpGameScenes()
    {
        if (transitionImage == null)
        {
            transitionImage = GameObject.Find("Fade Image").GetComponentInChildren<Image>();
        }

        if(anim == null)
        {
            anim = GetComponent<Animator>(); 
        }

        if(transitionOpen == null)
        {
            transitionOpen = GameObject.Find("AfterEffects_TransitionOpen").GetComponentInChildren<GameObject>();
        }
    }

    public IEnumerator DoneWithTransition()
    {
        AsyncOperation target = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        while (!target.isDone)
        {
            yield return null;
        }

        SetUpGameScenes();
        TransitionOpen(); 

        if (this != null)
        {
            SoundManager sound = GetComponent<SoundManager>();
            DontDestroyOnLoad(sound.audioItemMXlevel);
            DontDestroyOnLoad(transitionOpen); 
        }
        else
        {
            yield return null;
        }
    }

    // End Transition Levels 
}