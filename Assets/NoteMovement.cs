using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Note))]
public class NoteMovement : MonoBehaviour
{
    private Note note;
    private float fallSpeed;
    private float boundary;
    private float perspectiveRate = 1f;
    private bool isDebugged;

    // Start is called before the first frame update
    void Start()
    {
        boundary = -10f;
        note = GetComponent<Note>();
        isDebugged = false;
    }

    // Update is called once per frame
    void Update()
    {
        fallSpeed = Utility.baseSpeed * GameManager.Instance.approachRate * perspectiveRate;
        transform.position += new Vector3(0f, 0f, -fallSpeed * Time.deltaTime);

        if (transform.position.z < 0 && !isDebugged)
        {
            isDebugged = true;
        }

        // Check if note has passed the boundary without being hit
        if (transform.position.z < boundary)
        {
            HandleMiss();
        }
    }

    private void HandleMiss()
    {
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            // Call OnNoteHit with miss timing (you can define how miss is handled in ScoreManager)
            scoreManager.OnNoteHit(float.MaxValue, note.realtimeHit);  // Use float.MaxValue to represent a miss
        }

        Debug.Log($"Note missed. Lane: {note.lane}");
    }
}
