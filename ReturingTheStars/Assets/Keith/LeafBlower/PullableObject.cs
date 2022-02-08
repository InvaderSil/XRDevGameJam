using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class PullableObject : MonoBehaviour
{
    [SerializeField] private Color m_highlightedColor = Color.green;

    private Color m_originalColor;
    private Renderer m_rend;

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

    public void OnGrabStart(LeafBlower hand)
    {
        //transform.parent = hand.transform;
        GetComponent<Rigidbody>().isKinematic = true;

        var blowerPos = hand.GetComponentInChildren<NozzlePoint>();

        transform.DOMove(blowerPos.transform.position, 3f);

        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 50000f;
        fx.breakTorque = 50000f;
        fx.connectedBody = hand.GetComponent<Rigidbody>();

        // Make sure you create the FixedJoint first before making the parent
        transform.parent = hand.transform;
        //m_trackedPositions.Clear();
        //m_isHeld = true;
    }

    

    public virtual void OnGrabEnd()
    {
        transform.parent = null;
        if (GetComponent<FixedJoint>())
        {
            Debug.Log("In OnGrabEnd()");
            Destroy(GetComponent<FixedJoint>());
            //Vector3 direction = m_trackedPositions[m_trackedPositions.Count - 1] - m_trackedPositions[0];
            //GetComponent<Rigidbody>().AddForce(direction * m_throwForce);
            //Debug.Log(direction * m_throwForce);
            //m_isHeld = false;
            //m_isThrown = true;
            GetComponent<Rigidbody>().isKinematic = true;
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
