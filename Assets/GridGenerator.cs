using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    [SerializeField] private float rows;

    [SerializeField] private float columns;

    [SerializeField] private Texture2D gridCircleTexture;

    [SerializeField] private Texture2D gridTexture;
    // Start is called before the first frame update

    private float ScreenHeight;
    private float ScreenWidth;
    [SerializeField] private float Xoffset;
    [SerializeField] private float Yoffset = 0.02f;

    void Start()
    {
        int counter = 0;

        ScreenWidth = transform.parent.gameObject.GetComponent<MeshRenderer>().bounds.size.x - Xoffset;
        ScreenHeight = transform.parent.gameObject.GetComponent<MeshRenderer>().bounds.size.y - Yoffset;
        float cellWidth = ScreenWidth / columns;
        float cellHeigth = ScreenHeight / rows;
        float Xpercentage = Xoffset / transform.parent.gameObject.GetComponent<MeshRenderer>().bounds.size.x;
        float Ypercentage = Yoffset / transform.parent.gameObject.GetComponent<MeshRenderer>().bounds.size.y;

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
                cell.transform.parent = transform.parent;
                cell.transform.localScale = new Vector3((1-Xpercentage)/columns, (1-Ypercentage)/rows, 1);
                cell.transform.localPosition = new Vector3(x, y, -0.05f);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
