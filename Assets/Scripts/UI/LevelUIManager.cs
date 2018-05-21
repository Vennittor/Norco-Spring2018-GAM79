using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    public static LevelUIManager _instance;
    public Image swipeImage; 

    public static LevelUIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LevelUIManager();
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

        _instance.transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
     }
}