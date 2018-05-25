using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamGame : MonoBehaviour
{
    public float gameTime = 5.0f;
    float gameTimer = 10.0f;
    public float positiveProgressIncrement = 0.06f;
    public float negativeProgressIncrement = 0.1f;
    public UnityEngine.UI.Slider gameBar;
    public float progress = 0;
    public bool begun = false;
    public float abilityModifier;

    // Use this for initialization
    void Awake()
    {
        gameBar = GetComponent<UnityEngine.UI.Slider>();
        gameTimer = gameTime;
    }
    void Start ()
    {
        begun = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        ButtonSpam();
        gameBar.value = progress;
        NegativeTimer();
        TimeLimit();
        Winner();
    }

    public void ButtonSpam()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (progress <= 1)
            {
                progress += positiveProgressIncrement;
               
            }
        }
    }

    public void NegativeTimer()
    {
        if (progress >= 0 && progress <=1)
        {
           progress -= negativeProgressIncrement * Time.deltaTime;
        }
    }

    public void TimeLimit()
    {
        if (begun == true)
        {
            gameTimer -= Time.deltaTime;
            /*if (gameTimer <= 0.0f)
              {
                  Debug.Log("Huck");
              }*/
        }
    }

    public void Winner()
    {
        if (progress >=1)
        {
            abilityModifier = 5;
            CombatManager.Instance.UseCharacterAbility(abilityModifier);
            Debug.Log("Winner");
            this.gameObject.SetActive(false);
        }
        else
        {
            abilityModifier = 1;
        }
    }
}
