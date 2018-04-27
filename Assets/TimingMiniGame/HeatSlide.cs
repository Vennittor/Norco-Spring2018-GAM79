using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatSlide : MonoBehaviour
{
    public Slider heatSlider;
    public Image completionArea;

    public float fillTime = 1;

    [Range(0,100)]
    public float midPoint;
    public float distanceBetweenInPoints;

    bool started = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && !started)
        {
            StartCoroutine(DoTheThing());
            started = true;
        }
    }

    IEnumerator DoTheThing()
    {
        //Start
        heatSlider.value = 0;
        heatSlider.maxValue = fillTime;

        //Create the visual for the stopping area
        float guh = heatSlider.GetComponent<RectTransform>().sizeDelta.x;
        guh *= midPoint * 0.01f;
        guh -= heatSlider.GetComponent<RectTransform>().sizeDelta.x / 2;
        completionArea.GetComponent<RectTransform>().localPosition = Vector3.zero + (Vector3.right * guh);

        completionArea.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, distanceBetweenInPoints * 2 * (heatSlider.GetComponent<RectTransform>().sizeDelta.x * 0.01f));

        yield return null;
        
        //Do the thing
        bool going = true;
        while (going)
        {
            heatSlider.value += Time.deltaTime * fillTime;
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                float width = distanceBetweenInPoints * 2;

                float minVal = midPoint - width/2;
                float maxVal = midPoint + width/2;

                print(minVal + ", " + maxVal);

                float val = heatSlider.value * 100;

                if (val >= minVal && val <= maxVal)
                {
                    print("you hit it!" + val);
                }
                else
                {
                    print("ya bum!" + val);
                }
                
                going = false;
            }

            yield return null;
            if (heatSlider.value >= 1)
            {
                print("you didnt press anything");
                going = false;
            }
        }
        
        started = false;
    }
}
