﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyLight : MonoBehaviour
{
    private Light dirLight;
    public GameObject playerObj;
    public GameObject enemyObj; 

    private void Start()
    {
        dirLight = GetComponent<Light>();
        playerObj = GameObject.Find("Player Party"); 
        enemyObj = GameObject.Find("Ra"); 
    }

    private void FixedUpdate()
    {
        if (enemyObj != null)
        {
            ChangeLightSetting();

            if (enemyObj.transform.gameObject == null)
            {
                Destroy(gameObject);
            }
        }
    }

    private void ChangeLightSetting()
    {
        float distanceA = Vector3.Distance(enemyObj.transform.position, playerObj.transform.position);
        Vector3 pos = enemyObj.transform.position - playerObj.transform.position;

        if (Vector3.Distance(pos, playerObj.transform.position) <= distanceA)
        {
            dirLight.color = Color.Lerp(Color.red, Color.blue, 5);
        }

        if (Vector3.Distance(playerObj.transform.position, pos) > distanceA)
        {
            dirLight.color = Color.Lerp(Color.blue, Color.blue, 5);
        }
        else
        {
            if (Vector3.Distance(pos, playerObj.transform.position) < distanceA)
            {
                dirLight.color = Color.Lerp(Color.red, Color.red, 5);
            }
        }
        return; 
    }
}