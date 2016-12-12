using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public string MenuScene 		  = "StartMenu";
	public string TutoCinematicScene = "TutoCinematic";
	public string TutoScene          = "BenjiScene";
	public string GameCinematicScene = "GameCinematic";
	public string GameScene 		  = "BenjiSceneLevel1";
	public string EndCinematicScene  = "EndCinematic";
	public string EndScene  		  = "Ending";

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	public void StartMenuScene()
	{
		SceneManager.LoadScene(MenuScene);
	}


	public  void StartTutoCineScene()
	{
		SceneManager.LoadScene(TutoCinematicScene);
	}


	public  void StartTutoScene()
	{
		SceneManager.LoadScene(TutoScene);
	}


	public  void StartGameCineScene()
	{
		SceneManager.LoadScene(GameCinematicScene);
	}


	public  void StartGameScene()
	{
		SceneManager.LoadScene(GameScene);
	}


	public  void StartEndCineScene()
	{
		SceneManager.LoadScene(EndCinematicScene);
	}


	public  void StartEndScene()
	{
		SceneManager.LoadScene(EndScene);
	}

	public void Exit()
	{
		Application.Quit();
	}

}
