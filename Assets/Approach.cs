using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectData))]
public class Approach : MonoBehaviour
{
    public float approachRate = 1f;
    private float fallSpeed;
    private float baseSpeed = 20f;
    private float boundary = -40f;
    // Start is called before the first frame update
    void Start()
    {
        fallSpeed = baseSpeed * approachRate;
    }

    // Update is called once per frame
    void Update()
    {
        //float tempScale = (maxHeight-transform.position.y)/(maxHeight-minHeight);
        //transform.localScale = new Vector3(tempScale, tempScale, 1f);
        transform.position += new Vector3(0f, 0f, -fallSpeed * Time.deltaTime);
        if (transform.position.z < boundary)
        {
            Destroy(gameObject);
        }
    }
}
