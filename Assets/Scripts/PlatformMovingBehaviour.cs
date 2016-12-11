using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovingBehaviour : MonoBehaviour {

    public float distance = 3.0f;
    public float movingSpeed = 0.1f;
    public bool moveVertical = false;
    public bool stop = false;
    public bool invert = false;

    private Vector3 platformMove;
    private Vector2 platformSpeed;
    private Transform centre;

    private Rigidbody2D body;

    // Use this for initialization
    void Start () {

        body = GetComponent<Rigidbody2D>();

        if (invert)
        {
            if (moveVertical)
            {
                //platformMove = new Vector3(0f, -movingSpeed, 0f);
                platformSpeed = new Vector2(0f, -movingSpeed);
            }
            else
            {
                //platformMove = new Vector3(-movingSpeed, 0f, 0f);
                platformSpeed = new Vector2(-movingSpeed, 0f);

            }
            centre = transform.parent.transform;
        }
        else
        {
            if (moveVertical)
            {
                //platformMove = new Vector3(0f, movingSpeed, 0f);
                platformSpeed = new Vector2(0f, movingSpeed);

            }
            else
            {
                //platformMove = new Vector3(movingSpeed, 0f, 0f);
                platformSpeed = new Vector2(movingSpeed, 0f);

            }
            centre = transform.parent.transform;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
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
                //platformMove = new Vector3(0f, movingSpeed, 0f);
                platformSpeed = new Vector2(0f, movingSpeed);

            }
            else
            {
                if (transform.position.y >= centre.position.y+distance)
                {
                    //platformMove = new Vector3(0f, -movingSpeed, 0f);
                    platformSpeed = new Vector2(0f, -movingSpeed);
                }
            }
        }
        else
        {
            if(transform.position.x <= centre.position.x-distance)
            {
                platformSpeed = new Vector2(movingSpeed, 0f);
                //platformMove = new Vector3(movingSpeed, 0f, 0f);
            }
            else
            {
                if (transform.position.x >= centre.position.x+distance)
                {
                    platformSpeed = new Vector2(-movingSpeed, 0f);
                   // platformMove = new Vector3(-movingSpeed, 0f, 0f);
                }
            }
            
        }

        //transform.position += platformMove;
        body.velocity = platformSpeed;
        print(platformSpeed);
        

    }

    public void Activate()
    {
        stop = false;
    }

    public void Desactivate()
    {
        stop = true;
    }
}
