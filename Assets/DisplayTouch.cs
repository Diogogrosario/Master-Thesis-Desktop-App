using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTouch : MonoBehaviour
{

    private float width;
    private float height;
    private float aspectRatio;
    private Vector3 cubeWorldSize;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setWidth(float phoneWidth)
    {
        width = phoneWidth;
    }

    public void setHeight(float phoneHeight)
    {
        height = phoneHeight;
    }
    
    
    public void setAspectRatio()
    {
        aspectRatio = width / height;
    }
    

    public void showTouch(float x, float y)
    {
        GameObject dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // Get scale and subtract x/2, y/2 so the "origin" is at bottom left. Then do the 3 rule, scale -> resolution so ? -> touch
        Vector3 localScale = transform.localScale;
        float xTransform = localScale.x * x / width - localScale.x/2;
        float yTransform = localScale.y * y / height - localScale.y/2;
        dot.transform.Translate(xTransform, yTransform, 0.0f);
        dot.GetComponent<Renderer>().material.color = Color.red;


    }

}
