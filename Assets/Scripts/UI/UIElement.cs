using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElement : MonoBehaviour
{
    public Image healthBar;
    public Character character;  

    public void Update()
    {
        UpdateHealthBar();
    }
    
    public void UpdateHealthBar()
    {
        if(healthBar != null && character != null)
        healthBar.fillAmount = (float)character.currentHealth / (float)character.maxhealth;
    }
}
