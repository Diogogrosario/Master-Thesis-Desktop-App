using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneOverlap : MonoBehaviour
{
    // Start is called before the first frame update
    Renderer renderer;

    public int currentGridCell = -1;
    private int previousCell = -1;
    
    private TestControl masterScript;
    void Start()
    {
     
        renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;
        masterScript = GameObject.Find("TestControlScript").GetComponent<TestControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TouchScreen")
        {
            if(masterScript.isProjection)
                renderer.enabled = true;
        }

        if (other.tag == "Grid")
        {
            previousCell = currentGridCell;
            currentGridCell = int.Parse(other.name.Substring(4));
            
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TouchScreen")
        {
            if(masterScript.isProjection)
                renderer.enabled = false;
        }

        if (other.tag == "Grid")
        {
            if (previousCell == currentGridCell)
            {
                currentGridCell = -1;
                previousCell = -1;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
