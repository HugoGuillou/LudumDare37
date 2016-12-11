using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCheckPointBehaviour : MonoBehaviour {

    public GameObject checkpoint;
    public LevelManager levelManager;

    private bool sawOnce;

    // Use this for initialization
    void Start () {
        levelManager = FindObjectOfType<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //affiche feedback
            if (Input.GetButtonDown("Action"))
            {
                levelManager.currentCheckpoint = checkpoint;
                GetComponentInChildren<Animator>().SetBool("isOn", true);
            }
        }
    }
}
