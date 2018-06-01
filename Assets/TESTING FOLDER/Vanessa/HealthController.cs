using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public BackForthController healthBar;
    public float healthChange;
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            healthBar.health += healthChange;
        }
        if (Input.GetKey(KeyCode.S))
        {
            healthBar.health -= healthChange;
        }
    }
}
