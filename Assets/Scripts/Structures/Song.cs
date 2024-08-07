using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Song", menuName = "ScriptableObjects/Song")]
public class Song : ScriptableObject
{
    public string songName;
    public string difficultyName;
    public Map map;
    public float duration;
    public float bpm;
    public float offset;
    //mp3
    //cover icon
    //difficulty
}
