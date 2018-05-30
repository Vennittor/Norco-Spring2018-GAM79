using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTransitionEffect : MonoBehaviour
{
    private ParticleSystem pS;
    public GameObject thisObj;
    public GameObject targetObj;

    private void Start()
    {
        pS = GetComponentInChildren<ParticleSystem>();
    }

    public void StartEmit()
    {     
        Emitting();
    }

    public void Emitting()
    {
        pS.transform.SetParent(null);
        pS.Play();
    }
}