using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Rendering;
using UnityEngine;

public class skyboxControl : MonoBehaviour
{
    public float timer = 100.0f; 
    private Skybox skybox;
    private Shader duskShader; 
    public Color color;
    public Color col1;
    public Color col2;
    public int step = 1;

    public Color colorStart;
    public Color colorEnd;
    public float duration = 1.0F;

    public Material skyBox1;

    public Camera camera;

    void Start()
    {
        duskShader = GetComponent<Shader>(); 
        skyBox1 = GetComponent<Material>();
        timer = 100.0f;
        camera = GetComponent<Camera>();
        Material skybox = RenderSettings.skybox;
        skybox.SetInt("Rotation", step);
        RenderSettings.skybox = skybox;
        skybox.shader.name = "StarNight";
    }

    protected void LateUpdate()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration; // duration = 1;

        if(camera)
        {
            StartCoroutine(GetSkyBoxSettings());
        }
    }

    IEnumerator GetSkyBoxSettings()
    {
        step += 1;
        step %= 720; // rotation step of skybox in real time
        Material skybox = RenderSettings.skybox; // access skybox rendersettings 
        skybox.SetInt("_Rotation", step); // set the property name to change the rotation step if needed
        RenderSettings.skybox = skybox; // set the material of the skybox
        skybox.shader.name = "Dusk"; // find the name of material or assign it manually in the inspector

        if (skybox != null)
        {
            //set the exposure setting if wanting to 
            RenderSettings.skybox.SetFloat("_Exposure", Mathf.Sin(Time.time * Mathf.Deg2Rad * 40) + 1); 
            yield return new WaitForSeconds(40.0f);
            RenderSettings.skybox.SetColor("_Tint", Color.Lerp(colorStart, colorEnd, 20.0f));
            // access the color property in the shader, it is _Tint and not _Color, or _Tint Color
            RenderSettings.skybox.SetFloat("Rotation", 180 * Mathf.Abs(Time.time * Mathf.Deg2Rad * 2)); 
        }
        else
        {
            yield return null;
        }
    }
}