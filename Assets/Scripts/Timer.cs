using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float timeRemaining = 300.0f;
    public GameObject Pastas;

    private string timer;
    private Text txt;
    private float time;
	// Use this for initialization
	void Start () {
        time = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if(timeRemaining > 0)
            timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0f;
            Pasta();
        }
        Affichage();
        txt = gameObject.GetComponent<Text>();
        txt.text = timer;
    }

    void Affichage()
    {
        string minutes;
        if (timeRemaining == 0f)
        {
            minutes = "00";
        }
        else
        {
            minutes = Mathf.Floor(timeRemaining / 60).ToString("00");
        }
        
        string seconds = (timeRemaining % 60).ToString("00");
        timer = minutes + " : " + seconds;
    }

    void Pasta()
    {
        time += Time.deltaTime;
        if(time >= 2.0f)
        {
            Pastas.SetActive(false);
        }
        else
        {
            Pastas.SetActive(true);
        }
    }
}
