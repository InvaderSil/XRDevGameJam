using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float WaitTime;

    public bool IsComplete { get; private set; }


    public Timer()
    {
        IsComplete = false;
    }


    public void StartTimer()
    {
        StartCoroutine(TimerCoroutine());
    }



    private IEnumerator TimerCoroutine()
    {
        float elapsedtime = 0;

        while(elapsedtime <= WaitTime)
        {
            elapsedtime += Time.deltaTime;
            yield return null;
        }

        IsComplete = true;

    }
}
