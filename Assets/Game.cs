using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.DualShock;

public class Game : MonoBehaviour
{

    [SerializeField] private float gameTime;
    [SerializeField] private float multiTouchCooldown;

    private float timeLeft = 0;
    private bool gameInit = false;

    private float multiTouchTargetCooldown = 0;
    
    private int nTargets = 0;
    [SerializeField] private int maxTargets = 2;
    
    private float gridSize;
    
    [SerializeField] private Texture2D gridCircleTexture;
    [SerializeField] private Texture2D gridTexture;

    private int lastRandom = -1;
    
    
    private GameObject LeftHandProjection;
    private GameObject RightHandProjection;

    private Queue<KeyValuePair<float, float>> touchQueue = new Queue<KeyValuePair<float, float>>();
    private Dictionary<int, bool> activeCells = new Dictionary<int, bool>();

    private void Start()
    {
        LeftHandProjection = GameObject.Find("LeftHandProjection");
        RightHandProjection = GameObject.Find("RightHandProjection");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameInit)
        {
            //use x,y
            while (touchQueue.Count != 0)
            {
                var coords = touchQueue.Dequeue();
                var x = coords.Key;
                var y = coords.Value;
                var collidingCell = RightHandProjection.GetComponent<PhoneOverlap>().currentGridCell;
                Debug.Log("Projection colliding with cell -> " + collidingCell);
                if (collidingCell == -1)
                {
                    return;
                }
                if (activeCells[collidingCell] == false)
                {
                    return;
                }
                GameObject.Find("Grid" + collidingCell).GetComponent<Renderer>().material.mainTexture = gridTexture;
                Debug.Log("Collided, will reduce targets");
                activeCells[collidingCell] = false;
                nTargets--;
                multiTouchTargetCooldown = 0;
            }

            //Reduce time
            timeLeft -= Time.deltaTime;
            
            if (multiTouchTargetCooldown != 0)
            {
                multiTouchTargetCooldown -= Time.deltaTime;
            }

            //Debug.Log("Time left for game: " + timeLeft);
            //Debug.Log("Time left for multi touch event: " + multiTouchTargetCooldown);
            
            //Generate Target and force multiTouch later
            if (nTargets < maxTargets && multiTouchTargetCooldown <= 0)
            {
                generateTarget();
                Debug.Log(multiTouchTargetCooldown);
                //generated multitouch -> reset
                if (multiTouchTargetCooldown < 0)
                {
                    multiTouchTargetCooldown = 0;
                }
                else //force multitouch later
                {
                    multiTouchTargetCooldown = multiTouchCooldown;
                }
            }
        }
        
        //End game if no time
        if (timeLeft <= 0)
        {
            endGame();
        }
    }
    
    //need to implement force not same target
    private void generateTarget()
    {
        int randomTarget = (int)Random.Range(0, gridSize - 1);
        while(randomTarget == lastRandom)
        {
            randomTarget = (int)Random.Range(0, gridSize - 1);
        }
        GameObject.Find("Grid" + randomTarget).GetComponent<Renderer>().material.mainTexture = gridCircleTexture;
        lastRandom = randomTarget;
        activeCells[randomTarget] = true;
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
        gridSize = GameObject.Find("GridGenerator").GetComponent<GridGenerator>().gridSize;
        for (int i = 0; i < gridSize; i++)
        {
            activeCells.Add(i,false);
        }
        gameInit = true;
    }
    
    private void endGame()
    {
        gameInit = false;
    }

    public bool hasStarted()
    {
        return gameInit;
    }

    //Decide if its right projection or left projection used to trigger;
    public void triggerInput(float x, float y)
    {
        touchQueue.Enqueue(new KeyValuePair<float, float>(x,y));
    }
}

