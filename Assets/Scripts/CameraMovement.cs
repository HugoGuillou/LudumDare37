using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform player;

    private bool goingLeft;

	// Use this for initialization
	void Start () {
        goingLeft = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (goingLeft)
        {
            transform.position = new Vector3(player.position.x - 6.0f, 0f, -10.0f);
        }
        else
        {
            transform.position = new Vector3(player.position.x + 6.0f, 0f, -10.0f);
        }
    }

    void ChangeDirection ()
    {
        if (goingLeft)
        {
            goingLeft = false;
        }
        else
        {
            goingLeft = true;
        }
    }

}
