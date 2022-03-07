using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryAgainManager : MonoBehaviour
{

    [SerializeField] private GameObject m_tryAgainUi;
    [SerializeField] private GameObject m_laserPointer;
    [SerializeField] private GameObject m_movement;

    
    public void EnableUI()
    {
        // Need to turn on UI
        m_tryAgainUi.SetActive(true);

        // Turn on Laser Pointer
        m_laserPointer.SetActive(true);

        // Disable movement
        m_movement.GetComponent<VRMovement>().enabled = false;
    }
}
