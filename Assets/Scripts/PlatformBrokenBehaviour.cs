using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBrokenBehaviour : MonoBehaviour {

    public float plateformBrokenDuration = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("enter");
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("enter");
            Disapear();
        }
    }

    void Disapear()
    {
        //Feedback visuel
        Destroy(gameObject, plateformBrokenDuration);
    }
}
