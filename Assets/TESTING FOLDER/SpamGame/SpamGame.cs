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
    public void BeginTheSpam ()
    {
        begun = true;
        StartCoroutine(SpammyGame());
	}
	
	void Update ()
    {
        gameBar.value = progress;
    }

    public IEnumerator SpammyGame()
    {
        Debug.Log("Start Spam Game");
        while (begun)
        {
            gameTimer -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space) && progress <= 1)
            {
                progress += positiveProgressIncrement;
            }
            if (progress >= 0 && progress <= 1)
            {
                progress -= negativeProgressIncrement * Time.deltaTime;
            }
            if (progress >= 1)
            {
                begun = false;
                abilityModifier = 5;
            }
            else if(gameTimer == 0 && progress < 1)
            {
                begun = false;
                abilityModifier = 1;
            }
            yield return abilityModifier;
        }
        
        CombatManager.Instance.UseCharacterAbility(abilityModifier);
        Debug.Log("Winner");
        progress = 0;
        gameTimer = 10;
        this.gameObject.SetActive(false);
        
        Debug.Log(abilityModifier);
        yield return null;
    }
}
