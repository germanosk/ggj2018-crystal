using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour {
    public GridElement[,] grid;
    public int length, height;

    #region Singleton
    private static GridMaker instance;
    public static GridMaker Instance
    {
        get
        {
            return instance;
        }
    }
    private GridMaker()
    {
        instance = this;
    }
    #endregion

    public GridElement[] elements;
    void Awake()
    {
        GenerateGrid();
    }

    /// <summary>
    /// Find all Grid Elements and make an array of them and
    /// calculate height and lenght of the grid
    /// Intended only to be used on Editor
    /// </summary>
    public void MakeMap()
    {
        elements = FindObjectsOfType<GridElement>();
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
        length = maxX + 1;
        height = minZ + 1;
        
    }

    /// <summary>
    /// Use element array to generate an multidimension array
    /// Also setup grid elements
    /// </summary>
    void GenerateGrid()
    {
        grid = new GridElement[length, height];
        Vector2Int p = Vector2Int.zero;
        for (int i = 0; i < elements.Length; i++)
        {
            Vector3 pos = elements[i].transform.position;
            p.x = (int)pos.x;
            p.y = (int)-pos.z;
            elements[i].gridPos = p;
            grid[p.x, p.y] = elements[i];
        }
    }

    public List<GridElement> GetNeighbor(int x, int y)
    {
        List<GridElement> result = new List<GridElement>();
        int i, j;
        i = x - 1;
        j = y;
        if (i >= 0)
        {
            AddElementIfNotNull(result, i, j);
            j = y - 1;
            if (j >= 0)
            {
                AddElementIfNotNull(result, i, j);
            }
            j = y + 1;
            if (j >= 0)
            {
                AddElementIfNotNull(result, i, j);
            }
        }
        i = x + 1;
        j = y;
        if (i < length)
        {
            AddElementIfNotNull(result, i, j);
            j = y - 1;
            if (j >= 0)
            {
                AddElementIfNotNull(result, i, j);
            }
            j = y + 1;
            if (j >= 0)
            {
                AddElementIfNotNull(result, i, j);
            }
        }
        j = x;
        j = y - 1;
        if (j >= 0)
        {
            AddElementIfNotNull(result, i, j);
        }
        j = y + 1;
        if (j >= 0)
        {
            AddElementIfNotNull(result, i, j);
        }
        return result;
    }

    void AddElementIfNotNull(List<GridElement> list, int x, int y)
    {
        if (grid[x, y] != null)
        {
            list.Add(grid[x, y]);
        }
    }
}
