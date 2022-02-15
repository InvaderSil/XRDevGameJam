using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRTeleport : MonoBehaviour
{
    [SerializeField] private Handness m_hand;
    [SerializeField] private LineRenderer m_line;
    [SerializeField] private Transform m_XrRig;
    [SerializeField] private LayerMask m_teleportLayer;
    [SerializeField] private GameObject m_teleportIndicator;

    [SerializeField] private int m_lineResolution = 15;
    [SerializeField] private float m_lineCurvature = 1;
    [SerializeField] private Renderer m_blackScreen;

    private bool m_teleportLocked;
    private string m_teleportButton;
    private Vector3? m_hitPoint;

    // Start is called before the first frame update
    void Start()
    {
        m_teleportButton = $"XRI_{m_hand}_PrimaryButton";
        m_line.positionCount = m_lineResolution;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(m_teleportButton) && !m_teleportLocked)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, ~m_teleportLayer))
            {
                m_line.enabled = true;
                //m_line.SetPosition(0, transform.position);
                //m_line.SetPosition(1, hit.point);

                SetLinePositions(transform.position, hit.point);
                m_teleportIndicator.SetActive(true);
                m_hitPoint = hit.point;
                m_teleportIndicator.transform.position = m_hitPoint.Value;
            }
            else
            {
                m_hitPoint = null;
                m_teleportIndicator.SetActive(false);
                m_line.enabled = false;
            }
        }
        else if (Input.GetButtonUp(m_teleportButton) && !m_teleportLocked)
        {
            m_teleportIndicator.SetActive(false);
            m_line.enabled = false;
            if (m_hitPoint.HasValue)
            {
                StartCoroutine(FadedTeleport());
            }
        }

       
    }

    private void SetLinePositions(Vector3 start, Vector3 end)
    {
        Vector3 mid = Vector3.Lerp(start, end, .5f);
        mid.y += m_lineCurvature;

        for(var x = 0; x < m_lineResolution; x++)
        {
            float percent = x /(float)m_lineResolution;

            Vector3 startToMid = Vector3.Lerp(start, mid, percent);
            Vector3 MidToEnd = Vector3.Lerp(mid, end, percent);
            Vector3 curvePoint = Vector3.Lerp(startToMid, MidToEnd, percent);

            m_line.SetPosition(x, curvePoint);

        }
    }

    IEnumerator FadedTeleport()
    {
        m_teleportLocked = true;

        float currentTime = 0;

        while(currentTime < 1)
        {
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            m_blackScreen.material.color = Color.Lerp(Color.clear, Color.black, currentTime);
        }

        m_XrRig.position = m_hitPoint.Value;

        while(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
            m_blackScreen.material.color = Color.Lerp(Color.clear, Color.black, currentTime);
        }

        m_teleportLocked = false;
    }
}
