using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class testFade : MonoBehaviour
{
    public Animator anim;
    private Image im;
    public bool fin;
    public bool fout; 

    private void Start()
    {
        anim = FindObjectOfType<Animator>();
        im = FindObjectOfType<Image>();
        fin = false;
        fout = false; 
    }

    public IEnumerator fadein ()
    {
        fin = true;
        if(fin)
        {
            anim.Play("fadeIn1");
            anim.GetComponentInChildren<Animator>();
            anim.SetBool("fadein", true);
            anim.SetBool("fadeout", false);
        }
        yield return new WaitForEndOfFrame();
/*
        anim.Play("fadeOut1");
        anim.GetComponentInChildren<Animator>();

        yield return null;
        */
    }

    public IEnumerator fadeout()
    {
        yield return new WaitForEndOfFrame();

        fout = true;
        if(fout)
        {
            anim.SetBool("fadein", false);
            anim.SetBool("fadeout", true);
            anim.Play("fadeOut1");
            anim.GetComponentInChildren<Animator>();
        }
    }
}
