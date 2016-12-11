using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWallTrigger : MonoBehaviour {

    public GameObject laserWall;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            laserWall.GetComponent<DeathWallsBehaviour>().Activate();
        }
    }
}
