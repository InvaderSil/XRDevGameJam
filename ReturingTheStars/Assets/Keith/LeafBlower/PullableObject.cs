using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class PullableObject : MonoBehaviour
{
    [SerializeField] private Color m_highlightedColor = Color.green;
    [SerializeField] private float m_shootForce = 200f;
    //[SerializeField] private Material m_outlineMaterial;
    //[SerializeField] private float m_outlineScaleFactor;
    //[SerializeField] private Color m_outlineColor;
    //private Renderer m_outlineRenderer;


    private Color m_originalColor;
    private Renderer m_rend;
    private Transform m_blowerPos;
    private DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> m_dotweener;

    private void Start()
    {
        m_rend = GetComponent<Renderer>();
    }

    public virtual void OnHoverStart()
    {
        //m_rend.material.color = m_highlightedColor;
        //m_outlineRenderer.enabled = true;
        GetComponent<OutlineScript>().ToggleOutline();
    }

    public virtual void OnHoverEnd()
    {
        //m_rend.material.color = m_originalColor;
        //m_outlineRenderer.enabled = false;
        GetComponent<OutlineScript>().ToggleOutline();
    }

    public void OnTriggerStart(GameObject nozzlePoint)
    {
        //transform.parent = hand.transform;
        GetComponent<Rigidbody>().isKinematic = true;

        m_blowerPos = nozzlePoint.transform;

        //Debug.Log("m_blowerPos = " + m_blowerPos);

        var boxCollider = GetComponent<BoxCollider>();
        var renderer = GetComponent<Renderer>();
        var estimatedRadius = renderer.bounds.size / 2;
        //var estimatedRadius = boxCollider.size/2;

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
        //m_trackedPositions.Clear();
        //m_isHeld = true;
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

            


            var spawnStar = GetComponent<SpawnStar>();
            if(spawnStar == null)
            {
                Debug.Log("spawnStar not found");

            }
            else
            {
                spawnStar.OnStartStarDeath();
            }

            //if(TryGetComponent<SpawnStar>(out spawnStar))
            //{
            //    Debug.Log("Starting Star Death");
            //    spawnStar.OnStartStarDeath();
            //}

        }

    }

    private void OnJointBreak(float breakForce)
    {
        transform.parent = null;

    }

    
}
