using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StronglyConnectedComponents;

public class GridMaker : MonoBehaviour {
    public GridElement[,] grid;

    public Vertex<ContactCollor>[,] gridVertex;

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
        gridVertex = new Vertex<ContactCollor>[length, height];
        Vector2Int p = Vector2Int.zero;
        for (int i = 0; i < elements.Length; i++)
        {
            Vector3 pos = elements[i].transform.position;
            p.x = (int)pos.x;
            p.y = (int)-pos.z;
            elements[i].gridPos = p;
            grid[p.x, p.y] = elements[i];
            ContactCollor contact = elements[i].GetComponent<ContactCollor>();
            if (contact != null)
            {
                gridVertex[p.x, p.y] = new Vertex<ContactCollor>(contact);
                //gridVertex[p.x, p.y].Id = p.x + p.y+1;
                contact.vertex = gridVertex[p.x, p.y];
            }
        }
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if(gridVertex[i, j] != null)
                {
                    AddDependencies(i, j, gridVertex[i, j]);
                }
            }
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
        i = x;
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

    public void AddDependencies(int x, int y, Vertex<ContactCollor> v)
    {
        
        int i, j;
        i = x;
        j = y - 1;
        if (j >= 0)
        {
            AddDependencyIfNotNull(i, j, v);
        }
        j = y + 1;
        if (j < height)
        {
            AddDependencyIfNotNull(i, j, v);
        }

        i = x - 1;
        j = y;
        if (i >= 0)
        {
            AddDependencyIfNotNull( i, j,v);
            j = y - 1;
            if (j >= 0)
            {
                AddDependencyIfNotNull( i, j, v);
            }
            j = y + 1;
            if (j < height)
            {
                AddDependencyIfNotNull( i, j, v);
            }
        }
        i = x + 1;
        j = y;
        if (i < length)
        {
            AddDependencyIfNotNull(i, j, v);
            j = y - 1;
            if (j >= 0)
            {
                AddDependencyIfNotNull(i, j, v);
            }
            j = y + 1;
            if (j < height)
            {
                AddDependencyIfNotNull(i, j, v);
            }
        }
    }

    void AddElementIfNotNull(List<GridElement> list, int x, int y)
    {
        if (grid[x, y] != null)
        {
            list.Add(grid[x, y]);
        }
    }


    void AddDependencyIfNotNull( int x, int y, Vertex<ContactCollor> v)
    {
        if(x <0 || y < 0)
        {
            Debug.Log("Lower than zero X: " + x + " Y: " + y);
        }
        if(x >=length || y >= height)
        {

            Debug.Log("Above than allowed X: " + x + " Y: " + y);
        }
        if (gridVertex[x, y] != null)
        {
            v.Dependencies.Add(gridVertex[x, y]);
        }
    }
}
