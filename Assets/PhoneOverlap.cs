using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneOverlap : MonoBehaviour
{
    // Start is called before the first frame update
    Renderer renderer;

    public int currentGridCell = -1;
    
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TouchScreen")
        {
            renderer.enabled = true;
        }

        if (other.tag == "Grid")
        {
            currentGridCell = int.Parse(other.name.Substring(3));
            Debug.Log(currentGridCell);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TouchScreen")
        {
            renderer.enabled = false;
        }

        if (other.tag == "Grid")
        {
            currentGridCell = -1;
        }
    }
}
