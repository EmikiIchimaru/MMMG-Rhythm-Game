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

    public List<Note> currentNotes = new List<Note>();
    public ScoreManager scoreManager; // Reference to ScoreManager

    void Start()
    {
        isPlaying = false;
        PlayLevel();
    }

    void Update()
    {
        if (!isPlaying) { return; }
        if (currentTrackTime > 0 && !hasSongStarted) { StartSong(); }
        if (currentTrackTime > song.duration) { StopSong(); }
        if (currentObjectIndex < map.notes.Length) { InstantiateNotes(); }

        HandleTouchInput();  // Handle touch inputs for note hits
        currentTrackTime += Time.deltaTime;
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    Note hitNote = GetHitNoteAtPosition(touchPosition);

                    if (hitNote != null)
                    {
                        float hitTiming = Time.time;
                        Debug.Log("Note Hit! Updating Score..."); // Debug statement for note hit
                        scoreManager.OnNoteHit(hitTiming, hitNote.realtimeHit);
                        currentNotes.Remove(hitNote);
                        Destroy(hitNote.gameObject);
                    }
                    else
                    {
                        Debug.Log("Missed Note..."); // Debug statement for miss
                        scoreManager.OnNoteHit(Time.time, 0f); // Register miss
                    }
                }
            }
        }
    }

    private Note GetHitNoteAtPosition(Vector3 touchPosition)
    {
        foreach (Note note in currentNotes)
        {
            // You can adjust the hit detection range here
            if (Mathf.Abs(note.transform.position.x - touchPosition.x) < 0.5f)
            {
                return note;
            }
        }
        return null;
    }

    private void InstantiateNotes()
    {
        while (true)
        {
            float nextNoteRealtimeHit = Utility.TimePositionToRealtime(map.notes[currentObjectIndex].timePosition, bpm, song.offset);
            float actualSpawnDistance;
            if (Utility.ShouldInstantiateNote(nextNoteRealtimeHit, currentTrackTime, out actualSpawnDistance))
            {
                CreateNote(map.notes[currentObjectIndex], actualSpawnDistance, nextNoteRealtimeHit);
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
        Vector3 tempSpawn = new Vector3(noteStruct.lane * 15f / 7, 0f, actualSpawnDistance);
        GameObject noteGO = Instantiate(notePrefab, tempSpawn, Quaternion.Euler(90, 0, 0));
        Note note = noteGO.GetComponent<Note>();
        note.lane = noteStruct.lane;
        note.realtimeHit = realtimeHit;
        note.duration = noteStruct.duration;
        float lengthScale = Utility.GetBaseTimeUnit(song.bpm) * Utility.baseSpeed * approachRate * 1f;
        GenerateHoldNote(note, lengthScale, true);
        currentNotes.Add(note);
    }

    public void GenerateHoldNote(Note note, float lengthScale, bool isInPlayMode)
    {
        for (int i = 1; i <= note.duration; i++)
        {
            TouchType touchType;
            Vector3 newPos;
            GameObject holdNoteGO;
            if (i < note.duration)
            {
                touchType = TouchType.Hold;
                newPos = new Vector3(note.transform.position.x, 0, note.transform.position.z + i * lengthScale);
                holdNoteGO = InstantiateGO(touchType, newPos);  // Add intermediate notes
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
    }

    public GameObject InstantiateGO(TouchType touchType, Vector3 position)
    {
        GameObject prefab = TouchTypeToGO(touchType);
        GameObject prefabGO = Instantiate(prefab, position, Quaternion.Euler(90, 0, 0));
        return prefabGO;
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
                Debug.Log("TouchType to GO null");
                return null;
        }
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
        hasSongStarted = true;
        AudioManager.Instance.Play(song.songName);
    }

    private void StopSong()
    {
        Debug.Log($"Completed notes: {map.notes.Length}");
        isPlaying = false;
    }
}
