﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBrokenBehaviour : MonoBehaviour {

    public float plateformBrokenDuration = 1.0f;
    public AudioClip land;
    public AudioClip brokeSound;

    private float time;
    private bool isActivated;

	// Use this for initialization
	void Start () {
        time = 0f;
        isActivated = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(isActivated)
        {
            Disapear();
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            //PlaySound Land
            GetComponent<AudioSource>().PlayOneShot(land);
            isActivated = true;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    void Disapear()
    {
        time += Time.deltaTime;

        if (time >= plateformBrokenDuration)
        {
            gameObject.SetActive(false);
            time = 0f;
            isActivated = false;
            //Feedback visuel
            //PlaySound Disparition
            GetComponent<AudioSource>().PlayOneShot(brokeSound);
        }
    }

    public void Reset()
    {
        time = 0f;
        isActivated = false;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
