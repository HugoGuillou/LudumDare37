using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform player;

    private bool goingUp;

	// Use this for initialization
	void Start () {
        goingUp = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (goingUp)
        {
            transform.position = new Vector3(0f, player.position.y, -10.0f);
        }
        else
        {

            transform.position = new Vector3(player.position.x, 0f, -10.0f);

        }
    }

    void ChangeDirection ()
    {
        if (goingUp)
        {
            goingUp = false;
        }
        else
        {
            goingUp = true;
        }
    }

}
