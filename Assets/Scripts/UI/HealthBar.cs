using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    #region Variables 
    private Image healthBar;
    private PlayerCharacter character;
    private UIManager instance;
    #endregion

    #region Unity Functions 

    void Start()
    {
        character = gameObject.GetComponent<PlayerCharacter>();
    }
    void Update()
    {
        UpdateHealthBar();

        if (character.uiManager != null && instance == null)
        {
            instance = gameObject.GetComponent<PlayerCharacter>().uiManager;

            if (instance != null)
                Initialize();

        }

    }
    #endregion

    #region Functions    

    void Initialize()
    {

        if (gameObject.GetComponent<PlayerCharacter>().characterName == ("Crusader"))
        {
            healthBar = instance.crusaderHealth;
        }

        if (gameObject.GetComponent<PlayerCharacter>().characterName == ("Alchemist"))
        {
            healthBar = instance.alchemistHealth;
        }

        if (gameObject.GetComponent<PlayerCharacter>().characterName == ("Female"))
        {
            healthBar = instance.hunterHealth;
        }

    }

    void UpdateHealthBar()
    {
        if (healthBar != null && character != null)
        {
            healthBar.fillAmount = (float)character.currentHealth / (float)character.maxHealth;
        }
    }
    #endregion

}
