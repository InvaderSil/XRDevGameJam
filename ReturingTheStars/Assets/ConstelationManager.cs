using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstelationManager : MonoBehaviour
{

    [SerializeField] private List<StarCapture> m_captureList = new List<StarCapture>();
    [SerializeField] private ParticleSystem m_fireworksParticls;

    private bool m_isFinished;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (IsConstelationComplete() && !m_isFinished)
        {
            HandleParticles();
            m_isFinished = true;
        }
    }

    private void HandleParticles()
    {
        m_fireworksParticls.Play();
        Debug.Log("Fire particle system here.");
    }

    private bool IsConstelationComplete()
    {
        var retBool = true;

        foreach (var item in m_captureList)
        {
            if (!item.IsComplete)
            {
                retBool = false;
                break;
            }
        }

        return retBool;
    }

}
