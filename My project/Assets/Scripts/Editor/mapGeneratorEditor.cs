using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(mapGenerater))]
public class mapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        mapGenerater mapGen = (mapGenerater)target;


        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.GenerateMap();
                mapGen.createTilemapMap();
            }
        }

        if(GUILayout.Button("Generate")){
            mapGen.GenerateMap();
        }
        
        if(GUILayout.Button("Generate tile map"))
            mapGen.createTilemapMap();
        
        if(GUILayout.Button("Clear"))
            mapGen.clearTiles();
    }
}