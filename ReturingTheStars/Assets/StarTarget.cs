using UnityEngine;

public class StarTarget : MonoBehaviour
{

    [SerializeField] private FadeScreen m_fader;

    
    public void TriggerHighlight()
    {
        GetComponent<MeshRenderer>().enabled = true;
        m_fader.FadeIn();
    }
}
