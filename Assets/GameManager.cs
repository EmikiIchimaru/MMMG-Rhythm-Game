using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Song song;
    private Map map { get { return song.map; } }
    private float bpm { get { return song.bpm; } }

    public float approachRate = 5f;

    public bool isPlaying;
    private float currentTrackTime;

    private int currentObjectIndex;

    [SerializeField] private GameObject notePrefab;
    [SerializeField] private Vector3 spawnPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        isPlaying = false;
        PlaySong();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying) { return; }
        if (currentObjectIndex >= map.notes.Length) 
        { 
            StopSong();
            return; 
        }
        while (true)
        {
            float nextNoteRealtimeHit = Utility.TimePositionToRealtime(map.notes[currentObjectIndex].timePosition, bpm);
            Debug.Log($"next note rt hit: {nextNoteRealtimeHit}");
            if (Utility.ShouldInstantiateNote(nextNoteRealtimeHit, currentTrackTime, approachRate))
            {
                CreateNote(map.notes[currentObjectIndex]);
                currentObjectIndex++;
                if (currentObjectIndex >= map.notes.Length) 
                {
                    StopSong();
                    break;
                }
            }
            else
            {
                break;
            }
        }
        currentTrackTime += Time.deltaTime;
    }

    private void PlaySong()
    {
        isPlaying = true;
        currentTrackTime = -3f;
        currentObjectIndex = 0;
    }

    private void StopSong()
    {
        Debug.Log($"completed notes: {map.notes.Length}");
        isPlaying = false;
    }

    private void CreateNote(Note note)
    {
        Vector3 tempSpawn = spawnPosition + note.lane * new Vector3(2f,0f,0f);
        GameObject noteGO = Instantiate(notePrefab, tempSpawn, Quaternion.identity);
        noteGO.transform.Rotate(90f,0,0);

    }
}
