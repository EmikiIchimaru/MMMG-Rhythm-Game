#if UNITY_EDITOR
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
        if (GUILayout.Button("generate hold note"))
        {
            MapCache.Instance.GenerateHoldNotes();
        }
        /* if (GUILayout.Button("timescale"))
        {
           
            int timeInt = (int) Mathf.Floor(Time.timeScale * 2);
            timeInt = (timeInt + 1) % 4;
            Time.timeScale = 0.5f * timeInt;
            Debug.Log(Time.timeScale);
        } */
    }
}
#endif
