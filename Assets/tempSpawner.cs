using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempSpawner : MonoBehaviour
{
       // Assign the prefab to be instantiated in the Inspector
    public GameObject prefab;

    // Define the position where the prefab will be instantiated
    public Vector3 spawnPosition = new Vector3(0, 5, 0);

    // Define the interval in seconds
    public float interval = 3f;

    void Start()
    {
        // Start the coroutine
        StartCoroutine(SpawnPrefabCoroutine());
    }

    private IEnumerator SpawnPrefabCoroutine()
    {
        while (true)
        {
            // Instantiate the prefab at the defined position with no rotation
            Instantiate(prefab, spawnPosition, Quaternion.identity);

            // Wait for the specified interval before repeating
            yield return new WaitForSeconds(interval);
        }
    }
}
