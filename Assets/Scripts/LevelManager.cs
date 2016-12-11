using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckpoint;
    public DeathWallsBehaviour[] laserWalls;
    public LavaWallBehaviour[] lavaWalls;

    private CharacterController player;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RespawnPlayer()
    {
        player.transform.position = currentCheckpoint.transform.position;
        for(int i = 0; i < laserWalls.Length; ++i)
        {
            laserWalls[i].Desactivate();
            laserWalls[i].SetPositionInitial();
        }
        for (int j = 0; j < lavaWalls.Length; ++j)
        {
            lavaWalls[j].Desactivate();
            lavaWalls[j].SetPositionInitial();
        }
    }
}
