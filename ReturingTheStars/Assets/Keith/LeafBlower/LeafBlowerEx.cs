using System;
using UnityEngine;

public class LeafBlowerEx : GrabbableObject
{
    [SerializeField] private GameObject m_nozzlePoint;

    private PullableObject m_pullableObject;
    private PullableObject m_highlightedObject;

    [SerializeField] private AudioClip leafblowerSound;
    [SerializeField] private AudioClip triggerReleaseSound;
    private AudioSource m_audioSource;

    private bool starTargetHighlighted = false;

    public override void Start()
    {
        base.Start();
        m_audioSource = GetComponent<AudioSource>();
    }

    public override void Update()
    {
        base.Update();
        HighlightPullableObject();
        // TODO Put in call to highlight target star
        HighlightStarTarget();
    }

    private void HighlightStarTarget()
    {
        if (((Star)m_pullableObject)?.GetCurrentState() == StarState.Ready)
        {

            RaycastHit hit;
            if (!Physics.Raycast(m_nozzlePoint.transform.position, m_nozzlePoint.transform.forward, out hit, Mathf.Infinity))
            {
                return;
            }

            StarTarget targetedStar;
            if (hit.transform.gameObject.TryGetComponent<StarTarget>(out targetedStar) && !starTargetHighlighted)
            {
                starTargetHighlighted = true;
                targetedStar.TriggerHighlight();


            }
            else
            {
                starTargetHighlighted = false;
            }
        }
    }

    private void HighlightPullableObject()
    {
        if (m_highlightedObject != null)
        {       
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
        }
      
    }

    public override void OnTriggerDown()
    {
        base.OnTriggerDown();

        ProcessAudioPlay();
        ProcessHit();
    }

    private void ProcessHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_nozzlePoint.transform.position, m_nozzlePoint.transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.TryGetComponent<PullableObject>(out m_pullableObject))
            {
                m_pullableObject.OnTriggerStart(m_nozzlePoint);
            }

        }
    }

    private void ProcessAudioPlay()
    {
        if (m_audioSource == null)
        {
            Debug.LogError("The AudioSource in the player NULL!");
        }
        else
        {
            m_audioSource.clip = leafblowerSound;
            m_audioSource.Play();
        }
    }

    public override void OnTriggerUp()
    {
        base.OnTriggerUp();

        if (m_pullableObject != null)
        {
            m_pullableObject.OnTriggerEnd();
            ProcessAudioStop();
        }
    }

    private void ProcessAudioStop()
    {
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
