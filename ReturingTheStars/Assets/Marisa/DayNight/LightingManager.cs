using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//Update method only gets called when something in the scene changes
[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightPreset Preset;
    //Variables
    [SerializeField, Range(0, 24)] public float TimeOfDay;
    [SerializeField] private float startHour;
    [SerializeField] private float timeMultiplier; //time speed

    [SerializeField] private AnimationCurve lightChangeCurve;
    [SerializeField] private float maxSunLightIntensity;
    [SerializeField] private float maxMoonLightIntensity;
    [SerializeField] private Light moonLight;
    [SerializeField] private Color dayAmbientLight;
    [SerializeField] private Color nightAmbientLight;

    [SerializeField] public float sunriseTime;

    public SkyboxBlender skyboxScript;

    [SerializeField] private AudioClip roosterSound;
    private AudioSource m_audioSource;
    private bool roosterCrowed;

    //public float fadeSpeed;
    public GameObject ObjectsToFade;
    private bool faded;
    //private DG.Tweening.Core.TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> m_dotweener;

    public GameObject StarSpawners;
    private GameObject[] stars;

    private void Start()
    {
        TimeOfDay = startHour;

        m_audioSource = GetComponent<AudioSource>();
        roosterCrowed = false;

        faded = false;
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

            if ( TimeOfDay >= sunriseTime )
            {
                skyboxScript.SkyboxBlend();
                Destroy(StarSpawners.gameObject);

                stars = GameObject.FindGameObjectsWithTag("star");
                foreach(GameObject star in stars)
                {
                    Destroy(star.gameObject);
                }
                Debug.LogError("Destroy Stars");

                if (!faded)
                {
                    FadeOutObject();
                    faded = true;
                }
                if (!roosterCrowed)
                {
                    Debug.Log("PLAY THE DAMN SOUND");
                    ProcessAudioPlay();
                    roosterCrowed = true;
                }
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
            moonLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));

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

    private void ProcessAudioPlay()
    {
        if (m_audioSource == null)
        {
            Debug.LogError("The AudioSource in the player NULL!");
        }
        else
        {
            m_audioSource.clip = roosterSound;
            m_audioSource.Play();
        }
    }

    //public IEnumerator FadeOutObject(float fadeSpeed)
    public void FadeOutObject()
    {
        Shader transparent = Shader.Find("Transparent/Diffuse");

        //Renderer rend = ObjectsToFade.transform.GetComponent<Renderer>();
        //m_dotweener = rend.material.DOFade(0, 3f);

        MeshRenderer[] meshRenderers = ObjectsToFade.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer mr in meshRenderers)
        {
            Material[] materials = mr.materials;
            foreach(Material m in materials)
            {
                m.DOFade(0, 3f);

                //if (m.shader != null)
                //{
                //    // make it a transparent one?
                //    //m.shader = transparent;

                //    if (m.HasProperty("_Color"))
                //    {
                //        Debug.Log("Shader!");
                //    }
                //}
                //else
                //{
                //    Debug.Log("Regular Material Fade Out");
                //    m.DOFade(0, 3f);
                //}
            }
        }
    }
}
