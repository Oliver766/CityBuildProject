using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
// script by Oliver lancashire
// sid 1901981
public class Daysystem : MonoBehaviour
{
    [Header("Lights")]
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPresent lightingPresent;
    [Header("floats")]
    [SerializeField, Range(0, 24)] public float TimeOfDay;
    [Header("reference")]
    public GameManager manager;

    private void Update()
    {
        // check if objects if in game scene
        if (lightingPresent == null)
            return;
        // if game is playing then run day systen
        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime / 2;
            TimeOfDay %= 24;
        }
    }

    /// <summary>
    /// function that will updating light in gam object.
    /// </summary>
    /// <param name="timePercent"></param>
    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = lightingPresent.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = lightingPresent.FogColor.Evaluate(timePercent);
        if(DirectionalLight != null)
        {
            DirectionalLight.color = lightingPresent.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));

        }
    }
    // checking if light object is available
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;
        if(RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }







}
