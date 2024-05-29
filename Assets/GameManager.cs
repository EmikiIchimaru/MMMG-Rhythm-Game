using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Song song;
    private Map map { get { return song.map; } }
    private float bpm { get { return song.bpm; } }

    public float approachRate = 5f;
    public float spawnDistance;
    public bool isPlaying;

    private bool hasSongStarted;
    public float currentTrackTime;
    private int currentObjectIndex;
    [SerializeField] private GameObject notePrefab;
    private Vector3 spawnPosition { get { return new Vector3(0f, 0f, spawnDistance); } }
    public List<Note> currentNotes = new List<Note>();
    
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
        if (currentTrackTime > 0.001f * song.offset && !hasSongStarted) { StartSong(); }
        if (currentTrackTime > song.duration) { StopSong(); }
        if (currentObjectIndex < map.notes.Length) { InstantiateNotes(); }
        currentTrackTime += Time.deltaTime;
    }

    public void GenerateHoldNote(Note note, bool isInPlayMode)
    {
        if (note.duration <= 0) { return; }
        //float lengthScale = 4f;
        //instantiate hold note
        for (int i = 1; i < note.duration; i++)
        {
            //add intermediate notes
        }

    }

    private void InstantiateNotes()
    {
        while (true)
        {
            float nextNoteRealtimeHit = Utility.TimePositionToRealtime(map.notes[currentObjectIndex].timePosition, bpm);
            //Debug.Log($"next note rt hit: {nextNoteRealtimeHit}");
            if (Utility.ShouldInstantiateNote(nextNoteRealtimeHit, currentTrackTime, approachRate))
            {
                CreateNote(map.notes[currentObjectIndex]);
                currentObjectIndex++;
                break;
            }
            else
            {
                break;
            }
        }
    }

    private void PlaySong()
    {
        isPlaying = true;
        hasSongStarted = false;
        currentTrackTime = -3f;
        currentObjectIndex = 0;
    }

    private void StartSong()
    {
        Debug.Log($"play");
        hasSongStarted = true;
        AudioManager.Instance.Play("anime song");
    }

    private void StopSong()
    {
        Debug.Log($"completed notes: {map.notes.Length}");
        isPlaying = false;
    }

    private void CreateNote(NoteStruct noteStruct)
    {
        Vector3 tempSpawn = spawnPosition + noteStruct.lane * new Vector3(15f/7,0f,0f);
        GameObject noteGO = Instantiate(notePrefab, tempSpawn, Quaternion.identity);
        noteGO.transform.Rotate(90f,0,0);
        Note note = noteGO.GetComponent<Note>();
        note.lane = noteStruct.lane;
        GenerateHoldNote(note, true);
        currentNotes.Add(note);
        //add correction
    }




/*     public void RemoveNote(NoteData noteData)
    {
        currentNotes.Remove(noteData);
    } */
}
