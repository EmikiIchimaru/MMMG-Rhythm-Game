using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : Singleton<PlayManager>
{
    public void TouchToGameInput(int touchIndex, Vector2 screenPoint, Touch touch)
    {
        TouchType touchType = TouchType.None;
        bool isValid;
        int touchedLane = Utility.LocalPointToLane(screenPoint, out isValid);
        if (!isValid)
        {
            Debug.Log("Touch is not in panel");
            return;
        }

        switch (touch.phase)
        {
            case TouchPhase.Began:
                touchType = TouchType.Tap;
                break;
            case TouchPhase.Stationary:
            case TouchPhase.Moved:
                touchType = TouchType.Hold;
                break;
            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                touchType = TouchType.End;
                break;
        }

        HandlePlayerInput(touchIndex, touchedLane, touchType, screenPoint);
    }

    void HandlePlayerInput(int touchIndex, int touchedLane, TouchType touchType, Vector2 screenPoint)
    {
        foreach (Note note in GameManager.Instance.currentNotes)
        {
            if (touchedLane != note.lane) { continue; }
            if (note.transform.position.z > 10f) { continue; } // Skip notes too far away
            if (touchType == note.touchType)
            {
                HandleTiming(note);
                VFXManager.Instance.HitVFX(note.transform.position);
                break;
            }
        }
    }

    void HandleTiming(Note note)
    {
        AudioManager.Instance.Play(note.hitsound);

        float hitTiming = GameManager.Instance.currentTrackTime - note.realtimeHit;
        Debug.Log($"Hit Timing: {hitTiming}");

        // Pass hit timing to ScoreManager
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.OnNoteHit(GameManager.Instance.currentTrackTime, note.realtimeHit);
        }
    }
}
