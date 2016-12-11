using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoorBehaviour : MonoBehaviour {
    public Transform closedDoor;
    public Transform openDoor;

    private bool isClosed;
    private bool sawOnce;
	// Use this for initialization
	void Start () {
        isClosed = true;
        sawOnce = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetButtonDown("Action"))
        {
            if (!sawOnce)
            {
                if (isClosed)
                {
                    closedDoor.gameObject.SetActive(false);
                    openDoor.gameObject.SetActive(true);
                    isClosed = false;
                }
                else
                {
                    closedDoor.gameObject.SetActive(true);
                    openDoor.gameObject.SetActive(false);
                    isClosed = true;
                }
                sawOnce = true;
            }
            else
            {
                sawOnce = false;
            }
        }
    }
}
