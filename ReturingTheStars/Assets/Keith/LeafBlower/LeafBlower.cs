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
    private PullableObject m_pullableObject;

    private void Start()
    {
        m_pullObjectButton = $"XRI_{m_hand}_TriggerButton";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(m_pullObjectButton))
        {
            Debug.DrawRay(m_nozzlePoint.position, m_nozzlePoint.forward * 100f, Color.red, 3f);
            RaycastHit hit;
            if (Physics.Raycast(m_nozzlePoint.position, m_nozzlePoint.forward, out hit, Mathf.Infinity))
            {
                //Debug.Log($"Nozzle: {m_nozzlePoint.position}, directrion: {m_nozzlePoint.forward} ");

                //Debug.Log($"Hit {hit.transform.name} from LeafBlower.");
                //m_pullableObject = hit.transform.gameObject.TryGetComponent<PullableObject>();
                if(hit.transform.gameObject.TryGetComponent<PullableObject>(out m_pullableObject))
                {
                    //m_pullableObject.OnTriggerStart(this);
                    //Debug.Log($"{m_pullableObject.name} has been captured");
                }
                
            }
        }
        else if (Input.GetButtonUp(m_pullObjectButton))
        {
            if (m_pullableObject != null)
            {
                //Debug.Log($"{m_pullableObject.name} has been released");
                m_pullableObject.OnTriggerEnd();
            }
            //else
            //{
            //    //Debug.Log("No valid pullalbeObject");
            //}
        }
    }




}
