using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineScript : MonoBehaviour
{

    [SerializeField] private Material m_outlineMaterial;
    [SerializeField] private float m_outlineScaleFactor;
    [SerializeField] private Color m_outlineColor;
    private Renderer m_outlineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_outlineRenderer = CreateOutline(m_outlineMaterial, m_outlineScaleFactor, m_outlineColor);
        m_outlineRenderer.enabled = false;
    }

    public void ToggleOutline()
    {
        m_outlineRenderer.enabled = !m_outlineRenderer.enabled;
    }

    private Renderer CreateOutline(Material outlineMaterial, float outlineScaleFactor, Color outlineColor)
    {
        GameObject outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        Renderer rend = outlineObject.GetComponent<Renderer>();
        

        rend.material = m_outlineMaterial;
        rend.material.SetColor("_OutlineColor", outlineColor);
        rend.material.SetFloat("_Scale", outlineScaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineObject.GetComponent<OutlineScript>().enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;
        outlineObject.GetComponent<Rigidbody>().isKinematic = true;

        outlineObject.transform.parent = this.gameObject.transform;

        rend.enabled = false;

        return rend;
    }

    
}
