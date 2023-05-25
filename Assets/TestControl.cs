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
    public GameObject touchscreen;
    
    public GameObject calibrationFinger;
    public GameObject gridGenerator;
    
    
    public StreamWriter touchWriter;
    public StreamWriter dataWriter;

    void Awake()
    {
        if (setups == Setups.MobilePhoneProjection)
        {
            leftHandProjection = GameObject.Find("LeftHandProjection");
            rightHandProjection = GameObject.Find("RightHandProjection");

            isMobile = true;
            touchscreen = GameObject.Find("MobileTouchScreen");
            
            GameObject.Find("baseMeshHand_Left_GRP").SetActive(false);
            GameObject.Find("baseMeshHand_Right_GRP").SetActive(false);

            calibrationFinger = GameObject.FindWithTag("Thumb");

            gridGenerator = GameObject.Find("MobileGridGenerator");
            
            GameObject.Find("BiTouchScreen").SetActive(false);
        }
        else if (setups == Setups.MobilePhoneBaseline)
        {
            isMobile = true;
            touchscreen = GameObject.Find("MobileTouchScreen");
            
            GameObject.Find("baseMeshHand_Left_GRP").SetActive(true);
            GameObject.Find("baseMeshHand_Right_GRP").SetActive(true);
            
            calibrationFinger = GameObject.FindWithTag("Thumb");
            
            gridGenerator = GameObject.Find("MobileGridGenerator");
            
            GameObject.Find("BiTouchScreen").SetActive(false);
        }
        else if (setups == Setups.TouchscreenProjection)
        {
            leftHandProjection = GameObject.Find("LeftHandProjection");
            rightHandProjection = GameObject.Find("RightHandProjection");

            isMobile = false;
            touchscreen = GameObject.Find("DellTouchScreen");
            
            GameObject.Find("baseMeshHand_Left_GRP").SetActive(false);
            GameObject.Find("baseMeshHand_Right_GRP").SetActive(false);
            
            calibrationFinger = GameObject.FindWithTag("Index");
            
            gridGenerator = GameObject.Find("DellGridGenerator");
            
            GameObject.Find("MobileDevice").SetActive(false);
        }
        else if (setups == Setups.TouchscreenBaseline)
        {
            isMobile = false;
            touchscreen = GameObject.Find("DellTouchScreen");
            
            GameObject.Find("baseMeshHand_Left_GRP").SetActive(true);
            GameObject.Find("baseMeshHand_Right_GRP").SetActive(true);
            
            calibrationFinger = GameObject.FindWithTag("Index");
            
            gridGenerator = GameObject.Find("DellGridGenerator");
            
            GameObject.Find("MobileDevice").SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
