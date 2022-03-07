using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainHandler : MonoBehaviour
{
    [SerializeField] private SceneTransitionManager m_sceneManager;
    [SerializeField] private string m_startingScene;


    public void YesClickHandler()
    {
        Debug.Log("Yes Click is clicked.");

        if (!string.IsNullOrWhiteSpace(m_startingScene))
        {
            m_sceneManager.LoadScene(m_startingScene);
        }
    }

    public void NoClickHandler()
    {
        Debug.Log("No Click is clicked.");

        Application.Quit();
    }

}
