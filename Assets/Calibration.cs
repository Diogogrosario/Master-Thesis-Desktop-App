using System.Collections;
using System.Collections.Generic;
using Leap;
using Leap.Unity;
using UnityEngine;

public class Calibration : MonoBehaviour
{
    //Assume tracker is placed on a flat horizontal surface
    //We will assume tracker is in this position   -
    //                                           -   -
    //only the right thumb will touch each "peak" in clockwise order 1-2-3 starting from the top
    //Coordinates from top to bottom -> z axis
    //Coordinates from left to right -> x axis

    //Offsets between hand and tracker position to calculate the offset after
    private Vector3 Offset1 = new Vector3(0, 0, 0);
    private Vector3 Offset2 = new Vector3(0, 0, 0);
    private Vector3 Offset3 = new Vector3(0, 0, 0);
    
    private bool calibrated = false;

    private GameObject peak1;
    private GameObject peak2;
    private GameObject peak3;
    
    [SerializeField] private Transform VrCamera;
    

    // Start is called before the first frame update
    void Start()
    {
        peak1 = GameObject.Find("Peak1");
        peak2 = GameObject.Find("Peak2");
        peak3 = GameObject.Find("Peak3");
    }

    // Update is called once per frame

    void Update()
    {
        //Calibrated dont need to do anything
        if (calibrated)
        {
            return;
        }

        if (Offset1 != Vector3.zero && Offset2 != Vector3.zero && Offset3 != Vector3.zero)
        {
            
            //Calculate average and change the value
            Debug.Log(Offset1);
            Debug.Log(Offset2);
            Debug.Log(Offset3);
            Vector3 calibrationOffset = (Offset1 + Offset2 + Offset3) / 3;
            Debug.Log((Offset1+Offset2+Offset3)/3);
            
            
            float offsetY = Vector3.Project(calibrationOffset, VrCamera.up).magnitude;
            float offsetZ = Vector3.Project(calibrationOffset, VrCamera.forward).magnitude;
            GameObject.Find("Service Provider (XR)").GetComponent<LeapXRServiceProvider>().deviceOffsetYAxis += offsetY;
            GameObject.Find("Service Provider (XR)").GetComponent<LeapXRServiceProvider>().deviceOffsetZAxis += offsetZ;
            calibrated = true;
        }
        
        Vector3 handPosition = new Vector3(0,0,0);
        GameObject thumb;
        thumb = GameObject.FindWithTag("Thumb");
        if(thumb != null)
            handPosition = thumb.transform.position;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Vector3 peak1Position = peak1.transform.position;
            Offset1 = handPosition - peak1Position;
            Debug.Log("1 was pressed");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Vector3 peak2Position = peak2.transform.position;
            Offset2 = handPosition - peak2Position;
            Debug.Log("2 was pressed");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Vector3 peak3Position = peak3.transform.position;
            Offset3 = handPosition - peak3Position;
            Debug.Log("3 was pressed");
        }
    }
}
