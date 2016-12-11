﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckpoint;

    private CharacterController player;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RespawnPlayer()
    {
        player.transform.position = currentCheckpoint.transform.position;
    }
}
