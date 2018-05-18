using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class Return : MonoBehaviour
{
    private void Update()
    {
        ReturnToMenu(); 
    }

	public void ReturnToMenu()
    {
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene(0); 
        }
    }
}
