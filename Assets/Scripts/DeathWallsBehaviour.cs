using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWallsBehaviour : MonoBehaviour {

    public float wallSpeed = 0.1f;
    public bool stop = false;

    private Vector3 wallMove;

	// Use this for initialization
	void Start () {
        wallMove = new Vector3(-wallSpeed, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        if(!stop)
        {
            Move();
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }

    void Move()
    {
        transform.position += wallMove;
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
