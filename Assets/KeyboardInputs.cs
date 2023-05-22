using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputs : MonoBehaviour
{
    private GameObject gridGenerator;
    // Start is called before the first frame update
    void Start()
    {
        gridGenerator = GameObject.Find("GridGenerator");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1 was pressed");
            gridGenerator.GetComponent<GridGenerator>().task = 1;
            gridGenerator.GetComponent<GridGenerator>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("2 was pressed");
            gridGenerator.GetComponent<GridGenerator>().task = 2;
            gridGenerator.GetComponent<GridGenerator>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("3 was pressed");
            gridGenerator.GetComponent<GridGenerator>().task = 3;
            gridGenerator.GetComponent<GridGenerator>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("4 was pressed");
            gridGenerator.GetComponent<GridGenerator>().task = 4;
            gridGenerator.GetComponent<GridGenerator>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("5 was pressed");
            gridGenerator.GetComponent<GridGenerator>().task = 5;
            gridGenerator.GetComponent<GridGenerator>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("6 was pressed");
            gridGenerator.GetComponent<GridGenerator>().task = 6;
            gridGenerator.GetComponent<GridGenerator>().enabled = true;
        }
    }
}
