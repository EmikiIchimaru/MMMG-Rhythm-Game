using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HitObject))]
public class Approach : MonoBehaviour
{
    
    public float fallSpeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float tempScale = (maxHeight-transform.position.y)/(maxHeight-minHeight);
        //transform.localScale = new Vector3(tempScale, tempScale, 1f);
        transform.position += new Vector3(0f, 0f, -fallSpeed * Time.deltaTime);
        if (transform.position.z <-40f)
        {
            Destroy(gameObject);
        }
    }
}
