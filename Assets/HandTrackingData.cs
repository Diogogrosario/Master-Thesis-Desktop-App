using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
public class HandTrackingData : MonoBehaviour
{
    public LeapProvider leapProvider;

    private GameObject dot;
    private GameObject HMDPosition;

    private void Start()
    {
        dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        dot.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        HMDPosition = GameObject.Find("VRCamera");
    }

    private void Update()
    {
        List<Hand> _allHands = Hands.Provider.CurrentFrame.Hands;

        foreach (Hand _hand in _allHands)
        {
            //Use _hand to Explicitly get the specified fingers from it
            Finger _thumb = _hand.GetThumb();

            Debug.Log("Start logging, isLeft = " + _hand.IsLeft);
            ProjectOnMobile(_thumb.TipPosition);

        }
        
    }

    private void ProjectOnMobile(Vector3 thumbTipPosition)
    {
        Transform mobilePhonePlane = GameObject.Find("MobileDevice").transform;
        
        var up = mobilePhonePlane.forward;
        Vector3 targetPos = Vector3.ProjectOnPlane(thumbTipPosition, up) + Vector3.Dot(mobilePhonePlane.position, up) * up;

        dot.transform.localPosition = targetPos;
        dot.GetComponent<Renderer>().material.color = Color.red;
        
    }
}
