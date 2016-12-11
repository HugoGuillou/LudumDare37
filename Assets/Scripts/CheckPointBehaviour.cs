using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "player")
        {
            levelManager.currentCheckpoint = gameObject;
            Debug.Log("Activated checkpoint at" + transform.position);
        }
    }

    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCo");
    }

    public void KillCo()
    {
        Debug.Log("Player died.");
        
        player.enabled = false;
        camera.isFollowing = false;
        renderer.enabled = false;
        ScoreManager.AddPoints(-pointPenaltyOnDeath);
    }

    public void RespawnCo()
    {
        Debug.Log("Player respawned.");
        player.knockBackCount = 0;
        Instantiate(respawnParticles, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
        player.enabled = true;
        camera.isFollowing = true;
        renderer.enabled = true;
        healthManager.FullHealth();
        healthManager.isDead = false;
        player.transform.position = currentCheckpointPosition;
    }

    public IEnumerator RespawnPlayerCo()
    {
        KillCo();
        yield return new WaitForSeconds(respawnDelay);
        RespawnCo();
    }
}
