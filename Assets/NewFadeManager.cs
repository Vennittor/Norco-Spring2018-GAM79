using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NewFadeManager : MonoBehaviour
{
    /// <summary>
    /// Ways to Transition through scenes with multiple approaches; 
    /// </summary>
    /// 

        ///
            /// Completed from Youtube Source >> 
        ///

    // First way 
    public static NewFadeManager Instance { set; get; }

    public Image fadeImage;
    private bool isintransition;
    private float transition;
    private bool isdisplaying;
    private float duration;

    // second way vvvv
    public Canvas uiImage;

    // first way
    private void Awake()
    {
        Instance = this; 
    }

    public void Fade(bool displayed, float duration)
    {
        isdisplaying = displayed;
        isintransition = true;
        this.duration = duration;
        transition = (isdisplaying) ? 0 : 1; 
    }

    private void Update()
    {
        // test
        if(Input.GetKeyDown(KeyCode.A))
        {
            Fade(true, 1.5f);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Fade(false, 1.25f);
        }

        if(!isintransition)
        {
            return; 
        }

        transition += (isdisplaying) ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 /duration);
        fadeImage.color = Color.Lerp(new Color(1, 1, 1, 0), Color.black, transition); 
        
        if(transition > 1 || transition < 0)
        {
            isintransition = false; 
        } 
    }
    // end first way 
}
