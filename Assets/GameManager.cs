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
    [SerializeField] private GameObject holdPrefab;
    [SerializeField] private GameObject tailPrefab;
    //private Vector3 spawnPosition { get { return new Vector3(0f, 0f, spawnDistance); } }
    public List<Note> currentNotes = new List<Note>();
    
    // Start is called before the first frame update
    void Start()
    {
        isPlaying = false;
        PlayLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying) { return; }
        if (currentTrackTime > 0 && !hasSongStarted) { StartSong(); }
        if (currentTrackTime > song.duration) { StopSong(); }
        if (currentObjectIndex < map.notes.Length) { InstantiateNotes(); }
        currentTrackTime += Time.deltaTime;
    }

    public void GenerateHoldNote(Note note, float lengthScale, bool isInPlayMode)
    {
        //Debug.Log($"{lengthScale}");
        //Transform[] notes = new Transform[duration+1];
        //notes[0] = transform;
        
        for (int i = 1; i <= note.duration; i++)
        {
            TouchType touchType;
            Vector3 newPos;
            GameObject holdNoteGO;
            if (i < note.duration)
            {
                touchType = TouchType.Hold;
                newPos = new Vector3(note.transform.position.x, 0, note.transform.position.z + i * lengthScale);
                holdNoteGO = InstantiateGO(touchType, newPos);//add intermediate notes
            }
            else
            {
                touchType = TouchType.End;
                newPos = new Vector3(note.transform.position.x, 0, note.transform.position.z + i * lengthScale);
                holdNoteGO = InstantiateGO(touchType, newPos);
                holdNoteGO.GetComponent<NoteTail>().headTransform = note.transform;
            }

            Note newNote = holdNoteGO.GetComponent<Note>();
            newNote.lane = note.lane;
        }
        //Time.timeScale = 0f;
    }

    public GameObject InstantiateGO(TouchType touchType, Vector3 position)
    {
        GameObject prefab = TouchTypeToGO(touchType);
        GameObject prefabGO = Instantiate(prefab, position, Quaternion.Euler(90,0,0));
        return prefabGO;
    }

    private void InstantiateNotes()
    {
        while (true)
        {
            float nextNoteRealtimeHit = Utility.TimePositionToRealtime(map.notes[currentObjectIndex].timePosition, bpm, song.offset);
            float actualSpawnDistance;
            //Debug.Log($"next note rt hit: {nextNoteRealtimeHit}");
            if (Utility.ShouldInstantiateNote(nextNoteRealtimeHit, currentTrackTime, out actualSpawnDistance))
            {
                CreateNote(map.notes[currentObjectIndex], actualSpawnDistance, nextNoteRealtimeHit);
                //Debug.Log($"next note rt hit : {nextNoteRealtimeHit}");
                currentObjectIndex++;
                break;
            }
            else
            {
                break;
            }
        }
    }

    private void CreateNote(NoteStruct noteStruct, float actualSpawnDistance, float realtimeHit)
    {
        Vector3 tempSpawn = new Vector3(noteStruct.lane * 15f/7, 0f, actualSpawnDistance); //spawnPosition + new Vector3(,0f,0f);
        GameObject noteGO = Instantiate(notePrefab, tempSpawn, Quaternion.Euler(90,0,0));
        //noteGO.transform.Rotate(90f,0,0);
        Note note = noteGO.GetComponent<Note>();
        note.lane = noteStruct.lane;
        note.realtimeHit = realtimeHit;
        note.duration = noteStruct.duration;
        float lengthScale = Utility.GetBaseTimeUnit(song.bpm) * Utility.baseSpeed * approachRate * 1f;
        GenerateHoldNote(note, lengthScale, true);
        currentNotes.Add(note);
        //add correction
    }

    private void PlayLevel()
    {
        isPlaying = true;
        hasSongStarted = false;
        currentTrackTime = -3f;
        currentObjectIndex = 0;
    }

    private void StartSong()
    {
        //Debug.Log($"play");
        hasSongStarted = true;
        AudioManager.Instance.Play(song.songName);
        //Debug.Log($"{Time.time}");
    }

    private void StopSong()
    {
        Debug.Log($"completed notes: {map.notes.Length}");
        isPlaying = false;
    }



    private GameObject TouchTypeToGO(TouchType touchType)
    {
        switch (touchType)
        {
            case TouchType.Tap:
                return notePrefab;
            case TouchType.Hold:
                return holdPrefab;
            case TouchType.End:
                return tailPrefab;
            default:
                Debug.Log("touchtype to GO null");
                return null;
        }
    }



/*     public void RemoveNote(NoteData noteData)
    {
        currentNotes.Remove(noteData);
    } */
}
