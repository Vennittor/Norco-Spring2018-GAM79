using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public Image transitionImage; // transitionImage 
    public Animator iAnim;

    public Transform entranceTransform;
    public Transform exitTransform;
    [SerializeField]
    private LevelManager levelMan;
    private FadeManager fadeMan; 
    private Party playerParty;
    public AnimationState state = AnimationState.neutral;
    public int animState = 2;
    private Color c;
    public Animation fadeOutAnim;
    public GameObject fadeOutObj; 
  //  public Animation anim;
  //  public Animation animOut;
   // public GameObject Load;
   // public GameObject transitionOpen;

    public enum AnimationState 
    {
        neutral, 
        fadein,
        fadeout
    }
    /*
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

           // TransitionManager.FindObjectOfType<TransitionManager>().TransitionIn();
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        Debug.Log("After first scene loaded");
        // if loaded and fadein set
        //  TransitionManager.FindObjectOfType<TransitionManager>().state = AnimationState.neutral;

        if (TransitionManager.FindObjectOfType<TransitionManager>() != null)
        {
            TransitionManager.FindObjectOfType<TransitionManager>().TransitionIn();

            TransitionManager.FindObjectOfType<TransitionManager>().state = AnimationState.fadein;

            TransitionManager.FindObjectOfType<TransitionManager>().StartCoroutine("Fade");
        }      

        //   TransitionManager.FindObjectOfType<TransitionManager>().StartCoroutine("Fade"); 
    }

   [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Debug.Log("RuntimeMethodLoad: After first scene loaded");

        if (TransitionManager.FindObjectOfType<TransitionManager>() != null)
        {
            TransitionManager.FindObjectOfType<TransitionManager>().StopCoroutine("Fade");
          //  TransitionManager.FindObjectOfType<TransitionManager>().StartCoroutine("In"); // ? 
        }
    }
    */

    void Awake()
    {
        state = AnimationState.neutral;
        animState = 0;
      //  TransitionManager.FindObjectOfType<TransitionManager>().gameObject.transform.SetParent(null); 
      //  DontDestroyOnLoad(TransitionManager.FindObjectOfType<TransitionManager>().gameObject);
          Party.FindObjectOfType<Party>().transform.SetParent(null); 
      //  DontDestroyOnLoad(playerParty);

        if (fadeOutObj == null)
        {
            fadeOutObj = FindObjectOfType<GameObject>();
        }

        fadeOutAnim = FindObjectOfType<Animation>();

        transitionImage.enabled = false; // was true
        c = Color.black;

        // test transitionOpen 
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
            fadeOutAnim = GetComponentInChildren<Animation>(); 
        }

        if(fadeOutObj == null)
        {
            GameObject.Find("FadeOutObj").GetComponentInChildren<GameObject>().SetActive(false); 
        }

        if (TransitionManager.FindObjectOfType<TransitionManager>() != null)
        {
            TransitionManager.FindObjectOfType<TransitionManager>().StopCoroutine("Fade");
            TransitionManager.FindObjectOfType<TransitionManager>().StartCoroutine("In"); // ? 
        }
    }

    public IEnumerator Fade()
    {
        iAnim.SetBool("Fade", true);
     //    Debug.Log("Fade is true"); fade is true
        yield return new WaitUntil(() => transitionImage.color.a == 1);
       // levelMan.Transition(); // enter transition/ loading sequence
        yield return null;
    }

    public IEnumerator FadeOut()
    {
        iAnim.SetBool("Fade", false);
        iAnim.SetBool("FadeOut", true);
        yield return new WaitUntil(() => transitionImage.color.a == 0);
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

    /*
    public void TransitionOpen()
    {
        StartCoroutine(TransitionComplete()); // was In(); 
        transitionOpen.gameObject.SetActive(true);  
    }*/

    public void TransitionIn()
    {
       // Debug.Log("Fadeed In"); fadesin
        StartCoroutine(In()); 
    }

    public IEnumerator In()
    {
        transitionImage.GetComponentInChildren<Image>().GetComponentInChildren<Animator>().Play("FadeIn");
      //  StartCoroutine(Fade());
       iAnim.SetBool("Fade", true);

        switch (animState)
        {
            case 0:
                {
                    state = AnimationState.fadein;
                }
                break;
        }
        while (state == AnimationState.fadein)
        {
            //  Debug.Log("Play Fade In Clip "); // plays fadein
            transitionImage.GetComponentInChildren<Image>().GetComponentInChildren<Animator>().Play("FadeIn");

            transitionImage.GetComponentInChildren<Image>().color = Color.black;

            Color c = transitionImage.color;
            c.a = 1;
            while (c.a > 0)
            {
                SetTransparency(transitionImage, 1);
                c.a -= Time.deltaTime * .1f;
                yield return null;
            }
            if (c.a <= 0) 
            {
                c.a = 0;
                //  Debug.Log("Completed cycle?"); completes cycle
            }

            yield return null; 
        }

        if (state == AnimationState.fadein)
        {
            iAnim.enabled = true;
            transitionImage.enabled = true;
            iAnim.GetComponentInChildren<Animator>().SetBool("FadeIn", true);
        }
        else if (state == AnimationState.neutral)
        {
            iAnim.enabled = false;
            transitionImage.enabled = false;
            iAnim.GetComponentInChildren<Animator>().SetBool("FadeIn", false);
           // StopCoroutine(In());
        }

        if (state != AnimationState.fadein || state != AnimationState.fadeout)
        {
            state = AnimationState.neutral;
        }
    }

    public IEnumerator Out(Image transitionImage, float transparency)
    {
        iAnim.SetBool("Fade", false);

        if (transitionImage == null)
        {
            transitionImage = GameObject.Find("Transition Image").GetComponentInChildren<Image>(); 
        }

     //   transitionImage.GetComponentInChildren<Image>().GetComponentInChildren<Animator>().Play("FadeOut");
        StartCoroutine(FadeOut()); 

        // Debug.LogError(state); reaches here when called in FadeOut function
        switch (animState)
        {
            case 1:
                {
                    state = AnimationState.fadeout;
                }
                break;
        }

        transitionImage.GetComponentInChildren<Image>().GetComponentInChildren<Animator>().Play("FadeOut");

        transitionImage.GetComponentInChildren<Image>().color = Color.black;

        while (state == AnimationState.fadeout)
        {
            if (transitionImage != null)
            {
                UnityEngine.Color __alpha = transitionImage.color;
                transparency = 0.2f;
                __alpha.a += transparency * Time.deltaTime;
                transitionImage.color = __alpha;
            }

            if (state == AnimationState.fadeout)
            {
                iAnim.enabled = true;
                transitionImage.enabled = true;
            }

            yield return null; 
        }

        Color c = transitionImage.color;
        c.a = 0;
        while (c.a <= 0)
        {
            SetTransparency(transitionImage, 0);

            Debug.Log(c.a);

            c.a += Time.deltaTime * .1f;
            yield return null;
        }
        if (c.a < 0.1f)
        {
            c.a = 0;
          //  Debug.Log("Completed cycle?"); // completes cycle
          //  Debug.LogError(c.a); // set at 0
        }

        if(c.a >= 0 || c.a <= 0)
        {
            iAnim.Play("FadeOut");
            state = AnimationState.fadeout;

            iAnim.enabled = true;
            transitionImage.enabled = true;

            if (fadeOutObj == null)
            {

                fadeOutAnim.GetComponentInChildren<Animation>().Play(fadeOutAnim.clip.name = "FadeOutFinal");
                GameObject.Find("FadeOutObj").GetComponentInChildren<GameObject>().SetActive(true);
            }

          //  FadeOut();
        }

        yield return null; 
    }

   
    public IEnumerator TransitionComplete()
    {
        // Fade Out();
        StartCoroutine(Out(transitionImage, 1)); // was TransitionOut
        yield return null; 
    }

   /* public IEnumerator TransitionOut()
    {
        StartCoroutine(Out());
       // Debug.LogError(state); gets to neutral state
       
    }*/

    public void BeginTransitionSequence()
    {
        fadeMan.TransitionOutFromMenu(); 
    }
}