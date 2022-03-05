using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : PullableObject
{
    [SerializeField] private float m_timeToDeath = 5f;

    public Timer TimerObject { get; set; }
    

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = StarState.Ready;
    }

    public void DestorySelf()
    {
        if (CurrentState != StarState.Caught)
        {
            //Destroy(gameObject.transform.parent);
            Destroy(gameObject);
        }
    }

    public StarState GetCurrentState()
    {
        return CurrentState;
    }

    public override void OnTriggerEnd()
    {
        base.OnTriggerEnd();
        if(CurrentState == StarState.Flying)
        {
            StartTimer();
        }
    }

    public void Capture()
    {
        CurrentState = StarState.Caught;
    }

    public void StartTimer()
    {
        TimerObject = gameObject.AddComponent<Timer>();
        TimerObject.WaitTime = m_timeToDeath; // TODO
        TimerObject.StartTimer();
    }

    public bool IsTimerCompleted()
    {
        if (TimerObject == null)
        {
            return false;

        }

        var result = TimerObject.IsComplete;
        if (!result)
        {
            return false;
        }

        TimerObject = null;
        return true;
    }
}

public enum StarState
{
    none,
    Ready,
    Flying,
    Caught
}
