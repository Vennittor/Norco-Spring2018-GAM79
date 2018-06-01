using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ToMainMenu : MonoBehaviour
{
    public VideoPlayer player;
    public Renderer mat;
    public Material mat1; 

	void Start()
    {
        player = GetComponent<VideoPlayer>();
        mat = FindObjectOfType<Renderer>();
        mat1 = FindObjectOfType<Material>();  
    }

    void Update()
    {
        float time; 
        time = Time.time;
    }

    void LateUpdate()
    {
        LoadToMenu(); 
    }

    void LoadToMenu()
    {
        float time;
        time = Time.deltaTime;
        time++; 
        Debug.Log(time); 
       
        if(time > 1)
        {
            StartCoroutine(IsLoading(0)); 
        }

        return;             
    }

    private IEnumerator IsLoading(float time)
    {
        Debug.Log("Waiting " + time); 
        yield return new WaitForSeconds(12.0f);

        if (time >= 1)
        {
            if(mat.name != null)
            {
                mat.GetComponent<MeshRenderer>().material = mat1;
            }
        }

        player.Stop();
        SceneManager.LoadScene("MainMenu");

        yield return null; 
    }
}
