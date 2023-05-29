using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Leap;
using Leap.Unity;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Calibration : MonoBehaviour
{
    
    [SerializeField] private Transform VrCamera;

    private GameObject finger;

    private List<Vector3> offsets = new List<Vector3>();
    

    // Start is called before the first frame update
    void Start()
    {
        var masterScript = GameObject.Find("TestControlScript").GetComponent<TestControl>();
        finger = masterScript.calibrationFinger;
    }

    // Update is called once per frame

    void Update()
    {
        
    }

    public Vector3 saveCoords(float width, float height, float x, float y)
    {
        GameObject dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        dot.transform.parent = transform.parent;
        dot.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        dot.GetComponent<Renderer>().enabled = false;

        // Get scale and subtract x/2, y/2 so the "origin" is at bottom left. Then do the 3 rule, scale -> resolution so ? -> touch
        Vector3 localScale = transform.localScale;
        float xTransform = localScale.x * x / width - localScale.x / 2;
        float yTransform = localScale.y * y / height - localScale.y / 2;
        dot.transform.localPosition = new Vector3(0f,0f,0f); //Reset position before translating
        dot.transform.localRotation = Quaternion.Euler(0,0,0);
        dot.transform.Translate(xTransform, yTransform, 0.0f);

        Vector3 handPosition = new Vector3(0, 0, 0);
        if (finger != null)
            handPosition = finger.transform.position;
        
        offsets.Add(dot.transform.position - handPosition);
        return dot.transform.position;
    }

    public void calibrate()
    {
        Vector3 offsetSum = new Vector3(0,0,0);
        foreach (Vector3 offset in offsets)
        {
            offsetSum += offset;
        }
        
        var yProject = Vector3.Project(offsetSum/offsets.Count, VrCamera.up);
        var zProject = Vector3.Project(offsetSum/offsets.Count, VrCamera.forward);
        var offsetY = yProject.magnitude * (Vector3.Dot(yProject, VrCamera.up) > 0 ? 1 : -1);
        var offsetZ = zProject.magnitude * (Vector3.Dot(zProject, VrCamera.forward) > 0 ? 1 : -1);
        GameObject.Find("Service Provider (XR)").GetComponent<LeapXRServiceProvider>().deviceOffsetYAxis += offsetY;
        GameObject.Find("Service Provider (XR)").GetComponent<LeapXRServiceProvider>().deviceOffsetZAxis += offsetZ;

        Debug.Log("Calibrated!");

    }
}
