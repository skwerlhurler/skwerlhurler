using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string startLevel;
	public string loadSelect;
	public string unloadLevel;
	
	
	public void NewGame(){
		SceneManager.LoadScene(startLevel);
	}
	
	public void LoadGame(){
		SceneManager.LoadScene(loadSelect);
	}
	
	public void QuitGame(){
		SceneManager.UnloadScene(unloadLevel);
	}
}
