using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Song", menuName = "ScriptableObjects/Song")]
public class Song : ScriptableObject
{
    public Map map;
    public float duration;
    public float bpm;
    public float offset;
    //mp3
    //cover icon
    //difficulty
}
