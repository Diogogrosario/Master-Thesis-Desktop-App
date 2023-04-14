using System.Collections;
using System.Collections.Generic;
using Leap;
using Leap.Unity;
using UnityEngine;

public class Calibration : MonoBehaviour
{
    
    public LeapProvider leapProvider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    //Assume tracker is placed on a flat horizontal surface
    //We will assume tracker is in this position   -
    //                                           -   -
    //only the right thumb will touch each "peak" in clockwise order 1-2-3 starting from the top
    //Coordinates from top to bottom -> z axis
    //Coordinates from left to right -> x axis

    
    //calculated from htc vive tracker specification sheet
    private Vector3 peak1Offset = new Vector3(0,0.04f,0.035f);
    private Vector3 peak2Offset = new Vector3(0.04f,0.04f,-0.035f);
    private Vector3 peak3Offset = new Vector3(-0.04f,0.4f,-0.035f);

    //Offsets between hand and tracker position to calculate the offset after
    private Vector3 Offset1 = new Vector3(0, 0, 0);
    private Vector3 Offset2 = new Vector3(0, 0, 0);
    private Vector3 Offset3 = new Vector3(0, 0, 0);

    private bool calibrated = false;
    
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
            GameObject.Find("Service Provider (XR)").GetComponent<LeapXRServiceProvider>().deviceOffsetYAxis = 0.5f;
            GameObject.Find("Service Provider (XR)").GetComponent<LeapXRServiceProvider>().deviceOffsetZAxis = 0.5f;
            calibrated = true;
        }
        
        List<Hand> _allHands = Hands.Provider.CurrentFrame.Hands;
        Vector3 handPosition = new Vector3(0,0,0);
        foreach (Hand _hand in _allHands)
        {
            if (_hand.IsRight)
            {
                Finger _thumb = _hand.GetThumb();
                handPosition = _thumb.TipPosition;
            }

        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Vector3 peak1Position = transform.position + peak1Offset;
            Offset1 = handPosition - peak1Position;
            Debug.Log("1 was pressed");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Vector3 peak2Position = transform.position + peak2Offset;
            Offset2 = handPosition - peak2Position;
            Debug.Log("2 was pressed");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Vector3 peak3Position = transform.position + peak3Offset;
            Offset3 = handPosition - peak3Position;
            Debug.Log("3 was pressed");
        }
    }
}
