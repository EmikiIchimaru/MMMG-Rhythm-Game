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

    public void DestroyNote()
    {
        GameManager.Instance.currentNotes.Remove(this);
        Destroy(gameObject);
    }
}
