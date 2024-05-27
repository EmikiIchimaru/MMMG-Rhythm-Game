using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCache : Singleton<MapCache>
{
    public Map map;
    public void LoadMap()
    {
        Debug.Log("load map");
    }

    public void SaveMap()
    {
        Debug.Log("save map");
    }
}
