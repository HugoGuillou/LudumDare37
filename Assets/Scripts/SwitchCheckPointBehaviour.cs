using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCheckPointBehaviour : MonoBehaviour {

    public GameObject checkpoint;
    public LevelManager levelManager;
    public AudioClip switchSound;

    private bool sawOnce;
    private bool wasUsed;

    // Use this for initialization
    void Start () {
        levelManager = FindObjectOfType<LevelManager>();
        sawOnce = false;
        wasUsed = false;
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
                if(!wasUsed)
                {
                    GetComponent<AudioSource>().PlayOneShot(switchSound);
                    wasUsed = true;
                }             
            }
        }
    }
}
