using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTail : MonoBehaviour
{
    
    public Transform headTransform;
    private LineRenderer lr;
    
    void Awake()
    { 
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        UpdateLinePositions();
    }

    private void UpdateLinePositions()
    {
        lr.SetPosition(0, transform.position);
        Vector3 headPos = (headTransform != null)?headTransform.position:new Vector3(transform.position.x, 0, -9.9f);
        lr.SetPosition(1, headPos);
        //Debug.Log($"{headTransform.position}");
        /* for (int i = 0; i < duration; i++)
        {
            lr.SetPosition(i, notes[i].position);
        } */
    }
}
