using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Note : MonoBehaviour
{
    public int lane = 0;
    public TouchType touchType = TouchType.Tap;
    public void DestroyNote()
    {
        GameManager.Instance.currentNotes.Remove(this);
        Destroy(gameObject);
    }
}
