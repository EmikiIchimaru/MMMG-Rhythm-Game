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
    public float currentTrackTime;
    private int currentObjectIndex;
    [SerializeField] private GameObject notePrefab;
    private Vector3 spawnPosition { get { return new Vector3(0f, 0.1f, spawnDistance); } }
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
        if (currentTrackTime > song.duration) { StopSong(); }
        if (currentObjectIndex < map.notes.Length) { InstantiateNotes(); }
        currentTrackTime += Time.deltaTime;
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
        currentTrackTime = -3f + 0.001f * song.offset;
        currentObjectIndex = 0;
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
        currentNotes.Add(note);
        //add correction
    }

/*     public void RemoveNote(NoteData noteData)
    {
        currentNotes.Remove(noteData);
    } */
}
