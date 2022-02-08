using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Haptics : MonoBehaviour
{
    [SerializeField] private XRNode m_xrNode;
    private InputDevice m_VRController;

    private bool m_canVibrate;

    // Start is called before the first frame update
    void Start()
    {
        m_VRController = InputDevices.GetDeviceAtXRNode(m_xrNode);

        if(m_VRController.isValid)
        {
            HapticCapabilities hapcap = new HapticCapabilities();
            m_VRController.TryGetHapticCapabilities(out hapcap);

            if(hapcap.supportsImpulse)
            {
                m_canVibrate = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Vibrate(float strength, float duration)
    {
        if (m_canVibrate)
        {
            m_VRController.SendHapticImpulse(0, strength, duration);
        }
    }
}
