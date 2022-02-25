using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private string m_sceneName;
    [SerializeField] private Timer m_timer;
    [SerializeField] private FadeScreen m_fadeScreen;

    private bool m_isLoading = false;

    private void Start()
    {
        if (m_timer != null)
        {
            m_timer.StartTimer();
        }
    }

    private void Update()
    {
        if (m_timer != null)
        {
            if (m_timer.IsComplete && !m_isLoading)
            {
                m_isLoading = true;
                
                LoadScene(m_sceneName);
            }
        }
    }

    /// <summary>
    /// Loads the Scene Name
    /// </summary>
    public void LoadScene()
    {
        LoadScene(m_sceneName);
    }

    /// <summary>
    /// Loades the passed in scene name
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        m_fadeScreen.FadeOut();

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        asyncOp.allowSceneActivation = false;

        float timer = 0;

        while(timer <= m_fadeScreen.fadeDuration && !asyncOp.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        asyncOp.allowSceneActivation = true;
        
        
    }
}
