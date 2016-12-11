using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {
    public AudioClip spawnArrow;

    public bool vertical = false;
    public bool negative = false;
    public GameObject arrowsDown;
    public GameObject arrowsUp;
    public GameObject arrowsLeft;
    public GameObject arrowsRight;
    public float arrowDelay = 1.0f;
    public int numberOfArrows = 1;

    private Vector3 resetPos;
    private Quaternion rotation;

	// Use this for initialization
	void Start () {
        resetPos = new Vector3(0f, 0f, 0f);
        rotation = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void /*IEnumerator*/ Activate()
    {
        if(vertical)
        {
            rotation.eulerAngles = new Vector3(0f, 0f, 90.0f);
            if (negative)
            {
                //vers le haut
                for(int i = 0; i < numberOfArrows; ++i)
                {
                    Instantiate(arrowsDown, transform.position, rotation);
                    //playSound
                    GetComponent<AudioSource>().PlayOneShot(spawnArrow);
                    //yield return new WaitForSeconds(arrowDelay);
                }         
            }
            else
            {
                //vers le bas
                for (int i = 0; i < numberOfArrows; ++i)
                {
                    Instantiate(arrowsUp, transform.position, rotation);
                    //playSound
                    GetComponent<AudioSource>().PlayOneShot(spawnArrow);
                    //yield return new WaitForSeconds(arrowDelay);
                }
            }
        }
        else
        {
            if(negative)
            {
                //vers la gauche
                for (int i = 0; i < numberOfArrows; ++i)
                {
                    Instantiate(arrowsLeft, transform.position, rotation);
                    GetComponent<AudioSource>().PlayOneShot(spawnArrow);
                    //playSound
                    //yield return new WaitForSeconds(arrowDelay);
                }
            }
            else
            {
                //vers la droite
                for (int i = 0; i < numberOfArrows; ++i)
                {
                    Instantiate(arrowsRight, transform.position, rotation);
                    GetComponent<AudioSource>().PlayOneShot(spawnArrow);
                    //playSound
                    //yield return new WaitForSeconds(arrowDelay);
                }
            }
        }
    }
}
