using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneTransition : MonoBehaviour
{
    [SerializeField] private SceneTransitionManager m_sceneManager;
    
    private bool m_isLoading = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !m_isLoading)
        {
            m_isLoading = true;
            m_sceneManager.LoadScene();
        }
    }

}
