using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopper : MonoBehaviour
{
    public bool doStopTimePlease = false;

    private float fixedDeltaTime;

    public float slowDownFactor = 0.05f;
    public float slowDownLerp = 1.2f;

    void Awake()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        if (doStopTimePlease)
        {
            StopTime();
        }
        else
        {
            ContinueTime();
        }

    }

    public void StopTime()
    {
        Time.timeScale          = slowDownFactor;
        Time.fixedDeltaTime     = Time.timeScale * 0.02f;
    }

    public void ContinueTime()
    {
        Time.timeScale += 1f / slowDownLerp * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }
}
