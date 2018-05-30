using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class testTransitionEffect : MonoBehaviour
{
    private ParticleSystem pS;
    public Animation an; 

    private void Start()
    {
        pS = GetComponentInChildren<ParticleSystem>();
        an = FindObjectOfType<Animation>(); 
    }

    public void StartEmit()
    {     
        Emitting();
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void TransitionImageOut(float time)
    {
        time = 5; 
        StartCoroutine(ExecuteAfterTime(time));       
        if(time >= 5.0f)
        {
            an.Play();
        }
        else if(time < 5 && an == null)
        {
            Debug.LogError(an);
            Debug.Log("not playing? ");
        }
    }

    public void Emitting()
    {
        pS.transform.SetParent(null);
        pS.Play();
    }
}