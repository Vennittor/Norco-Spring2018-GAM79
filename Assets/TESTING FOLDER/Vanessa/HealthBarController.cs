using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {
    public float health;
    public float minHealth;
    public float maxHealth;
    private Slider healthBar;
    public bool increase = true;
    public float successZone;

    float midMin = 5;
    float midMax = 95;

    public GameObject actionSlider;
    public Slider slider;
    public Image completionArea;

    public float fillTime = 1;

    [Range(0, 100)]
    public float midPoint;
    public float distanceBetweenInPoints;

    public float startDelay = 0.5f;

    bool going = true;



    private void Start()
    {
        healthBar = GetComponent<Slider>();
    }
    void Update()
    {
        UpdateHealthBar();
        health = Mathf.Clamp(health, minHealth, maxHealth);
    }
    void UpdateHealthBar()
    {
        if (increase == true)
        {
            healthBar.value += health * Time.deltaTime;
            if (healthBar.value >= 100f)
            {
                increase = false;
            }
        }
        else 
        {
            healthBar.value -= health * Time.deltaTime;
            if (healthBar.value <= 0f)
            {
                increase = true;
            }
        }
    }
    private IEnumerator ActionSlider()
    {
        float modifiedEffect = 0.0f;
        slider.value = 0;
        slider.maxValue = fillTime;
        midPoint = Random.Range(midMin, midMax);

        float sliderSize = slider.GetComponent<RectTransform>().sizeDelta.y;
        sliderSize *= midPoint * 0.01f;
        sliderSize -= slider.GetComponent<RectTransform>().sizeDelta.y / 2;
        completionArea.GetComponent<RectTransform>().localPosition = Vector3.zero + (Vector3.up * sliderSize);

        completionArea.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, distanceBetweenInPoints * 2 * (actionSlider.GetComponent<RectTransform>().sizeDelta.y * 0.01f));

        yield return new WaitForSeconds(startDelay);

        
        while (going)
        {
            slider.value += Time.deltaTime * fillTime;
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                float width = distanceBetweenInPoints * 2;

                float minVal = midPoint - width / 2;
                float maxVal = midPoint + width / 2;

                print(minVal + ", " +maxVal);

                float val = slider.value * 100;

                if (val >= minVal && val <= maxVal)
                {
                    print("you hit it!" + val);
                    modifiedEffect = 5f;
                }
                else
                {
                    print("you suck" + val);
                    modifiedEffect = 1f;
                }

                going = false;
            }

            yield return null;
            if (slider.value >= 1)
            {
                going = false;
            }
            yield return null;
        }
    }
}
