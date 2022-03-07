using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    [SerializeField] private float m_defaultLength = 5.0f;
    [SerializeField] private GameObject m_dot;
    [SerializeField] private VrInputModule m_vrInputModule;

    private LineRenderer m_lineRenderer;

    private void Awake()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateLine();
    }


    private void UpdateLine()
    {
        float targetLength = m_defaultLength;

        RaycastHit hit = CreateRaycast(targetLength);

        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        if(hit.collider != null)
        {
            endPosition = hit.point;
        }

        m_dot.transform.position = endPosition;

        m_lineRenderer.SetPosition(0, transform.position);
        m_lineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;

        Ray ray = new Ray(transform.position, transform.forward);

        Physics.Raycast(ray, out hit, m_defaultLength);

        return hit;
    }
}
