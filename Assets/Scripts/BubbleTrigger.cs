using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTrigger : MonoBehaviour {

    public GameObject objectToEnable;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            objectToEnable.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectToEnable.SetActive(false);
        }
    }
}
