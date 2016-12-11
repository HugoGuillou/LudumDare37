using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapArrowBehaviour : MonoBehaviour {

    private Transform arrows;
	// Use this for initialization
	void Start () {
		arrows = transform.GetChild(0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GetComponentInChildren<Animator>().SetBool("isOn", true);
            arrows.GetComponent<ArrowBehaviour>().Activate();
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        GetComponentInChildren<Animator>().SetBool("isOn", false);
    }
}
