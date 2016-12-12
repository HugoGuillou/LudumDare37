using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaWallBehaviour : MonoBehaviour {

    public float wallSpeed = 5.0f;
    public bool stop = false;
    public LevelManager levelManager;

    private Vector3 wallMove;
    private Vector3 initialPos;

    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        wallMove = new Vector3(0f, wallSpeed, 0f);
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!stop)
        {
            Move();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            levelManager.RespawnPlayer();
        }
    }

    void Move()
    {
        transform.position += wallMove * Time.deltaTime;
        transform.position = new Vector2(Mathf.Sin(Time.time), transform.position.y);
    }

    public void Activate()
    {
        stop = false;
    }

    public void Desactivate()
    {
        stop = true;
    }

    public void SetPositionInitial()
    {
        transform.position = initialPos;
    }
}
