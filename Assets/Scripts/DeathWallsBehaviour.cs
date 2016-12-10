﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWallsBehaviour : MonoBehaviour {

    public float wallSpeed = 0.1f;

    private Vector3 wallMove;

	// Use this for initialization
	void Start () {
        wallMove = new Vector3(-wallSpeed, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }

    void Move()
    {
        transform.position += wallMove;
    }
}
