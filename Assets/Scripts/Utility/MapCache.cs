using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class MapCache : Singleton<MapCache>
{
    public Map map;

    private float xOffset = 30f;

    private readonly float spacing = 2f;

    [SerializeField] private GameObject notePrefab;
    [SerializeField] private GameObject holdPrefab;
    [SerializeField] private GameObject tailPrefab;
    private Vector3 spawnPosition;
    public void LoadMap()
    {
        //Debug.Log("load map");
        CloseMap();
        for (int i = 0; i < map.notes.Length; i++) 
        {
            //GameObject tempPrefab = null;
            float tempX = map.notes[i].lane * spacing - xOffset;
            float tempZ = map.notes[i].timePosition * spacing;
            Vector3 tempSpawn = new Vector3(tempX, 0, tempZ);
            /* switch (map.notes[i].touchType)
            {
                case TouchType.Tap:
                    tempPrefab = notePrefab;
                    break;
                case TouchType.Hold:
                    tempPrefab = holdPrefab;
                    break;
                case TouchType.End:
                    tempPrefab = tailPrefab;
                    break;
            } */
            GameObject noteGO = Instantiate(notePrefab, tempSpawn, Quaternion.Euler(90,0,0), transform);
            //noteGO.transform.Rotate(90f,0,0);
            Note note = noteGO.GetComponent<Note>();
            note.lane = map.notes[i].lane;
            note.touchType = map.notes[i].touchType;
            note.duration = map.notes[i].duration;
            note.slide = map.notes[i].slide;
            //note.GenerateHoldNote(spacing, false);
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
            
            int intLane = (int) ((child.position.x + xOffset)/spacing);
            float floatTime = (child.position.z)/spacing;
            Note note = child.GetComponent<Note>();
            TouchType touchType = note.touchType;
            int intDuration = note.duration;
            int intSlide = note.slide;
            tempNotes[i] = new NoteStruct(intLane, floatTime, touchType, intDuration, intSlide);
        }
        //use system linq to sort the array
        tempNotes = tempNotes.OrderBy(note => note.timePosition).ToArray();
        
        map.notes = tempNotes;
        EditorUtility.SetDirty(map);
    
        // Save all unsaved assets to ensure the changes persist
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

    public void GenerateHoldNotes()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Note note = obj.GetComponent<Note>();
            if (note != null) { GameManager.Instance.GenerateHoldNote(note, spacing, false); }
        }
    }
}
