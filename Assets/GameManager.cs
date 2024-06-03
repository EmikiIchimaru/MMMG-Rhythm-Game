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

    public void GenerateHoldNote(Note note, float lengthScale, bool isInPlayMode)
    {
        Debug.Log($"{lengthScale}");
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
                newPos = new Vector3(note.transform.position.x, 0, transform.position.z + i * lengthScale);
                holdNoteGO = InstantiateGO(touchType, newPos);//add intermediate notes
                //holdNoteGO.transform.Rotate(90f,0,0);
            }
            else
            {
                touchType = TouchType.End;
                newPos = new Vector3(note.transform.position.x, 0, transform.position.z + i * lengthScale);
                holdNoteGO = InstantiateGO(touchType, newPos);
                //holdNoteGO.transform.Rotate(90f,0,0);
                holdNoteGO.GetComponent<NoteTail>().headTransform = note.transform;

            }
        }
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
        //Debug.Log($"play");
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
        Vector3 tempSpawn = new Vector3(noteStruct.lane * 15f/7, 0f, spawnDistance); //spawnPosition + new Vector3(,0f,0f);
        GameObject noteGO = Instantiate(notePrefab, tempSpawn, Quaternion.Euler(90,0,0));
        //noteGO.transform.Rotate(90f,0,0);
        Note note = noteGO.GetComponent<Note>();
        note.lane = noteStruct.lane;
        note.duration = noteStruct.duration;
        float lengthScale = Utility.baseSpeed * approachRate * 1.5f;
        GenerateHoldNote(note, lengthScale, true);
        currentNotes.Add(note);
        //add correction
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
