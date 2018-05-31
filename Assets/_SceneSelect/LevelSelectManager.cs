using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public Button LoadButton; 

    void Start()
    {
        transform.FindChild("DesertButton").GetComponent<Button>();
        LoadButton.onClick.RemoveAllListeners();
    }

    private void OnClick()
    {
        LoadButton.enabled.ToString();
    }

    public void LoadDesert()
    {
        SceneManager.LoadScene(0); 
    }

    public void LoadOasis()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadCatacombs()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadBossArena()
    {
        SceneManager.LoadScene(3); 
    }
}