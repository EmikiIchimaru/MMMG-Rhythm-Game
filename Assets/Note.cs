using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public int lane = 0;
    public TouchType touchType = TouchType.Tap;
    public int duration = 0;
    public int slide = 0;
    public float realtimeHit;
    public string hitsound;

    private bool isHit = false;

    void Update()
    {
        if (!isHit && DetectTouch())
        {
            float hitTiming = GameManager.Instance.currentTrackTime; // Current track time to compare
            ProcessHit(hitTiming);
        }

        // Optional: Add logic to destroy or miss notes if not hit within a certain time
        if (GameManager.Instance.currentTrackTime > realtimeHit + 0.5f) // For example, 0.5 seconds after expected hit
        {
            MissNote();
        }
    }

    private bool DetectTouch()
    {
        // Detect touch input on the screen, specific to lane and position
        // Replace with appropriate logic for touch-based detection
        // For simplicity, this checks for a mouse click/tap on the note object
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void ProcessHit(float hitTiming)
    {
        isHit = true; // Prevent further interactions with this note
        GameManager.Instance.currentNotes.Remove(this);

        // Calculate score based on hit timing and call ScoreManager's OnNoteHit
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.OnNoteHit(hitTiming, realtimeHit);
        }

        Destroy(gameObject); // Destroy the note after it's hit
    }

    private void MissNote()
    {
        // Handle note miss logic here
        isHit = true; // Prevent further interactions with this note
        GameManager.Instance.currentNotes.Remove(this);

        // Notify ScoreManager of the miss
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.OnNoteHit(GameManager.Instance.currentTrackTime, realtimeHit + 999); // large difference to indicate miss
        }

        Destroy(gameObject); // Destroy the note after it's missed
    }
}
