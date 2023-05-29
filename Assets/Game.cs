using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.DualShock;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{

    [SerializeField] private float gameTime;
    [SerializeField] private float multiTouchCooldown;

    private float timeLeft = 0;
    private bool gameInit = false;

    private int nTargets = 0;
    [SerializeField] private int maxTargets = 2;
    
    private float gridSize;
    
    [SerializeField] private Texture2D gridCircleTexture;
    [SerializeField] private Texture2D gridTexture;

    private int lastRandom = -1;
    
    
    private GameObject LeftHandProjection;
    private GameObject RightHandProjection;

    private Queue<Vector3> touchQueue = new Queue<Vector3>();
    private Dictionary<int, bool> activeCells = new Dictionary<int, bool>();

    private GameObject gridGenerator;

    private TestControl masterScript;

    private void Start()
    {
        masterScript = GameObject.Find("TestControlScript").GetComponent<TestControl>();
        LeftHandProjection = masterScript.leftHandProjection;
        RightHandProjection = masterScript.rightHandProjection;
        gridGenerator = masterScript.gridGenerator;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameInit)
        {
            
            while (touchQueue.Count != 0)
            {
                var coords = touchQueue.Dequeue();
                var collidingCell = -1;
                
                //instead of choosing projection, need to use actual coords for baseline
                if (masterScript.isProjection)
                {
                    if (touchIsCloserToLeft(coords))
                    {
                        collidingCell = LeftHandProjection.GetComponent<PhoneOverlap>().currentGridCell;
                    }
                    else
                    {
                        collidingCell = RightHandProjection.GetComponent<PhoneOverlap>().currentGridCell;
                    }
                    
                    Debug.Log("Projection colliding with cell -> " + collidingCell);
                }
                else
                {
                    GameObject dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    dot.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    dot.transform.localPosition = coords;
                    dot.AddComponent<Rigidbody>();
                    dot.GetComponent<Rigidbody>().isKinematic = true;
                    dot.GetComponent<SphereCollider>().isTrigger = true;
                    dot.GetComponent<SphereCollider>().radius = 0.1f;
                    dot.AddComponent<PhoneOverlap>();
                    collidingCell = dot.GetComponent<PhoneOverlap>().currentGridCell;
                    Debug.Log("Baseline sphere created, current grid cell = " + collidingCell);
                    Destroy(dot);
                }

                var cell1 = -1;
                var cell2 = -1;
                foreach (var cellValue in activeCells.Keys)
                {
                    if (activeCells[cellValue])
                    {
                        if (cell1 == -1)
                        {
                            cell1 = cellValue;
                        }

                        if (cell1 != -1)
                        {
                            cell2 = cellValue;
                        }
                    }
                }
                //Outside grid touch
                if (collidingCell == -1)
                {
                    masterScript.dataWriter.WriteLine(DateTime.UtcNow + "," + cell1 + "_" + cell2 + "," + RightHandProjection.GetComponent<PhoneOverlap>().currentGridCell 
                                                      + "," + LeftHandProjection.GetComponent<PhoneOverlap>().currentGridCell + ",Touch," 
                                                      + ",,,");
                    continue;
                }
                //Cell was not active
                if (activeCells[collidingCell] == false)
                {
                    masterScript.dataWriter.WriteLine(DateTime.UtcNow + "," + cell1 + "_" + cell2 + "," + RightHandProjection.GetComponent<PhoneOverlap>().currentGridCell 
                                                      + "," + LeftHandProjection.GetComponent<PhoneOverlap>().currentGridCell + ",Touch," 
                                                      + ",,,");
                    continue;
                }
                var cell = GameObject.Find("Grid" + collidingCell).GetComponent<Renderer>().material.mainTexture = gridTexture;
                
                Debug.Log("Collided, will reduce targets");
                masterScript.dataWriter.WriteLine(DateTime.UtcNow + "," + cell1 + "_" + cell2 + "," + RightHandProjection.GetComponent<PhoneOverlap>().currentGridCell 
                                                  + "," + LeftHandProjection.GetComponent<PhoneOverlap>().currentGridCell + ",Hit," 
                                                  + cell.GetComponent<TimeToClick>().endTimer().ToString() + ",,");
                activeCells[collidingCell] = false;
                nTargets--;
            }

            //Reduce time
            timeLeft -= Time.deltaTime;
            

            //Debug.Log("Time left for game: " + timeLeft);
            
            //Generate Target and force multiTouch later
            if (nTargets < maxTargets )
            {
                generateTarget();
                
            }
        }
        
        //End game if no time
        if (timeLeft <= 0 && gameInit)
        {
            endGame();
        }
    }

    private bool touchIsCloserToLeft(Vector3 coords)
    {
        float leftDistance = Vector3.Distance(coords, LeftHandProjection.transform.position);
        float rightDistance = Vector3.Distance(coords, RightHandProjection.transform.position);
        Debug.Log("left distance: " + leftDistance);
        Debug.Log("right distance: " + rightDistance);
        return leftDistance < rightDistance;

    }

    //need to implement force not same target
    private void generateTarget()
    {
        int randomTarget = (int)Random.Range(0, gridSize - 1);
        while(randomTarget == lastRandom)
        {
            randomTarget = (int)Random.Range(0, gridSize - 1);
        }

        var cell = GameObject.Find("Grid" + randomTarget);
        cell.GetComponent<Renderer>().material.mainTexture = gridCircleTexture;
        cell.GetComponent<TimeToClick>().startTimer();
        lastRandom = randomTarget;
        activeCells[randomTarget] = true;
        var cell1 = -1;
        var cell2 = -1;
        foreach (var cellValue in activeCells.Keys)
        {
            if (activeCells[cellValue] == true)
            {
                if (cell1 == -1)
                {
                    cell1 = cellValue;
                }

                if (cell1 != -1)
                {
                    cell2 = cellValue;
                }
            }
        }
        masterScript.dataWriter.WriteLine(DateTime.UtcNow + "," + cell1 + "_" + cell2 + "," + RightHandProjection.GetComponent<PhoneOverlap>().currentGridCell + ","
        + LeftHandProjection.GetComponent<PhoneOverlap>().currentGridCell + ",Generate,,,");
        nTargets++;
        Debug.Log("Generating target, n targets = " + nTargets + "; Target value = " + randomTarget);
        
    }

    public void resetTimer()
    {
        timeLeft = gameTime;
    }

    public void startGame()
    {
        resetTimer();
        gridSize = gridGenerator.GetComponent<GridGenerator>().gridSize;
        for (int i = 0; i < gridSize; i++)
        {
            activeCells.Add(i,false);
        }
        gameInit = true;
    }
    
    private void endGame()
    {
        for (int i = 0; i < gridSize; i++)
        {
            Destroy(GameObject.Find("Grid" + i));
        }

        activeCells = new Dictionary<int, bool>();
        gameInit = false;
        gridGenerator.GetComponent<GridGenerator>().enabled = false;
        masterScript.dataWriter.Close();
    }

    public bool hasStarted()
    {
        return gameInit;
    }

    //Decide if its right projection or left projection used to trigger;
    public void triggerInput(Vector3 touchPositionInSpace)
    {
        touchQueue.Enqueue(touchPositionInSpace);
    }
}

