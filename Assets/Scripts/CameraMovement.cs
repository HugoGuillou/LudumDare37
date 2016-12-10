using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform player;
    public float cameraSpeed = 0.1f;
    public Transform topWall;
    public Transform rightWall;
    public Transform leftWall;
    public Transform botWall;
    private Vector2 screenSize; public float colDepth = 4f;

    private float zPosition = 0f;
    private Vector3 cameraPos;
    private bool isOnSafeSpot;
    private Vector3 cameraMove;
    private bool move;

	// Use this for initialization
	void Start () {
        cameraPos = transform.position;
        cameraMove = new Vector3(- cameraSpeed, 0f, 0f);
        isOnSafeSpot = false;
        move = true;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        rightWall.localScale = new Vector3(colDepth, screenSize.y * 2, colDepth);
        rightWall.position = new Vector3(cameraPos.x + screenSize.x + (rightWall.localScale.x * 0.5f), cameraPos.y, zPosition);
        leftWall.localScale = new Vector3(colDepth, screenSize.y * 2, colDepth);
        leftWall.position = new Vector3(cameraPos.x - screenSize.x - (leftWall.localScale.x * 0.5f), cameraPos.y, zPosition);
        topWall.localScale = new Vector3(screenSize.x * 2, colDepth, colDepth);
        topWall.position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (topWall.localScale.y * 0.5f), zPosition);
        botWall.localScale = new Vector3(screenSize.x * 2, colDepth, colDepth);
        botWall.position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (botWall.localScale.y * 0.5f), zPosition);
    }
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        if (move)
        {
            if (isOnSafeSpot)
            {
                transform.position = new Vector3(player.position.x + 6.0f, 0f, -10.0f);
            }
            else
            {
                transform.position += cameraMove;
            }
        }
    }

    void StopCamera()
    {
        move = false;
    }

    void MoveCamera()
    {
        move = true;
    }
}
