using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TilemapVariables))]
public class TilemapVariablesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TilemapVariables com = (TilemapVariables) target;
        DrawDefaultInspector();
        if (GUILayout.Button("Regenerate Grid"))
        {
            com.PopulateCellArray();
        }
        /*
        TilemapVariables myTilemapVariables = (TilemapVariables) target;
        myTilemapVariables.amount = EditorGUILayout.IntField("Amount", myTilemapVariables.amount);
        */
    }
}
