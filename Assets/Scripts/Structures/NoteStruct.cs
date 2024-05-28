using UnityEngine;

[System.Serializable]
public struct NoteStruct
{
    public int lane;
    public int timePosition;

    //
    /* 
    [HideInInspector] public Song song;
    [HideInInspector] public float realTime { 
        get { return Utility.TimePositionToRealtime(timePosition, song.bpm); } } */
}