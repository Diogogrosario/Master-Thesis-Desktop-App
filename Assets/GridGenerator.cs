using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;
using Valve.VR;

public class GridGenerator : MonoBehaviour
{

    private float rows;
    private float columns;
    public int task;

    public float gridSize;

    [SerializeField] private Texture2D gridTexture;
    // Start is called before the first frame update

    [SerializeField] private float Xoffset;
    [SerializeField] private float Yoffset;

    private GameObject game;
    
    void OnEnable()
    {
        Debug.Log("I was enabled, task = " + task);
        if (task == 0)
        {
            this.enabled = false;
            return;
        }
        if (task == 1)
        {
            rows = 6;
            columns = 4;
        }
        else if (task == 2)
        {
            rows = 9;
            columns = 6;
        }
        else if (task == 3)
        {
            rows = 16;
            columns = 9;
        }
        else if (task == 4)
        {
            rows = 4;
            columns = 6;
        }
        else if (task == 5)
        {
            rows = 6;
            columns = 9;
        }
        else if (task == 6)
        {
            rows = 9;
            columns = 16;
        }
        int counter = 0;
        gridSize = rows * columns;

        
        var ScreenWidth = transform.parent.localScale.x - Xoffset;
        var ScreenHeight = transform.parent.localScale.y - Yoffset;
        var Xpercentage = Xoffset / transform.parent.localScale.x;
        var Ypercentage = Yoffset / transform.parent.localScale.y;
        
        float cellWidth = ScreenWidth / columns;
        float cellHeigth = ScreenHeight / rows;

        float startX = -cellWidth * (columns - 1) / (2 * transform.parent.localScale.x);
        float startY = -cellHeigth * (rows - 1) / (2 * transform.parent.localScale.y);
        

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                float x = startX + col * cellWidth / transform.parent.localScale.x;
                float y = startY + row * cellHeigth / transform.parent.localScale.y;
                
                GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cell.tag = "Grid";
                cell.name = "Grid" + counter;
                counter++;
                cell.GetComponent<Renderer>().material.mainTexture = gridTexture;
                cell.AddComponent<TimeToClick>();
                cell.transform.rotation = transform.parent.rotation;
                cell.transform.parent = transform.parent;
                cell.transform.localScale = new Vector3((1-Xpercentage)/columns, (1-Ypercentage)/rows, 1);
                cell.transform.localPosition = new Vector3(x, y, -0.05f);
            }
        }

        game = GameObject.Find("Game");
        
        //Need to move this to menu
        game.GetComponent<Game>().startGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
