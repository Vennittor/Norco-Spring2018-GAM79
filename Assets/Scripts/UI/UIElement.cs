using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElement : MonoBehaviour
{
    #region Variables
    public Image[] healthBars;
    public PlayerCharacter[] characters;
    #endregion

    #region Unity Functions
    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        UpdateHealthBar();
    }
    #endregion

    #region Functions   

    void Initialize()
    {
        /*healthBar = FindObjectOfType<Image>();
      
        character = FindObjectOfType<PlayerCharacter>();

        if (character)
        {
            print("Character found: " + character.characterName);
        }*/

        foreach(Image healthBar in healthBars)
        {
                
        }
    }

    void UpdateHealthBar()
    {
        //if (healthBars != null && characters != null)
            //healthBar.fillAmount = (float)character.currentHealth / (float)character.maxhealth;
    }
    #endregion
}
