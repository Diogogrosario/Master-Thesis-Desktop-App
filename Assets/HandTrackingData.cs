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

    private void Start()
    {
        var masterScript = GameObject.Find("TestControlScript").GetComponent<TestControl>();
        LeftHandProjection = masterScript.leftHandProjection;
        RightHandProjection = masterScript.rightHandProjection;
        isMobile = masterScript.isMobile;
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
        if (!left)
        {
            LeftHandProjection.GetComponent<Renderer>().material.color = new Color(136/255f, 138/255f, 133/255f);
        }

        if (!right)
        {
            RightHandProjection.GetComponent<Renderer>().material.color = new Color(136/255f, 138/255f, 133/255f);
        }
    }

    private void ProjectOnMobile(bool isLeft,Vector3 thumbTipPosition)
    {
        Transform mobilePhonePlane = GameObject.Find("MobileDevice").transform;
        
        var up = mobilePhonePlane.forward;
        Vector3 targetPos = Vector3.ProjectOnPlane(thumbTipPosition, up) + Vector3.Dot(mobilePhonePlane.position, up) * up;
        Vector3 upShift = -up * 0.0025f;

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
