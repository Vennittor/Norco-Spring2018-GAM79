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
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject == player)
        {
            fadeScript.StartCoroutine("fadein"); 
        }
    }

}
