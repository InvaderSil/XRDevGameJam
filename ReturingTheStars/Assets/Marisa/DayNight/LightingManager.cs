using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//Update method only gets called when something in the scene changes
[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    protected virtual bool DoubleBuffered { get; set; }

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

    [SerializeField] private TryAgainManager m_tryAgainManager;

    public SkyboxBlender skyboxScript;

    [SerializeField] private AudioClip roosterSound;
    private AudioSource m_audioSource;
    private bool roosterCrowed;

    public GameObject ObjectsToFade;
    private bool faded;


    private bool m_skyBoxBlended = false;

    private bool m_showingTryAgainUI = false;

    private GameObject[] stars;

    public Material fadeMaterial;

    private void Start()
    {
        DoubleBuffered = true;

        TimeOfDay = startHour;

        m_audioSource = GetComponent<AudioSource>();
        roosterCrowed = false;

        faded = false;

        
    }

    private void Awake()
    {
        skyboxScript.StopSkyboxBlend(true);
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
            //Debug.Log("TimeOfDay1 = " + TimeOfDay);
            TimeOfDay %= 24; //clamp between 0-24

            //Debug.Log("TimeOfDay2 = " + TimeOfDay);
            UpdateLighting( TimeOfDay / 24f );

            if ( TimeOfDay >= sunriseTime )
            {
                HandleSkyBoxBlending();
                HandleFadingObjects();
                HandleAudio();

                HandleRetry();
            }
        }
        else
        {
            UpdateLighting( TimeOfDay / 24f );
        }
        if(faded == true)
        {
            //Debug.Log("DESTROY EVERYTHING~!!!!! please");
            //Destroy(ObjectsToFade, 5f);
        }
    }

    private void HandleAudio()
    {
        if (!roosterCrowed)
        {
            ProcessAudioPlay();
            roosterCrowed = true;
        }
    }

    private void HandleFadingObjects()
    {
        if (!faded)
        {
            FadeOutObject();
            FadeStars();
            faded = true;
        }
    }

    private void HandleSkyBoxBlending()
    {
        if (!m_skyBoxBlended)
        {
            skyboxScript.SkyboxBlend();
            m_skyBoxBlended = true;
        }
        
    }

    private void HandleRetry()
    {
        if (!m_showingTryAgainUI)
        {
            m_showingTryAgainUI = true;

            m_tryAgainManager.EnableUI();
        }
         
    }

    private void UpdateLighting(float timePercent)
    {
        //Debug.Log("TimePercent = " + timePercent);

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
            m_audioSource.PlayDelayed(1f);
        }
    }

    //public IEnumerator FadeOutObject(float fadeSpeed)
    public void FadeOutObject()
    {
        Shader transparent = Shader.Find("Transparent/Diffuse");

        MeshRenderer[] meshRenderers = ObjectsToFade.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer mr in meshRenderers)
        {
            Material[] materials = mr.materials;
            foreach(Material m in materials)
            {
                if (m.shader != null)
                {
                    //make it a transparent one?
                    m.CopyPropertiesFromMaterial(fadeMaterial);
                    m.DOFade(0, 5f);
                    //Destroy(this, 5f);
                }
                m.DOFade(0, 3f);
                //Destroy(this, 5f);
            }
        }
    }

    private void FadeStars()
    {
        stars = GameObject.FindGameObjectsWithTag("star");

        foreach (GameObject star in stars)
        {
            MeshRenderer[] meshRenderers = star.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mr in meshRenderers)
            {
                Material[] materials = mr.materials;
                foreach (Material m in materials)
                {
                    if (m.shader != null)
                    {
                        //make it a transparent one?
                        m.CopyPropertiesFromMaterial(fadeMaterial);
                        m.DOFade(0, 4f);
                        Destroy(star, 4f);
                    }
                }
            }
        }
    }
}
