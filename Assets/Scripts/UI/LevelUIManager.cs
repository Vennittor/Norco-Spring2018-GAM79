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
            Debug.Log("Whats up visual studio"); 
                Destroy(gameObject);
                return;
            }

        _instance = this;

        // swipeImage = GetComponent<Image>();
        //DontDestroyOnLoad(gameObject);
     }
}
