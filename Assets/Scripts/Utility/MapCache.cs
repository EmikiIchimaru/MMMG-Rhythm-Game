using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class MapCache : Singleton<MapCache>
{
    public Map map;

    [SerializeField] private GameObject notePrefab;
    private Vector3 spawnPosition;
    public void LoadMap()
    {
        //Debug.Log("load map");
        CloseMap();
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
        //Debug.Log("save map");
        int tempLength = transform.childCount;  
        NoteStruct[] tempNotes = new NoteStruct[tempLength];
        for (int i = 0; i < tempLength; i++)
        {
            Transform child = transform.GetChild(i);        
            
            int intLane = (int) (0.5f * (child.position.x + 30f));
            int intTime = (int) (1f * (child.position.z));
            
            tempNotes[i] = new NoteStruct(intLane, intTime);
        }
 
        tempNotes = tempNotes.OrderBy(note => note.timePosition).ToArray();
        // Create a new instance of the ScriptableObject
        Map newMap = ScriptableObject.CreateInstance<Map>();

        newMap.notes = tempNotes;
        
        // Save the duplicate as a new asset (optional)
        string path = "Assets/new map.asset";
        AssetDatabase.CreateAsset(newMap, path);
        AssetDatabase.SaveAssets(); 
    
    }

    public void CloseMap()
    {
        //Debug.Log("Close map");
        int tempLength = transform.childCount;  
        while(transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
