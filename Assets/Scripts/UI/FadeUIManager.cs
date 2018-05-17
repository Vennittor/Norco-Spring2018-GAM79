using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;

public class FadeUIManager : MonoBehaviour
{
    public static FadeUIManager _instance;
    public Image fadeImage;

    public static FadeUIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new FadeUIManager();
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }
}