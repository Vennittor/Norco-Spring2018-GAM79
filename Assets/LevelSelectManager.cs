using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public Button desert;
    public Button oasis;
    public Button catacombs;
    public Button bossArena; 

    void Start()
    {
        desert = GetComponentInChildren<Button>(); 
        oasis = GetComponentInChildren<Button>(); 
        catacombs = GetComponentInChildren<Button>();
        bossArena = GetComponentInChildren<Button>(); 
    }

    void Update()
    {
        if (Input.GetButtonDown("Desert"))
        {
            LoadDesert(); 
        }

        if (Input.GetButtonDown("Oasis"))
        {
            LoadOasis(); 
        }

        if (Input.GetButtonDown("Catacombs"))
        {
            LoadCatacombs();
        }

        if (Input.GetButtonDown("BossArena"))
        {
            LoadBossArena();
        }
    }

    public void LoadDesert()
    {
        SceneManager.LoadScene(""); 
    }

    public void LoadOasis()
    {   
        Debug.Log("I am pressed"); 
        SceneManager.LoadScene(0);
    }

    public void LoadCatacombs()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadBossArena()
    {
        SceneManager.LoadScene("");
    }
}
