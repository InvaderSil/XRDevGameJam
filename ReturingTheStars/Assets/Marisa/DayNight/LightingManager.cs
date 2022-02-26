using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Update method only gets called when something in the scene changes
[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightPreset Preset;
    //Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    [SerializeField] private float startHour;
    [SerializeField] private float timeMultiplier; //time speed

    private void Start()
    {
    }

    private void Update()
    {
        if(Preset == null)
        {
            return;
        }
        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime * timeMultiplier;
            TimeOfDay %= 24; //clamp between 0-24
            UpdateLighting( TimeOfDay / 24f );
        }
        else
        {
            UpdateLighting( TimeOfDay / 24f );
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if(DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalLight.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        } else
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
