using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(NoteData))]
public class Approach : MonoBehaviour
{
    //public float approachRate;
    private float fallSpeed;

    private float perspectiveRate = 1f;
    private float boundary = -10f;
    private float maxHeight = 140f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        perspectiveRate = 0.5f + 1f * (transform.position.z/maxHeight);
        fallSpeed = Utility.baseSpeed * GameManager.Instance.approachRate * perspectiveRate;
        transform.position += new Vector3(0f, 0f, -fallSpeed * Time.deltaTime);
        if (transform.position.z < boundary)
        {
            Destroy(gameObject);
        }
    }
}
