﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLavaBehaviour : MonoBehaviour {

    public LevelManager levelManager;

    // Use this for initialization
    void Start () {
        levelManager = FindObjectOfType<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            levelManager.RespawnPlayer();
        }
    }
}
