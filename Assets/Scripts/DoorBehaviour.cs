using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour {


    private bool isIn;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Activate();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isIn = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isIn = false;
        }
    }

    void Activate()
    {
        if(isIn && Input.GetButtonDown("Action"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

}
