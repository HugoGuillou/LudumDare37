using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckpoint;
    public DeathWallsBehaviour[] laserWalls;
    public LavaWallBehaviour[] lavaWalls;

    private CharacterController player;
    private GameObject[] platformsBroke;
    private GameObject[] arrows;
    private GameObject[] platformsMove;
    private GameObject[] switches;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<CharacterController>();
        platformsBroke = GameObject.FindGameObjectsWithTag("PlatformBroken");
        platformsMove = GameObject.FindGameObjectsWithTag("MovingPlatform");
        arrows = GameObject.FindGameObjectsWithTag("Arrow");
        switches = GameObject.FindGameObjectsWithTag("SwitchArrow");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RespawnPlayer()
    {
        player.transform.position = currentCheckpoint.transform.position;
        for (int i = 0; i < laserWalls.Length; ++i)
        {
            laserWalls[i].Desactivate();
            laserWalls[i].SetPositionInitial();
        }
        for (int j = 0; j < lavaWalls.Length; ++j)
        {
            lavaWalls[j].Desactivate();
            lavaWalls[j].SetPositionInitial();
        }
        foreach (GameObject platform in platformsBroke)
        {
            platform.GetComponent<PlatformBrokenBehaviour>().Reset();
            platform.SetActive(true);
        }
        foreach (GameObject platform in platformsMove)
        {
            platform.GetComponent<PlatformMovingBehaviour>().Reset();
        }
        foreach (GameObject arrow in arrows)
        {
            Destroy(arrow);
        }
        foreach (GameObject switchArrow in switches)
        {
            switchArrow.GetComponent<SwitchArrowBehaviour>().Reset();
        }
    }
}
