using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class PullableObject : MonoBehaviour
{

    [SerializeField] private float m_shootForce = 200f;
   
    private Transform m_blowerPos;
    private DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> m_dotweener;

    protected StarState CurrentState;

    private void Start()
    {
        CurrentState = StarState.none;
    }

    public virtual void OnHoverStart()
    {
        GetComponent<OutlineScript>().ToggleOutline();
    }

    public virtual void OnHoverEnd()
    {
        GetComponent<OutlineScript>().ToggleOutline();
    }

    public void OnTriggerStart(GameObject nozzlePoint)
    {
        GetComponent<Rigidbody>().isKinematic = true;

        m_blowerPos = nozzlePoint.transform;

        

        var boxCollider = GetComponent<BoxCollider>();
        var renderer = GetComponent<Renderer>();
        var estimatedRadius = renderer.bounds.size / 2;
        
        m_dotweener = transform.DOMove(m_blowerPos.position + estimatedRadius, 3f);
        
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 50000f;
        fx.breakTorque = 50000f;

        Rigidbody nozzleRB; 
        if (nozzlePoint.TryGetComponent<Rigidbody>(out nozzleRB))
        {
            fx.connectedBody = nozzleRB;
        }
        else
        {
            Debug.Log("failed to get RigidBody for FixedJoint in PullableObject");
        }
        // Make sure you create the FixedJoint first before making the parent
        transform.parent = nozzlePoint.transform;
        
    }

    

    public virtual void OnTriggerEnd()
    {
        transform.parent = null;

        if (TryGetComponent<FixedJoint>(out FixedJoint fj))
        {
            
            GetComponent<Rigidbody>().isKinematic = false;
                        
            Destroy(fj);

            if (m_dotweener.IsPlaying())
            {
                m_dotweener.Kill();
                GetComponent<Rigidbody>().useGravity = true;
                return;
            }

            Rigidbody localRB = GetComponent<Rigidbody>();
            localRB.useGravity = false;
            GetComponent<Rigidbody>().AddForce(m_blowerPos.forward * m_shootForce);

            CurrentState = StarState.Flying;         

        }

    }

    private void OnJointBreak(float breakForce)
    {
        transform.parent = null;

    }

    
}
