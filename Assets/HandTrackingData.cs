using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class HandTrackingData : MonoBehaviour
{
    public LeapProvider leapProvider;

    private GameObject LeftHandProjection;
    private GameObject RightHandProjection;
    private bool isMobile;
    private TestControl masterScript;

    private GameObject device;
    private float upShift;

    private void Start()
    {
        masterScript = GameObject.Find("TestControlScript").GetComponent<TestControl>();
        LeftHandProjection = masterScript.leftHandProjection;
        RightHandProjection = masterScript.rightHandProjection;
        isMobile = masterScript.isMobile;
        device = masterScript.device;
        upShift = masterScript.upShift;
    }

    private void Update()
    {
        List<Hand> _allHands = Hands.Provider.CurrentFrame.Hands;
        var left = false;
        var right = false;
        
        foreach (Hand _hand in _allHands)
        {
            Finger _finger;
            if (isMobile)
            {
                _finger = _hand.GetThumb();
            }
            else
            {
                _finger = _hand.GetIndex();
            }
            
            
            ProjectOnMobile(_hand.IsLeft,_finger.TipPosition);
            if (_hand.IsLeft)
            {
                left = true;
            }
            else
            {
                right = true;
            }
        }

        //lost track, change color
        var color = new Color(136 / 255f, 138 / 255f, 133 / 255f);
        if (!left)
        {
            if (masterScript.dataWriter != null && LeftHandProjection.GetComponent<Renderer>().material.color != color)
            {
                masterScript.dataWriter.WriteLine(DateTime.UtcNow + ",," +
                                                  RightHandProjection.GetComponent<PhoneOverlap>().currentGridCell
                                                  + "," + LeftHandProjection.GetComponent<PhoneOverlap>()
                                                      .currentGridCell + ",LostTrack,,1,");
            }

            LeftHandProjection.GetComponent<Renderer>().material.color = color;
        }

        if (!right)
        {
            if (masterScript.dataWriter != null && RightHandProjection.GetComponent<Renderer>().material.color != color)
            {
                masterScript.dataWriter.WriteLine(DateTime.UtcNow + ",," +
                                                  RightHandProjection.GetComponent<PhoneOverlap>().currentGridCell
                                                  + "," + LeftHandProjection.GetComponent<PhoneOverlap>()
                                                      .currentGridCell + ",LostTrack,,,1");
            }

            RightHandProjection.GetComponent<Renderer>().material.color = color;
        }
    }

    private void ProjectOnMobile(bool isLeft,Vector3 thumbTipPosition)
    {
        Transform mobilePhonePlane = device.transform;
        
        var up = mobilePhonePlane.forward;
        Vector3 targetPos = Vector3.ProjectOnPlane(thumbTipPosition, up) + Vector3.Dot(mobilePhonePlane.position, up) * up;
        Vector3 upShift = -up * this.upShift;

        if (isLeft)
        {
            LeftHandProjection.transform.localPosition = targetPos + upShift;
            LeftHandProjection.GetComponent<Renderer>().material.color = new Color(239/255f, 41/255f, 41/255f);
        }
        else
        {
            RightHandProjection.transform.localPosition = targetPos + upShift;
            RightHandProjection.GetComponent<Renderer>().material.color = new Color(114/255f, 159/255f, 207/255f);
        }

    }
}
