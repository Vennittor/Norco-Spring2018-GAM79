using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public UnityEngine.UI.Slider gameBar;
    float gameTimer = 10.0f;
    public float progress = 0;
    public bool begun = false;

    // Use this for initialization
    void Start ()
    {
        gameBar = GetComponent<UnityEngine.UI.Slider>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        gameBar.value = progress;
        buttonSpam();
        timeLimite();
	}

    public void buttonSpam()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (progress <= 100)
            {
                begun = true;
                if (begun == true)
                {
                    gameTimer -= Time.deltaTime;
                    if (gameTimer <= 0.0f)
                    {
                        Debug.Log("Huck");
                    }
                }
            }
        }
    }

    public void timeLimite()
    {
        if (progress <= 100 && begun == true)
        { 
            progress += .15f * Time.deltaTime;

        }
    }

}
