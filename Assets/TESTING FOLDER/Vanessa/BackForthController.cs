using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackForthController : MonoBehaviour {
    public float health;
    public float minSlide;
    public float maxSlide;
    private Slider slideBar;
    public bool increase = true;
    public float successZone;
    

    float midMin = 5;
    float midMax = 95;

    public GameObject backForthSlider;
    public Slider slider;
    public Image completionArea;

    public float fillTime = 1;
    float fillinterval;

    [Range(0, 100)]
    float midPoint;
    public float distanceBetweenInPoints;

    public float startDelay = 0.5f;

    bool going;

    private void Start()
    {
        slideBar = GetComponent<Slider>();
        slideBar.maxValue = maxSlide;
        slideBar.minValue = minSlide;
        fillinterval = (maxSlide - minSlide) / fillTime;
        Debug.Log("end me");
    }

    public void LetsBegin()
    {
        StartCoroutine(BackForthSlider());
    }

    private IEnumerator BackForthSlider()
    {
        float modifiedEffect = 1.0f;
        slider.value = 0;
        midPoint = Random.Range(midMin, midMax);

        float sliderSize = slider.GetComponent<RectTransform>().sizeDelta.y;
        sliderSize *= midPoint * 0.01f;
        sliderSize -= slider.GetComponent<RectTransform>().sizeDelta.y / 2;
        completionArea.GetComponent<RectTransform>().localPosition = Vector3.zero + (Vector3.right * sliderSize);

        completionArea.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, distanceBetweenInPoints * 2 * (backForthSlider.GetComponent<RectTransform>().sizeDelta.y * 0.01f));

        yield return new WaitForSeconds(startDelay);


        going = true;
        while (going)
        {
            //slider.value += Time.deltaTime * fillTime;
            //(max - min)/fillTime
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("make it stop");
                float width = distanceBetweenInPoints * 2;

                float minVal = midPoint - width / 2;
                float maxVal = midPoint + width / 2;
                Debug.Log(minVal + maxVal);
                print(minVal + ", " +maxVal);

                float val = slider.value;
                Debug.Log(val);
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
            if(increase)
            {
                //increase
                slideBar.value += Time.deltaTime * fillinterval;
                if (slideBar.value >= maxSlide)
                {
                    increase = false;
                }
            }
            else if(!increase)
            {
                //decrease
                slideBar.value -= Time.deltaTime * fillinterval;
                if (slideBar.value <= minSlide)
                {
                    increase = true;
                }
            }
            
            yield return modifiedEffect;
        }
        CombatManager.Instance.UseCharacterAbility(modifiedEffect);
        this.gameObject.SetActive(false);
        yield return null;
    }
}
