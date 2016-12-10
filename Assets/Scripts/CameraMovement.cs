using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform player;

    private bool goingLeft;
    private bool goingUp;

	// Use this for initialization
	void Start () {
        goingLeft = false;
        goingUp = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (goingLeft)
        {
            transform.position = new Vector3(player.position.x - 6.0f, 0f, -10.0f);
        }
        else
        {
            if (goingUp)
            {
                transform.position = new Vector3(0f, player.position.y, -10.0f);
            }
            else
            {
                transform.position = new Vector3(player.position.x + 6.0f, 0f, -10.0f);
            }
        }
    }

    void ChangeDirection ()
    {
        goingUp = false;
        if (goingLeft)
        {
            goingLeft = false;
        }
        else
        {
            goingLeft = true;
        }
    }

    void goUp()
    {
        goingUp = true;
        goingLeft = false;
    }

}
