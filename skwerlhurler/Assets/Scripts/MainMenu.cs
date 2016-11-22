using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string loadLevel;
	public string unloadLevel;
	
	public void LoadLevel(){
		SceneManager.LoadScene(loadLevel);
	}
	
	public void UnloadLevel(){
		SceneManager.LoadScene (unloadLevel);
	}

	public void SelectLevel(string s){
		SceneManager.LoadScene (s);
	}
}
