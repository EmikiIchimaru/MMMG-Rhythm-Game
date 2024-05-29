using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Note))]
public class NoteMovement : MonoBehaviour
{
    //public float approachRate;
    private Note note;
    private float fallSpeed;
    private float boundary = -10f;
    private float perspectiveRate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        note = GetComponent<Note>();
    }

    // Update is called once per frame
    void Update()
    {
        perspectiveRate = 0.5f + 1f * (transform.position.z/GameManager.Instance.spawnDistance);
        Mathf.Clamp(perspectiveRate, 0.5f, 1.5f);
        fallSpeed = Utility.baseSpeed * GameManager.Instance.approachRate * perspectiveRate;
        transform.position += new Vector3(0f, 0f, -fallSpeed * Time.deltaTime);
        if (transform.position.z < boundary)
        {
            note.DestroyNote();
        }
    }
}
