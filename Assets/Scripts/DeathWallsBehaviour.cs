﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWallsBehaviour : MonoBehaviour {

    public float wallSpeed = 0.1f;
    public bool stop = false;
    public LevelManager levelManager;

    private Vector3 initialPos;
    private Vector3 deplac;

    // Use this for initialization
    void Start () {
        levelManager = FindObjectOfType<LevelManager>();
        initialPos = transform.position;
        if(stop)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        deplac = new Vector3(-wallSpeed, 0f, 0f);
    }
	
	// Update is called once per frame
	void Update () {
        if(!stop)
        {
            Move();
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            levelManager.RespawnPlayer();
        }
    }

    void Move()
    {
        //transform.position += wallMove;
        transform.position += deplac * Time.deltaTime;

    }

    public void Activate()
    {
        stop = false;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Desactivate()
    {
        stop = true;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void SetPositionInitial()
    {
        transform.position = initialPos;
    }
}
