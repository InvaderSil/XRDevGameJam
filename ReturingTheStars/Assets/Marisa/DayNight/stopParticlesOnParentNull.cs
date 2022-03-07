using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class stopParticlesOnParentNull : MonoBehaviour
{
    private void Update()
    {
        //Unparent the effect
        if(this.transform.parent = null)
        {
            UnityEngine.Debug.Log("noparent");
        }
        //Stop emitting particles
        //this.Stop();

        //Start coroutine that will check if all remaining particles are gone
    }
}
