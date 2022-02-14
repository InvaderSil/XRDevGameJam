using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrabbableObject : MonoBehaviour
{
    [SerializeField] private Color m_highlightedColor = Color.green;
    [SerializeField] private float m_throwForce = 1000f;
    
    private List<Vector3> m_trackedPositions = new List<Vector3>();
    private Color m_originalColor;
    private Renderer m_rend;
    private bool m_isHeld;
    protected bool m_isThrown = false;

    private int m_maxTrackedPositions = 15;

     private void Start()
    {
        m_rend = GetComponent<Renderer>();
        m_originalColor = m_rend.material.color;
    }

    public virtual void OnHoverStart()
    {
        m_rend.material.color = m_highlightedColor;
    }

    public virtual void OnHoverEnd()
    {
        m_rend.material.color = m_originalColor;
    }

    public virtual void Update()
    {
        if(m_isHeld)
        {
            if(m_trackedPositions.Count > m_maxTrackedPositions)
            {
                m_trackedPositions.RemoveAt(0);
            }
            m_trackedPositions.Add(transform.position);
        }
    }

    public void OnGrabStart(Grabber hand)
    {
         
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 50000f;
        fx.breakTorque = 50000f;
        fx.connectedBody = hand.GetComponent<Rigidbody>();

        // Make sure you create the FixedJoint first before making the parent
        transform.parent = hand.transform;
        m_trackedPositions.Clear();
        m_isHeld = true;
    }

    public virtual void OnGrabEnd()
    {
        transform.parent = null;
        if (GetComponent<FixedJoint>())
        {
            Destroy(GetComponent<FixedJoint>());
            Vector3 direction = m_trackedPositions[m_trackedPositions.Count - 1] - m_trackedPositions[0];
            GetComponent<Rigidbody>().AddForce(direction * m_throwForce);
            //Debug.Log(direction * m_throwForce);
            m_isHeld = false;
            m_isThrown = true;
        }

    }

    private void OnJointBreak(float breakForce)
    {
        transform.parent = null;
        
    }

    public virtual void OnTriggerDown()
    {

    }

    public virtual void OnTriggerUp()
    {

    }
}
