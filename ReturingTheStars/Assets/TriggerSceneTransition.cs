using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneTransition : MonoBehaviour
{
    [SerializeField] private string m_sceneName;
    [SerializeField] private float m_timeToLoad;
    [SerializeField] private Timer m_timer;

    private void Start()
    {
        if(m_timer != null)
        {
            m_timer.StartTimer();
        }
    }

    private void Update()
    {
        if(m_timer != null)
        {
            if(m_timer.IsComplete)
            {
                LoadScene(m_sceneName);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadScene(m_sceneName);
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAfterTimer(sceneName));
    }

    private IEnumerator LoadAfterTimer(string sceneName)
    {
        yield return new WaitForSeconds(m_timeToLoad);

        SceneManager.LoadSceneAsync(sceneName);
    }
}
