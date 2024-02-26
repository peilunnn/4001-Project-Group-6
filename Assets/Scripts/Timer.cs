using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshPro textField;

    protected float currentTime;
    protected float startTime;
    protected TimeSpan timespan;
    protected bool running;

    public virtual void ResetTimer()
    {
        startTime = Time.time;
        currentTime = 0f;
    }

    public virtual void Begin()
    {
        ResetTimer();
        Resume();
    }

    public virtual void Resume()
    {
        running = true;
    }

    public virtual void Stop()
    {
        running = false;
    }

    protected virtual void Update()
    {
        if (running)
        {
            currentTime = startTime - Time.time;
        }

        timespan = TimeSpan.FromSeconds(currentTime);
        textField.text = timespan.ToString(@"mm\:ss\:fff");
    }
}
