using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    private static TransitionManager instance; 
    public Image transitionImage; 
    public Animator iAnim;
    public Animator fadeOutAnim; 

    public Transform entranceTransform;
    public Transform exitTransform;
    [SerializeField]
    private LevelManager levelMan;
    private FadeManager fadeMan; 
    [SerializeField]
    private Party playerParty;
    public AnimationState state = AnimationState.neutral;
    public int animState = 2;
    private Color c;
    public Animation fadeOutAnimA;
    public GameObject fadeOutObj; 

    public static TransitionManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new TransitionManager(); 
            }
            return instance;
        }
    }

    public enum AnimationState 
    {
        neutral = 0, 
        fadein = 1,
        fadeout = 2
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void OnBeforeSceneLoadRuntimeMethod()
    {
        Debug.Log("Before scene loaded");

        if(TransitionManager.FindObjectOfType<TransitionManager>() == null)
        {
            ReferenceEquals(null, true);
            TransitionManager.FindObjectOfType<TransitionManager>().enabled = false;
            Debug.LogError("Transition Manager is not enabled. "); 
        }

        else if (TransitionManager.FindObjectOfType<TransitionManager>() != null)
        {
            TransitionManager.FindObjectOfType<TransitionManager>().state = AnimationState.neutral;
        }
        /*
        else if (TransitionManager.FindObjectOfType<TransitionManager>() != null)
        {
            TransitionManager.FindObjectOfType<TransitionManager>().state = AnimationState.fadeout;
            if (TransitionManager.FindObjectOfType<TransitionManager>().state == AnimationState.fadeout)
            {
                TransitionManager.FindObjectOfType<TransitionManager>().StartCoroutine(TransitionManager.FindObjectOfType<TransitionManager>().FadeOut());
            }
        }*/
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        Debug.Log("After first scene loaded");
        if (TransitionManager.FindObjectOfType<TransitionManager>() != null)
        {
            TransitionManager.FindObjectOfType<TransitionManager>().state = AnimationState.fadein;
        }

        Debug.Log("RuntimeMethodLoad: After first scene loaded");

        if (TransitionManager.FindObjectOfType<TransitionManager>() != null)
        {
            TransitionManager.FindObjectOfType<TransitionManager>().StartCoroutine("Fade");
            TransitionManager.FindObjectOfType<TransitionManager>().StartCoroutine("In");
        }

        TransitionManager.FindObjectOfType<TransitionManager>().state = AnimationState.neutral;
        if (TransitionManager.FindObjectOfType<TransitionManager>().state == AnimationState.neutral)
        {
            TransitionManager.FindObjectOfType<TransitionManager>().StopCoroutine(TransitionManager.FindObjectOfType<TransitionManager>().In());
        }
    }

    void Awake()
    {
        state = AnimationState.fadein;
        animState = 1;

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this; 

       // TransitionManager.FindObjectOfType<TransitionManager>().gameObject.transform.SetParent(null); 
       // DontDestroyOnLoad(gameObject);

        if(playerParty == null)
        {
            Debug.Log("Player party does not exhist");
        }
        TransitionManager.FindObjectOfType<TransitionManager>().enabled = true;
       // Party.FindObjectOfType<Party>().transform.SetParent(null); 
       // DontDestroyOnLoad(playerParty);

        if (fadeOutObj == null)
        {
            fadeOutObj = FindObjectOfType<GameObject>();
        }

        fadeOutAnimA = FindObjectOfType<Animation>();

        transitionImage.enabled = true; 
        c = Color.black;

        // test transitionOpen     // inactive
        /*  if(transitionOpen != null)
          {
              DontDestroyOnLoad(transitionOpen); 
              transitionOpen.gameObject.SetActive(false); 
          }
          */
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

        if(fadeOutAnim != null)
        {
            fadeOutAnimA = GetComponentInChildren<Animation>(); 
        }

        if(fadeOutObj == null)
        {
            GameObject.Find("FadeOutObj").GetComponentInChildren<GameObject>().SetActive(false); 
        }

        if (TransitionManager.FindObjectOfType<TransitionManager>() != null)
        {
            TransitionManager.FindObjectOfType<TransitionManager>().StartCoroutine("Fade");
        }
    }

    private void OnLevelWasLoaded()
    {
        TransitionManager.FindObjectOfType<TransitionManager>().state = AnimationState.fadein;
        TransitionManager.FindObjectOfType<TransitionManager>().StartCoroutine("Fade");
        TransitionManager.FindObjectOfType<TransitionManager>().StartCoroutine("In");
    }

    public IEnumerator Fade()
    {
        iAnim.SetBool("Fade", true);
     //    Debug.Log("Fade is true"); fade is true
        yield return new WaitUntil(() => transitionImage.color.a == 0);
       // levelMan.Transition(); // enter transition/ loading sequence
        yield return null;
    }

    public IEnumerator FadeOut()
    {
        iAnim.SetBool("Fade", false);
        iAnim.SetBool("FadeOut", true);
        yield return new WaitUntil(() => transitionImage.color.a == 1);
      //  Debug.Log("Fade is false"); fade is set to false
      //  StartCoroutine(TransitionComplete()); 
        yield return null;
    }

    public static void SetTransparency(Image transitionImage, float transparency)
    {
        if (transitionImage != null)
        {
            UnityEngine.Color __alpha = transitionImage.color;
            transparency = 0.2f; 
            __alpha.a -= transparency * Time.deltaTime;
            transitionImage.color = __alpha;
        }
    }

    /* // inactive
    public void TransitionOpen()
    {
        StartCoroutine(TransitionComplete()); // was In(); 
        transitionOpen.gameObject.SetActive(true);  
    }*/

    public void TransitionIn()
    {
        StartCoroutine(In()); 
    }

    public void TransitionOut()
    {
        StartCoroutine(Out()); 
    }

    public IEnumerator In()
    {
        Debug.Log(state); 

        switch (animState)
        {
            case 1:
                {
                    state = AnimationState.fadein;
                }
                break;
        }
        while (state == AnimationState.fadein)
        {
            if(iAnim.gameObject.activeSelf)
            {
                transitionImage.GetComponentInChildren<Image>().GetComponentInChildren<Animator>().Play("FadeIn");
            }
            transitionImage.GetComponentInChildren<Image>().color = Color.black;

            Color c = transitionImage.color;
            c.a = 1;
            while (c.a > 0)
            {
                SetTransparency(transitionImage, 1);
                c.a -= Time.deltaTime * .2f;
                yield return null;
            }
            if (c.a <= 0) 
            {
                c.a = 0;
                transitionImage.gameObject.SetActive(false);
                iAnim.enabled = false; 
            }
            else
            {
                transitionImage.gameObject.SetActive(true);
                iAnim.enabled = true;
            }

            yield return null; 
        }

        yield return null;
        StartCoroutine(Nuetral()); 
    }

    public IEnumerator Out()
    {
        switch (animState)
        {
            case 2:
                {
                    state = AnimationState.fadeout;
                }
                break;
        }
        while (state == AnimationState.fadeout)
        {
            if (iAnim.gameObject.activeSelf)
            {
                transitionImage.GetComponentInChildren<Image>().GetComponentInChildren<Animator>().Play("FadeOut");
            }
            transitionImage.GetComponentInChildren<Image>().color = Color.black;

            Color c = transitionImage.color;
            c.a = 0;
            while (c.a <= 0)
            {
                SetTransparency(transitionImage, 0);
                c.a += Time.deltaTime * .2f;
                yield return null;
            }
            if (c.a > 0)
            {
                c.a = 1;
                transitionImage.gameObject.SetActive(true);
                iAnim.enabled = true;
            }
        }

        yield return null;

        StartCoroutine(Nuetral()); 
    }

    public IEnumerator Nuetral()
    {
        switch(animState)
        {
            case 0:
                {
                    state = AnimationState.neutral; 
                }
                break;
        }

        if (state == AnimationState.neutral)
        {
            StopCoroutine(In());
            StopCoroutine(Fade()); 
            iAnim.GetComponentInChildren<Animator>().SetBool("Fade", false);
            iAnim.GetComponentInChildren<Animator>().SetBool("FadeOut", false);
        }

        if(state == AnimationState.fadein && transitionImage.gameObject == null)
        {
            iAnim.enabled = false;
            transitionImage.enabled = false;
            iAnim.GetComponentInChildren<Animator>().SetBool("Fade", false);
        }
        else if (state == AnimationState.fadein && state != AnimationState.neutral && transitionImage != null)
        {
            iAnim.enabled = true;
            transitionImage.enabled = true;
            iAnim.GetComponentInChildren<Animator>().SetBool("Fade", true);
        }

        StopCoroutine(In()); 

        yield return null; 
    }

    public void BeginTransitionSequence()
    {
        fadeMan.TransitionOutFromMenu(); 
    }
}