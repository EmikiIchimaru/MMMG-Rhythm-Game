using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay : MonoBehaviour
{
	public GameObject button1;
	public GameObject button2;
	public GameObject text1;
	public GameObject text2;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnDelay", 95);
    }

    // Update is called once per frame
    private void SpawnDelay()
    {
        button1.SetActive(true);
		button2.SetActive(true);
		text1.SetActive(true);
		text2.SetActive(true);
    }
}
