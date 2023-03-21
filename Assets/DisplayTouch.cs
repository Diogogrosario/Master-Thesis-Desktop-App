using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTouch : MonoBehaviour
{
    private float width;
    private float height;
    private List<GameObject> touches = new List<GameObject>();
    private List<Color> _colors = new List<Color>();

    private void Start()
    {
        _colors.Add(Color.red);
        _colors.Add(Color.green);
        _colors.Add(Color.blue);
        _colors.Add(Color.magenta);
        _colors.Add(Color.black);
        for (int i = 0; i < 5; i++)
        {
            touches.Add(null);
        }
    }

    public void setWidth(float phoneWidth)
    {
        width = phoneWidth;
    }

    public void setHeight(float phoneHeight)
    {
        height = phoneHeight;
    }


    public void showTouch(int id, float x, float y)
    {
        if (touches[id] == null)
        {
            GameObject dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            dot.transform.parent = transform.parent;
            dot.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

            // Get scale and subtract x/2, y/2 so the "origin" is at bottom left. Then do the 3 rule, scale -> resolution so ? -> touch
            Vector3 localScale = transform.localScale;
            float xTransform = localScale.x * x / width - localScale.x / 2;
            float yTransform = localScale.y * y / height - localScale.y / 2;
            dot.transform.localPosition = new Vector3(0f,0f,0f); //Reset position before translating
            dot.transform.localRotation = Quaternion.Euler(0,0,0);
            dot.transform.Translate(xTransform, yTransform, 0.0f);
            dot.GetComponent<Renderer>().material.color = _colors[id];
            touches[id] = dot;
        }
        else
        {
            // Get scale and subtract x/2, y/2 so the "origin" is at bottom left. Then do the 3 rule, scale -> resolution so ? -> touch
            Vector3 localScale = transform.localScale;
            float xTransform = localScale.x * x / width - localScale.x / 2;
            float yTransform = localScale.y * y / height - localScale.y / 2;
            touches[id].transform.localPosition = new Vector3(0f,0f,0f); //Reset position before translating
            touches[id].transform.localRotation = Quaternion.Euler(0,0,0);
            touches[id].transform.Translate(xTransform, yTransform, 0.0f);
        }
    }

    public void removeTouch(int id)
    {
        Destroy(touches[id]);
        touches[id] = null;
    }
}