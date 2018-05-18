using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public float flickerSpeed = 10.0f;

    public float minBrightness = 0.0f;
    public float maxBrightness = 1.0f;

    public Color color1 = Color.white;
    public Color color2 = Color.white;

    private Light light = null;

	// Use this for initialization
	void Start ()
    {
        light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (light == null)
            return;
        
        light.intensity = Mathf.Lerp(light.intensity, Random.Range(minBrightness, maxBrightness), Time.deltaTime * flickerSpeed);

        light.color = Color.Lerp(light.color, Color.Lerp(color1, color2, Random.RandomRange(0.0f, 1.0f)), Time.deltaTime * flickerSpeed);
	}
}
