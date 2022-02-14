using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StarCapture : MonoBehaviour
{
    [SerializeField] private float m_pullSpeed = 3f;

    private DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> m_dotweener;
    private bool m_isCaputing = false;
    private bool m_isComplete = false;

    

    // Update is called once per frame
    void Update()
    {
        ProcessCapture();
    }

    private void ProcessCapture()
    {
        if (m_dotweener == null)
        {
            return;
        }

        if (!m_dotweener.IsPlaying() && m_isCaputing)
        {
            // Assumed that the animation is done
            m_isCaputing = false;
            m_isComplete = true;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // We only want one object here
        if(m_isComplete)
        {
            return;
        }

        // pull the other to the center
        m_dotweener = other.transform.DOMove(transform.position, m_pullSpeed);
        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        other.GetComponent<Collider>().enabled = false;
        m_isCaputing = true;
    }

}
