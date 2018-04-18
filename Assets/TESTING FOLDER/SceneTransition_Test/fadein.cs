using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fadein : MonoBehaviour
{
    [SerializeField]
    private testFade fadeScript;
    public GameObject player; 
    
    void Start()
    {
        player = FindObjectOfType<GameObject>();
        fadeScript = FindObjectOfType<testFade>();
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            fadeScript.StartCoroutine(fadeScript.fadein());
            fadeScript.anim.Play("fadeIn1"); 
            fadeScript.fin = true;
            fadeScript.fout = false; 
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            fadeScript.StartCoroutine(fadeScript.fadeout());
            fadeScript.anim.Play("fadeOut1");
            fadeScript.fout = true;
            fadeScript.fin = false;
        }
    }
}