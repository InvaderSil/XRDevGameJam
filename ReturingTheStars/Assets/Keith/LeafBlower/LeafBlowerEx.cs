using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafBlowerEx : GrabbableObject
{
    [SerializeField] private GameObject m_nozzlePoint;

    private PullableObject m_pullableObject;
    private PullableObject m_highlightedObject;

    [SerializeField] private AudioClip leafblowerSound;
    [SerializeField] private AudioClip triggerReleaseSound;
    private AudioSource m_audioSource;


    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public override void Update()
    {
        base.Update();
        HighlightPullableObject();
    }

    private void HighlightPullableObject()
    {
        if (m_highlightedObject != null)
        {
            //Debug.Log($"{m_highlightedObject.name} has been un-highlighted.");
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
            //Debug.Log($"{m_highlightedObject.name} has been highlighted.");
            
        }
      
    }

    public override void OnTriggerDown()
    {
        base.OnTriggerDown();

        if (m_audioSource == null)
        {
            Debug.LogError("The AudioSource in the player NULL!");
        }
        else
        {
            m_audioSource.clip = leafblowerSound;
            m_audioSource.Play();
        }

        RaycastHit hit;
        if (Physics.Raycast(m_nozzlePoint.transform.position, m_nozzlePoint.transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.TryGetComponent<PullableObject>(out m_pullableObject))
            {
                m_pullableObject.OnTriggerStart(m_nozzlePoint);
                Debug.Log($"{m_pullableObject.name} OnTriggerDown in LeafBlowerEx");
            }
        }
    }

    public override void OnTriggerUp()
    {
        base.OnTriggerUp();

        if (m_pullableObject != null)
        {
            Debug.Log($"{m_pullableObject.name} OnTriggerUp in LeafBlowerEx");
            m_pullableObject.OnTriggerEnd();

            if (m_audioSource == null)
            {
                Debug.LogError("The AudioSource in the player NULL!");
            }
            else
            {
                m_audioSource.clip = triggerReleaseSound;
                m_audioSource.Play();
            }
        }
    }

}
