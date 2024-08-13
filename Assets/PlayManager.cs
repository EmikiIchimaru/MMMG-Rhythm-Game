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
        //Debug.Log($"Lane: {touchedLane}");
        switch (touch.phase)
        {
            case TouchPhase.Began:
                touchType = TouchType.Tap;
                //Debug.Log($"PlayerInput: {touchIndex}, {touchedLane}, {touchType}, {GameManager.Instance.currentTrackTime}");
                break;
            case TouchPhase.Stationary:
                touchType = TouchType.Hold;
                break;
            case TouchPhase.Moved:
                touchType = TouchType.Hold;
                break;
            case TouchPhase.Ended:
                touchType = TouchType.End;
                break;
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
            if (note.transform.position.z > 10f) { continue; }
            if (touchType == note.touchType)
            {
                //Debug.Log($"Hit");
                HandleTiming(note);
                VFXManager.Instance.HitVFX(note.transform.position);
                note.DestroyNote();
                break;
            }
        }
    }

    void HandleTiming(Note note)
    {
        AudioManager.Instance.Play(note.hitsound);

        Debug.Log(GameManager.Instance.currentTrackTime-note.realtimeHit);
    }
}
