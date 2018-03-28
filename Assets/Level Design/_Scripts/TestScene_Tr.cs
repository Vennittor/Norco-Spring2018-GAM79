using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TestScene_Tr : MonoBehaviour
{
    private GameObject player; 

    void Start()
    {
        player = FindObjectOfType<GameObject>();
        player.gameObject.SetActive(true);
    }

    public void LoadCombatScene()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, 10))
            print("There is something in front of the object!");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, 100))
            print("Hit something!");

        SceneManager.LoadSceneAsync("CombatTest"); 
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Player")
        {
            LoadCombatScene();
            player.gameObject.SetActive(false);
        }
    }
}