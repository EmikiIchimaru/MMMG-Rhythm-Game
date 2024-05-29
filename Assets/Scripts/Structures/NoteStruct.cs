using UnityEngine;

[System.Serializable]
public class NoteStruct
{
    public int lane;
    public int timePosition;
    public TouchType touchType;
    public int duration;
    public int slide;

    // Constructor to initialize the fields
    public NoteStruct(int lane, int timePosition, TouchType touchType, int duration, int slide)
    {
        this.lane = lane;
        this.timePosition = timePosition;
        this.touchType = touchType;
        this.duration = duration;
        this.slide = slide;
    }
    //
    /* 
    [HideInInspector] public Song song;
    [HideInInspector] public float realTime { 
        get { return Utility.TimePositionToRealtime(timePosition, song.bpm); } } */
}