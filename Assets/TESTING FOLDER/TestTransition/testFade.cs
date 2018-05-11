using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testFade : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        anim = FindObjectOfType<Animator>();
    }

    private IEnumerator fadein ()
    {
        anim.Play("fadeIn1");
        anim.GetComponentInChildren<Animator>();

        yield return new WaitForEndOfFrame();
/*
        anim.Play("fadeOut1");
        anim.GetComponentInChildren<Animator>();

        yield return null;
        */
    }

    private IEnumerator fadeout()
    {
        yield return new WaitForEndOfFrame();

        anim.Play("fadeOut1");
        anim.GetComponentInChildren<Animator>();
    }
}
