using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToClick : MonoBehaviour
{
    private float time = -1; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time != -1)
            time += Time.deltaTime;
    }

    public void startTimer()
    {
        time = 0;
    }

    public string endTimer()
    {
        return time.ToString();
    }
    
}
