using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrapBehaviour : MonoBehaviour {

    public float floorTrapDelay = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            KillPlayer(coll.gameObject);
        }
    }

    void KillPlayer(GameObject Player)
    {

    }
}
