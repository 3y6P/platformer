using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScenes : MonoBehaviour {

	public void loadScene(string nameScene)
	{
		SceneManager.LoadScene(nameScene);
	}
	public void mainMenu(){
		loadScene("mainMenuScene");
	}
	public void newGame(){
		loadScene("gameScene");
	}
	public void exitGame(){
		Application.Quit();
	}
}
