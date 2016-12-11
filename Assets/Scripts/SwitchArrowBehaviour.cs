using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchArrowBehaviour : MonoBehaviour {

    public GameObject movingPlatform;
    public GameObject arrowSpawner;
    public bool spawnArrow = true;


    private bool sawOnce;
	// Use this for initialization
	void Start () {
        sawOnce = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //affiche feedback
            if(Input.GetButtonDown("Action"))
            {
                if(spawnArrow)
                {
                    arrowSpawner.GetComponent<ArrowBehaviour>().Activate();
                }

                if (!sawOnce)
                {
                    if (movingPlatform.GetComponentInChildren<PlatformMovingBehaviour>().stop == true)
                    {
                        movingPlatform.GetComponentInChildren<PlatformMovingBehaviour>().Activate();
                    }
                    else
                    {
                        movingPlatform.GetComponentInChildren<PlatformMovingBehaviour>().Desactivate();
                    }
                    sawOnce = true;
                }
                else
                {
                    sawOnce = false;
                }
            }
        }
    }
}
