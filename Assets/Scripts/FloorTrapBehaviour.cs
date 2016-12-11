using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrapBehaviour : MonoBehaviour {

    public float floorTrapDelay = 1.0f;
    public float spikeDelay = 1.0f;

    private float time;
    private bool isOn;
    private bool touched;
    private bool isOut;

	// Use this for initialization
	void Start () {
        time = 0f;
        isOn = false;
        isOut = false;
        touched = false;
	}
	
	// Update is called once per frame
	void Update () {
        Activate();

	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            isOn = true;
            touched = true;
        }
    }

    void OnCollisionExit2D(Collision2D info)
    {
        if(info.transform.CompareTag("Player"))
        {
            isOn = false;
        }
    }

    void Activate()
    {
        if(isOn)
        {
            time += Time.deltaTime;
            if (time >= floorTrapDelay)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                isOut = true;
                time = 0f;
                //playSound Sortie piques
            }
        }
        else
        {
            if(!isOut && touched)
            {
                time += Time.deltaTime;
                if (time >= floorTrapDelay)
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    isOut = true;
                    touched = false;
                    time = 0f;
                    //playSound Sortie piques
                }
            }
            if(isOut)
            {
                time += Time.deltaTime;
                if (time >= spikeDelay)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    isOut = false;
                    touched = false;
                    time = 0f;
                    //playSound rentrée piques
                }
            }
        }
    }
}
