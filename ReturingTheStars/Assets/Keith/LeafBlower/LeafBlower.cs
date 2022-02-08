using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafBlower : MonoBehaviour
{
    [SerializeField] private Handness m_hand;
    [SerializeField] private Transform m_nozzlePoint;

    private string m_pullObjectButton;

    //GrabbableObject m_hoveredObject;
    //GrabbableObject m_grabbedObject;
    PullableObject m_pullableObject;

    private void Start()
    {
        m_pullObjectButton = $"XRI_{m_hand}_TriggerButton";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(m_pullObjectButton))
        {
            RaycastHit hit;
            if (Physics.Raycast(m_nozzlePoint.position, m_nozzlePoint.forward, out hit, Mathf.Infinity))
            {
                Debug.Log($"Hit {hit.transform.name}");
                m_pullableObject = hit.transform.gameObject.GetComponent<PullableObject>();
                m_pullableObject.OnGrabStart(this);
            }
        }
        else if (Input.GetButtonUp(m_pullObjectButton))
        {
            m_pullableObject.OnGrabEnd();
        }
    }




}
