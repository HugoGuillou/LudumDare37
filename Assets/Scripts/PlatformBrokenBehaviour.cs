using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBrokenBehaviour : MonoBehaviour {

    public float plateformBrokenDuration = 1.0f;

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
            isActivated = true;
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
            //PlaySound
        }
    }

    public void Reset()
    {
        time = 0f;
        isActivated = false;
    }
}
