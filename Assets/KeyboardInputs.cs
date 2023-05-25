using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using UnityEngine;

public class KeyboardInputs : MonoBehaviour
{
    private GameObject gridGenerator;
    private TestControl masterScript;
    private void Start()
    {
        masterScript = GameObject.Find("TestControlScript").GetComponent<TestControl>();
        gridGenerator = masterScript.gridGenerator;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1 was pressed");
            gridGenerator.GetComponent<GridGenerator>().task = 1;
            gridGenerator.GetComponent<GridGenerator>().enabled = true;
            string filePath = Application.persistentDataPath + "/user" + masterScript.userID + "_task1_data"+ ".csv";
            masterScript.dataWriter = new StreamWriter(filePath,true);
            masterScript.dataWriter.WriteLine("Timestamp,ActiveCells,RightProjectionCell,LeftProjectionCell,Action,TimeToHit,LostTrackOfLeft,LostTrackOfRight");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("2 was pressed");
            gridGenerator.GetComponent<GridGenerator>().task = 2;
            gridGenerator.GetComponent<GridGenerator>().enabled = true;
            string filePath = Application.persistentDataPath + "user" + masterScript.userID + "_task2_data"+ ".csv";
            masterScript.dataWriter = new StreamWriter(filePath,true);
            masterScript.dataWriter.WriteLine("Timestamp,ActiveCells,RightProjectionCell,LeftProjectionCell,Action,TimeToHit,LostTrackOfLeft,LostTrackOfRight");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("3 was pressed");
            gridGenerator.GetComponent<GridGenerator>().task = 3;
            gridGenerator.GetComponent<GridGenerator>().enabled = true;
            
            string filePath = Application.persistentDataPath + "user" + masterScript.userID + "_task3_data"+ ".csv";
            masterScript.dataWriter = new StreamWriter(filePath,true);
            masterScript.dataWriter.WriteLine("Timestamp,ActiveCells,RightProjectionCell,LeftProjectionCell,Action,TimeToHit,LostTrackOfLeft,LostTrackOfRight");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("4 was pressed");
            gridGenerator.GetComponent<GridGenerator>().task = 4;
            gridGenerator.GetComponent<GridGenerator>().enabled = true;
            string filePath = Application.persistentDataPath + "user" + masterScript.userID + "_task4_data"+ ".csv";
            masterScript.dataWriter = new StreamWriter(filePath,true);
            masterScript.dataWriter.WriteLine("Timestamp,ActiveCells,RightProjectionCell,LeftProjectionCell,Action,TimeToHit,LostTrackOfLeft,LostTrackOfRight");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("5 was pressed");
            gridGenerator.GetComponent<GridGenerator>().task = 5;
            gridGenerator.GetComponent<GridGenerator>().enabled = true;
            string filePath = Application.persistentDataPath + "user" + masterScript.userID + "_task5_data"+ ".csv";
            masterScript.dataWriter = new StreamWriter(filePath,true);
            masterScript.dataWriter.WriteLine("Timestamp,ActiveCells,RightProjectionCell,LeftProjectionCell,Action,TimeToHit,LostTrackOfLeft,LostTrackOfRight");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("6 was pressed");
            gridGenerator.GetComponent<GridGenerator>().task = 6;
            gridGenerator.GetComponent<GridGenerator>().enabled = true;
            string filePath = Application.persistentDataPath + "user" + masterScript.userID + "_task6_data"+ ".csv";
            masterScript.dataWriter = new StreamWriter(filePath,true);
            masterScript.dataWriter.WriteLine("Timestamp,ActiveCells,RightProjectionCell,LeftProjectionCell,Action,TimeToHit,LostTrackOfLeft,LostTrackOfRight");
        }
    }
}
