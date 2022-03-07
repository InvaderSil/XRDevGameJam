using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMovement : MonoBehaviour
{
    [SerializeField] private Handness m_hand;
    [SerializeField] private Transform m_XrRig;
    [SerializeField] private Transform m_director;
    [SerializeField] private float m_speed;

    private string m_horizontal;
    private string m_vertical;

    private AudioSource m_audioSource;

    // Start is called before the first frame update
    void Start()
    {
        m_horizontal = $"XRI_{m_hand}_Primary2DAxis_Horizontal"; 
        m_vertical = $"XRI_{m_hand}_Primary2DAxis_Vertical";

        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis(m_horizontal);
        float yAxis = Input.GetAxis(m_vertical);

        Vector3 playerForward = -m_director.forward;
        playerForward.y = 0; // else you are heading towards the y position if you are looking up
        playerForward.Normalize();

        Vector3 playerRight = m_director.right;
        playerRight.y = 0;
        playerRight.Normalize();

        m_XrRig.position += playerForward * yAxis * Time.deltaTime * m_speed;
        m_XrRig.position += playerRight * xAxis * Time.deltaTime * m_speed;

        if (yAxis > .1f || yAxis < -.1f || xAxis > .1f || xAxis < -.1f)
        {
            if (m_audioSource == null)
            {
                Debug.Log("The AudioSource in the player NULL!");
            }
            else
            {
                if (!m_audioSource.isPlaying)
                {
                    m_audioSource.Play();
                }
            }
        }
        else
        {
            if (m_audioSource != null)
            {
                m_audioSource.Pause();
            }
        }
    }
}
