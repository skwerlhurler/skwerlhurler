using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour {

	public GameObject menuName;
	public bool pause;
	
	void Start(){
		pause = false;
	}
	
	public void exit(){
		SceneManager.LoadScene("MainMenu");
	}
	
	public void togglePause(){
		if (!pause) pause = true;
		else if (pause) pause = false;
	}
}
