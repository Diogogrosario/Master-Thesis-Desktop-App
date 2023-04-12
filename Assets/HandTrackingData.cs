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

    private void Start()
    {
        LeftHandProjection = GameObject.Find("LeftHandProjection");
        RightHandProjection = GameObject.Find("RightHandProjection");
    }

    private void Update()
    {
        List<Hand> _allHands = Hands.Provider.CurrentFrame.Hands;

        foreach (Hand _hand in _allHands)
        {
            //Use _hand to Explicitly get the specified fingers from it
            Finger _thumb = _hand.GetThumb();

            ProjectOnMobile(_hand.IsLeft,_thumb.TipPosition);

        }
        
    }

    private void ProjectOnMobile(bool isLeft,Vector3 thumbTipPosition)
    {
        Transform mobilePhonePlane = GameObject.Find("MobileDevice").transform;
        
        var up = mobilePhonePlane.forward;
        Vector3 targetPos = Vector3.ProjectOnPlane(thumbTipPosition, up) + Vector3.Dot(mobilePhonePlane.position, up) * up;

        if (isLeft)
        {
            LeftHandProjection.transform.localPosition = targetPos;
            LeftHandProjection.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            RightHandProjection.transform.localPosition = targetPos;
            RightHandProjection.GetComponent<Renderer>().material.color = Color.blue;
        }

    }
}
