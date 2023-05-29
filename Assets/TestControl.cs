using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TestControl : MonoBehaviour
{
    public enum Setups
    {
        MobilePhoneProjection,
        MobilePhoneBaseline,
        TouchscreenProjection,
        TouchscreenBaseline
    }

    public Setups setups = Setups.MobilePhoneProjection;
    public string ip;
    public int port;
    public int userID;
    
    public GameObject leftHandProjection;
    public GameObject rightHandProjection;
    public bool isMobile;
    public bool isProjection;
    public GameObject touchscreen;
    
    public GameObject calibrationFinger;
    public GameObject gridGenerator;

    public GameObject device;

    public float upShift;
    
    public StreamWriter dataWriter;

    void Awake()
    {
        if (setups == Setups.MobilePhoneProjection)
        {
            leftHandProjection = GameObject.Find("LeftHandProjection");
            rightHandProjection = GameObject.Find("RightHandProjection");

            isMobile = true;
            isProjection = true;
            touchscreen = GameObject.Find("MobileTouchScreen");

            calibrationFinger = GameObject.FindWithTag("Thumb");
            
            var mesh = GameObject.FindGameObjectsWithTag("HandMesh");
            foreach (var hand in mesh)
            {
                hand.SetActive(false);
            }

            gridGenerator = GameObject.Find("MobileGridGenerator");

            device = GameObject.Find("MobileDevice");

            upShift = 0.0025f;
                
            GameObject.Find("BiTouchScreen").SetActive(false);
        }
        else if (setups == Setups.MobilePhoneBaseline)
        {
            leftHandProjection = GameObject.Find("LeftHandProjection");
            rightHandProjection = GameObject.Find("RightHandProjection");
            
            isProjection = false;
            isMobile = true;
            touchscreen = GameObject.Find("MobileTouchScreen");

            
            calibrationFinger = GameObject.FindWithTag("Thumb");
            
            gridGenerator = GameObject.Find("MobileGridGenerator");
            
            device = GameObject.Find("MobileDevice");
            
            upShift = 0.0025f;
            
            GameObject.Find("BiTouchScreen").SetActive(false);
        }
        else if (setups == Setups.TouchscreenProjection)
        {
            leftHandProjection = GameObject.Find("LeftHandProjection");
            rightHandProjection = GameObject.Find("RightHandProjection");

            isMobile = false;
            isProjection = true;
            touchscreen = GameObject.Find("DellTouchScreen");

            calibrationFinger = GameObject.FindWithTag("Index");

            var mesh = GameObject.FindGameObjectsWithTag("HandMesh");
            foreach (var hand in mesh)
            {
                hand.SetActive(false);
            }
            
            gridGenerator = GameObject.Find("DellGridGenerator");
            
            device = GameObject.Find("BiTouchScreen");
            
            upShift = 0.028f;
            
            GameObject.Find("MobileDevice").SetActive(false);
        }
        else if (setups == Setups.TouchscreenBaseline)
        {
            leftHandProjection = GameObject.Find("LeftHandProjection");
            rightHandProjection = GameObject.Find("RightHandProjection");
            
            isProjection = false;
            isMobile = false;
            touchscreen = GameObject.Find("DellTouchScreen");

            calibrationFinger = GameObject.FindWithTag("Index");

            gridGenerator = GameObject.Find("DellGridGenerator");
            
            device = GameObject.Find("BiTouchScreen");
            
            upShift = 0.028f;
            
            GameObject.Find("MobileDevice").SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
