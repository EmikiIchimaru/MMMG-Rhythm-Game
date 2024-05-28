using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Note note = other.gameObject.GetComponent<Note>();
        if (note != null)
        {
            note.DestroyNote();
        }
    }
}
