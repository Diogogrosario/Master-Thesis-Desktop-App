using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    [SerializeField] private float rows;

    [SerializeField] private float columns;
    // Start is called before the first frame update

    private float ScreenHeight;
    private float ScreenWidth;
    void Start()
    {
        ScreenWidth = transform.parent.gameObject.GetComponent<MeshRenderer>().bounds.size.x;
        ScreenHeight = transform.parent.gameObject.GetComponent<MeshRenderer>().bounds.size.y;
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
                cell.transform.parent = transform.parent;
                cell.transform.localScale = new Vector3(1/columns, 1/rows, 1);
                cell.transform.localPosition = new Vector3(x, y, -0.05f);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
