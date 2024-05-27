using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCache : Singleton<MapCache>
{
    public Map map;

    [SerializeField] private GameObject notePrefab;
    private Vector3 spawnPosition;
    public void LoadMap()
    {
        Debug.Log("load map");
        for (int i = 0; i < map.notes.Length; i++) 
        {
            float tempX = map.notes[i].lane * 2f - 30f;
            float tempZ = map.notes[i].timePosition * 2f;
            Vector3 tempSpawn = new Vector3(tempX, 0, tempZ);
            GameObject noteGO = Instantiate(notePrefab, tempSpawn, Quaternion.identity, transform);
            noteGO.transform.Rotate(90f,0,0);
        }
    }

    public void SaveMap()
    {
        Debug.Log("save map");
    }
}
