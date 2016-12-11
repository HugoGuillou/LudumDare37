using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLeftBehaviour : MonoBehaviour {

    public float arrowSpeed = 0.1f;
    public LevelManager levelManager;

    private Vector3 movement;

    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        movement = new Vector3(-arrowSpeed, 0f, 0f);
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
        Destroy(gameObject);
        if (coll.gameObject.tag == "Player")
        {
            levelManager.RespawnPlayer();
        }
    }
}
