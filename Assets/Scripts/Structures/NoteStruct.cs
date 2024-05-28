using UnityEngine;

[System.Serializable]
public class NoteStruct
{
    public int lane;
    public int timePosition;

    // Constructor to initialize the fields
    public NoteStruct(int lane, int timePosition)
    {
        this.lane = lane;
        this.timePosition = timePosition;
    }
    //
    /* 
    [HideInInspector] public Song song;
    [HideInInspector] public float realTime { 
        get { return Utility.TimePositionToRealtime(timePosition, song.bpm); } } */
}