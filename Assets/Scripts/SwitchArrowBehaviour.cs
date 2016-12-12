using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchArrowBehaviour : MonoBehaviour {

    public GameObject movingPlatform;
    public GameObject arrowSpawner;
    public bool spawnArrow = true;
    public AudioClip buttonSound;


    private bool sawOnce;
	// Use this for initialization
	void Start () {
        sawOnce = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reset()
    {
        GetComponentInChildren<Animator>().SetBool("isOn", false);
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
                        GetComponentInChildren<Animator>().SetBool("isOn", true);
                        GetComponent<AudioSource>().PlayOneShot(buttonSound);
                        //sound
                    }
                    else
                    {
                        movingPlatform.GetComponentInChildren<PlatformMovingBehaviour>().Desactivate();
                        GetComponentInChildren<Animator>().SetBool("isOn", false);
                        GetComponent<AudioSource>().PlayOneShot(buttonSound);
                        //sound
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
