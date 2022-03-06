using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutScript : MonoBehaviour
{
    public GameObject stars;
    public GameObject constellation;
    public float fadeSpeed;

    private LightingManager lightingManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOutObject(fadeSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        //if (lightingManagerScript.TimeOfDay >= lightingManagerScript.sunriseTime)
        //{
        //    StartCoroutine(FadeOutObject(fadeSpeed));
        //}

    }

    public IEnumerator FadeOutObject(float fadeSpeed)
    {
        Renderer rend = stars.transform.GetComponent<Renderer>();
        Color matColor = rend.material.color;
        float alphaValue = rend.material.color.a;

        while (alphaValue > 0f)
        {
            alphaValue -= Time.deltaTime / fadeSpeed;
            matColor = new Color(matColor.r, matColor.g, matColor.b, alphaValue);
            yield return new WaitForSeconds(fadeSpeed);
        }
        matColor = new Color(matColor.r, matColor.g, matColor.b, 0f);
    }
}
