using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridMaker))]
public class GridManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GridMaker gridMaker = (GridMaker)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Make Map"))
        {
            gridMaker.MakeMap();
        }
    }
}
