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

    [SerializeField] private AnimationCurve lightChangeCurve;
    [SerializeField] private float maxSunLightIntensity;
    [SerializeField] private float maxMoonLightIntensity;
    [SerializeField] private Light moonLight;
    [SerializeField] private Color dayAmbientLight;
    [SerializeField] private Color nightAmbientLight;

    [SerializeField] private float sunriseTime;

    public SkyboxBlender skyboxScript;

    private void Start()
    {
        TimeOfDay = startHour;
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
            if( TimeOfDay > sunriseTime)
            {
                skyboxScript.SkyboxBlend();
            }
        }
        else
        {
            UpdateLighting( TimeOfDay / 24f );
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalLight.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));

            //if the directionlight is pointing down the dotProduct = 1, horizontally = 0, and up = -1
            float dotProduct = Vector3.Dot(DirectionalLight.transform.forward, Vector3.down);
            DirectionalLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
            moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
            RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
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
