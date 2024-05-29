using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditor : EditorWindow
{
    //static int tempInt = 1;
    //public Map map;

    [MenuItem("Window/MapEditor")]
    public static void ShowWindow()
    {
        GetWindow<MapEditor>("Map Editor");
    }

    void OnGUI()
    {
        //EditorGUILayout.IntField("Integer", tempInt);

        if (GUILayout.Button("load map"))
        {
            MapCache.Instance.LoadMap();
        }
        if (GUILayout.Button("save map"))
        {
            MapCache.Instance.SaveMap();
        }
        if (GUILayout.Button("close map"))
        {
            MapCache.Instance.CloseMap();
        }
    }
}
