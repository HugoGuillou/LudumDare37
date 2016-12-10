﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaWallStopTrigger : MonoBehaviour {

    public GameObject lavaWall;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lavaWall.GetComponent<LavaWallBehaviour>().Desactivate();
        }
    }
}
