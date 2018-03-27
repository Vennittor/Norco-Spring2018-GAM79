using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class loadShopScene : MonoBehaviour
{
    // simple loading shop scene once at a desination to enter shop 
    public GameObject player;
    public Transform myTran;

    void Start()
    {
        DontDestroyOnLoad(this);
        player = FindObjectOfType<GameObject>();
        myTran = FindObjectOfType<Transform>();
        myTran = GetComponent<Transform>();
        myTran = player.transform;
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Player")
        {
            StartCoroutine(LoadSceneAsync());
            player.gameObject.SetActive(false);
        }
        else
        {
            player.gameObject.SetActive(true);
            player.transform.position = transform.position;
        }
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation sync = SceneManager.LoadSceneAsync("shopui");
        while (!sync.isDone)
        {
            yield return null;
        }
    }

    public IEnumerator LoadHallScene()
    {
        AsyncOperation sync = SceneManager.LoadSceneAsync("GrandHall_LevelDesign_Test");
        while (!sync.isDone)
        {
            yield return null;
        }
    }

    public void OnClick()
    {
        Debug.Log("exit");
        SceneManager.LoadSceneAsync(0);
    }
}