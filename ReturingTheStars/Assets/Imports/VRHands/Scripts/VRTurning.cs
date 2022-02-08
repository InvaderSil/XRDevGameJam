using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRTurning : MonoBehaviour
{
    [SerializeField] private Handness m_hand;
    [SerializeField] private Transform m_XrRig;
    [SerializeField] private float m_turnSpeed;

    private string m_horizontalAxis;
    private bool m_hasTurned;

    // Start is called before the first frame update
    void Start()
    {
        m_horizontalAxis = $"XRI_{m_hand}_Primary2DAxis_Horizontal";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis(m_horizontalAxis) > 0.75f && !m_hasTurned)
        {
            m_hasTurned = true;
            m_XrRig.Rotate(Vector3.up * m_turnSpeed);
        }
        else if(Input.GetAxis(m_horizontalAxis) < -0.75f && !m_hasTurned)
        {
            m_hasTurned = true;
            m_XrRig.Rotate(Vector3.up * -m_turnSpeed);
        }
        else if(Mathf.Abs(Input.GetAxis(m_horizontalAxis))< 0.75f)
        {
            m_hasTurned = false;
        }
    }
}
