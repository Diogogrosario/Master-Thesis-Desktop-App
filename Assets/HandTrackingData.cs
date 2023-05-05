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
        var left = false;
        var right = false;
        
        foreach (Hand _hand in _allHands)
        {
            //Use _hand to Explicitly get the specified fingers from it
            Finger _thumb = _hand.GetThumb();
            ProjectOnMobile(_hand.IsLeft,_thumb.TipPosition);
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
            LeftHandProjection.GetComponent<Renderer>().material.color = new Color(136, 138, 133);
        }

        if (!right)
        {
            RightHandProjection.GetComponent<Renderer>().material.color = new Color(136, 138, 133);
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
            LeftHandProjection.GetComponent<Renderer>().material.color = new Color(239, 41, 41);
        }
        else
        {
            RightHandProjection.transform.localPosition = targetPos;
            RightHandProjection.GetComponent<Renderer>().material.color = new Color(114, 159, 207);
        }

    }
}
