﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrapBehaviour : MonoBehaviour {

    public float floorTrapDelay = 1.0f;
    public LevelManager levelManager;

    private float time;

	// Use this for initialization
	void Start () {
        levelManager = FindObjectOfType<LevelManager>();
        time = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionStay2D(Collision2D coll)
    {
        Debug.Log("B" + time);
        if (coll.gameObject.tag == "Player")
        {
            time += Time.deltaTime;
            if(time >= floorTrapDelay)
            {
                levelManager.RespawnPlayer();
                //playSound
                //VisualFeedback
            }
        }
    }

    void OnCollisionExit2D(Collision2D info)
    {
        Debug.Log("A" + time);
        if(info.transform.CompareTag("Player"))
        {
            time = 0f;
        }
    }
}
