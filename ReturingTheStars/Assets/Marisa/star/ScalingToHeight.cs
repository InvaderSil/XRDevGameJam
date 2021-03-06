using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingToHeight : MonoBehaviour
{
    //[SerializeField] private float m_scale = 2f;

    private float maxHeight = 30;
    //private float initialHeight;
    private float currentHeight;
    private Vector3 scale;
    //private float currentScale;
    //private float tragetScale;
    //private bool scaling;

    // Start is called before the first frame update
    void Start()
    {
        //scale = transform.localScale;
        //initialHeight = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        currentHeight = transform.localPosition.y;

        
        //var scale = 2f * Time.deltaTime;

        if (currentHeight >= maxHeight)
        {
            transform.localScale = new Vector3(1, 1, 1) * 2f;
        }
        else
        {
            transform.localScale = new Vector3(.5f, .5f, .5f);
        }
    }
}
