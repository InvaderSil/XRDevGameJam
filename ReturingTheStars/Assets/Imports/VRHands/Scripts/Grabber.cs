using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Handness
{
    Left,
    Right
}

public class Grabber : MonoBehaviour
{
    [SerializeField]
    private Animator m_anim;
    [SerializeField] private Handness m_hand;
    private string m_gripButton;
    private string m_triggerButton;

    GrabbableObject m_hoveredObject;
    GrabbableObject m_grabbedObject;


    // Start is called before the first frame update
    void Start()
    {
        m_gripButton = $"XRI_{m_hand}_GripButton";
        m_triggerButton = $"XRI_{m_hand}_TriggerButton";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(m_gripButton))
        {
            m_anim.SetBool("Gripped", true);
            if(m_hoveredObject != null)
            {
                m_hoveredObject.OnGrabStart(this);
                m_grabbedObject = m_hoveredObject;
            }
        }
        else if(Input.GetButtonUp(m_gripButton))
        {
            m_anim.SetBool("Gripped", false);
            if(m_grabbedObject != null)
            {
                m_grabbedObject.OnGrabEnd();
                m_grabbedObject = null;
            }
        }

        if(m_grabbedObject != null && Input.GetButtonDown(m_triggerButton))
        {
            m_grabbedObject.OnTriggerDown();
        }

        if (m_grabbedObject != null && Input.GetButtonUp(m_triggerButton))
        {
            m_grabbedObject.OnTriggerUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GrabbableObject go = other.GetComponent<GrabbableObject>();
        //Debug.Log("OnTriggerEntered");
        if (go != null)
        {
            m_hoveredObject = go;
            go.OnHoverStart();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GrabbableObject go = other.GetComponent<GrabbableObject>();
        //Debug.Log("OnTriggerExit");
        if(go != null)
        {
            go.OnHoverEnd();
            if(go == m_hoveredObject)
            { 
                m_hoveredObject = null; 
            }
        }
    }

}
