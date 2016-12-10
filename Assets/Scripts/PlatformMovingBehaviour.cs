using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovingBehaviour : MonoBehaviour {

    public float distance = 3.0f;
    public float movingSpeed = 0.1f;
    public bool moveVertical = false;
    public bool stop = false;

    private Vector3 platformMove;
    private Transform centre;

    // Use this for initialization
    void Start () {
        if(moveVertical)
        {
            platformMove = new Vector3(0f, movingSpeed, 0f);
        }
        else
        {
            platformMove = new Vector3(movingSpeed, 0f, 0f);
        }
        centre = transform.parent.transform;
    }
	
	// Update is called once per frame
	void Update () {
		if(!stop)
        {
            Move();
        }
	}

    void Move()
    {
        if (moveVertical)
        {
            if (transform.position.y <= centre.position.y-distance)
            {
                platformMove = new Vector3(0f, movingSpeed, 0f);
            }
            else
            {
                if (transform.position.y >= centre.position.y+distance)
                {
                    platformMove = new Vector3(0f, -movingSpeed, 0f);
                }
            }
        }
        else
        {
            if(transform.position.x <= centre.position.x-distance)
            {
                platformMove = new Vector3(movingSpeed, 0f, 0f);
            }
            else
            {
                if (transform.position.x >= centre.position.y+distance)
                {
                    platformMove = new Vector3(-movingSpeed, 0f, 0f);
                }
            }
            
        }

        transform.position += platformMove;
    }
}
