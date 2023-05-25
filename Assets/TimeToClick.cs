using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToClick : MonoBehaviour
{
    private System.DateTime startTime;
    private System.DateTime endTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void startTimer()
    {
        startTime = DateTime.UtcNow;
    }

    public TimeSpan endTimer()
    {
        endTime = DateTime.UtcNow;
        System.TimeSpan ts = endTime - startTime;
        return ts;
    }
}
