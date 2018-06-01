using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPoint : MonoBehaviour
{
    public UIManager uiManager;

    public GameObject actionSlider;
    public Slider slider;
    public Image completionArea;

    public float fillTime = 1;

    [Range(0, 100)]
    private float midPoint;
    public float distanceBetweenInPoints;

    public float startDelay = 0.5f;

    bool actionBarRunning = false;

    // Use this for initialization
    private void Start()
    {
        if (actionSlider == null)
        {
            actionSlider = uiManager.actionSlider;

            if (actionSlider == null)
            {
                actionSlider = GameObject.Find("Action Slider");
            }
        }

        if (actionSlider != null)
        {
            slider = actionSlider.GetComponent<Slider>();

            completionArea = actionSlider.transform.Find("Completion Area").GetComponent<Image>();
        }
        if (actionSlider == null)
        {
            Debug.LogError("CombatManager cannot find Action Slider gamObject");
        }
    }
    public IEnumerator ActionSlider()
    {
        actionSlider.SetActive(true);


        // Update is called once per frame
        UIManager.InputMode oldMode = uiManager.inputMode;
        actionBarRunning = true;
        uiManager.inputMode = UIManager.InputMode.BLOCKED;
        uiManager.GetComponentInChildren<CanvasGroup>().interactable = false;
        actionSlider.SetActive(true);

        float modifiedEffect = 1f;
        float midMin = 25;
        float midMax = 85;

        //Start
        slider.value = 0;
        slider.maxValue = fillTime;
        midPoint = Random.Range(midMin, midMax);
        //Create the visual for the stopping area
        float sliderSize = slider.GetComponent<RectTransform>().sizeDelta.y;
        sliderSize *= midPoint * 0.01f;
        sliderSize -= slider.GetComponent<RectTransform>().sizeDelta.y / 2;
        completionArea.GetComponent<RectTransform>().localPosition = Vector3.zero + (Vector3.up * sliderSize);

        completionArea.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, distanceBetweenInPoints * 2 * (actionSlider.GetComponent<RectTransform>().sizeDelta.y * 0.01f));

        yield return new WaitForSeconds(startDelay);

        //Do the thing
        bool going = true;
        while (going)
        {

            slider.value += Time.deltaTime * fillTime;

            if (Input.GetKeyDown(KeyCode.Space) /*|| Input.GetMouseButtonDown(0)*/)
            {

                float width = distanceBetweenInPoints * 2;

                float minVal = midPoint - width / 2;
                float maxVal = midPoint + width / 2;

                print(minVal + ", " + maxVal);

                float val = slider.value * 100;

                if (val >= minVal && val <= maxVal)
                {
                    print("you hit it!" + val);
                    modifiedEffect = 5f;
                }
                else
                {
                    print("ya bum!" + val);
                    modifiedEffect = 1f;
                }

                going = false;
            }

            yield return null;
            if (slider.value >= 1)
            {
                print("you didnt press anything");
                going = false;
            }
        }
        uiManager.inputMode = oldMode;
        uiManager.GetComponentInChildren<CanvasGroup>().interactable = true;
        Debug.Log("ACTION");

        yield return null;

        actionBarRunning = false;

        uiManager.actionSlider.SetActive(false);


    }
}
