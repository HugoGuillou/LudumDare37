using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowUpBehaviour : MonoBehaviour {

    public float arrowSpeed = 0.1f;

    private Vector3 movement;

    // Use this for initialization
    void Start()
    {
        movement = new Vector3(0f, arrowSpeed, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += movement;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Destroy(coll.gameObject);
        }
        Destroy(gameObject);
    }
}
