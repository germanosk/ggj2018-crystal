using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour {
    public GridElement[,] grid;
    public int length, height;
    public Vector2 offset,tileSize;

    public void MakeMap()
    {
        GridElement[] elements = FindObjectsOfType<GridElement>();
        int maxX = 0, minZ = 0;
        for (int i = 0; i < elements.Length; i++)
        {
            Vector3 pos = elements[i].transform.position;
            if (pos.x > maxX)
            {
                maxX = (int)pos.x;
            }
            if (pos.z < minZ)
            {
                minZ = (int)pos.z;
            }
        }
        minZ = Mathf.Abs (minZ);
        grid = new GridElement[maxX +1, minZ +1];
        for (int i = 0; i < elements.Length; i++)
        {
            Vector3 pos = elements[i].transform.position;
            Debug.Log("Element " + i + " " + pos);
            grid[(int)pos.x, (int)-pos.z] = elements[i];
        }
    }
}
