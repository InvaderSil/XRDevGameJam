using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafBlowerEx : GrabbableObject
{
    [SerializeField] private GameObject m_nozzlePoint;

    private PullableObject m_pullableObject;
    private PullableObject m_highlightedObject;
    
    public override void Update()
    {
        base.Update();
        HighlightPullableObject();
    }

    private void HighlightPullableObject()
    {
        if (m_highlightedObject != null)
        {
            Debug.Log($"{m_highlightedObject.name} has been un-highlighted.");
            m_highlightedObject.OnHoverEnd();
            m_highlightedObject = null;
        }

        RaycastHit hit;
        if (!Physics.Raycast(m_nozzlePoint.transform.position, m_nozzlePoint.transform.forward, out hit, Mathf.Infinity))
        {
            return;
        }

        if (hit.transform.gameObject.TryGetComponent<PullableObject>(out m_highlightedObject))
        {
            m_highlightedObject.OnHoverStart();
            Debug.Log($"{m_highlightedObject.name} has been highlighted.");
            
        }
      
    }

    public override void OnTriggerDown()
    {
        base.OnTriggerDown();

        RaycastHit hit;
        if (Physics.Raycast(m_nozzlePoint.transform.position, m_nozzlePoint.transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.TryGetComponent<PullableObject>(out m_pullableObject))
            {
                m_pullableObject.OnTriggerStart(m_nozzlePoint);
                Debug.Log($"{m_pullableObject.name} has been captured");
            }

        }
    }

    public override void OnTriggerUp()
    {
        base.OnTriggerUp();

        if (m_pullableObject != null)
        {
            Debug.Log($"{m_pullableObject.name} has been released");
            m_pullableObject.OnTriggerEnd();
        }
    }

}
