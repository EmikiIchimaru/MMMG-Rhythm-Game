using UnityEngine;

[System.Serializable]
public class Note
{
    public int lane;
    public int timePosition;

    //
    /* 
    [HideInInspector] public Song song;
    [HideInInspector] public float realTime { 
        get { return Utility.TimePositionToRealtime(timePosition, song.bpm); } } */
}