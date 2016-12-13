using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameState : MonoBehaviour {

	NutTree tree;
	Scene currentScene;
	public static bool[] levels = new bool[4]; //set size to number of levels
	public GameObject[] buttons = new GameObject[4]; //set size to number of levels

	// Use this for initialization
	void Start () {
		tree = GameObject.Find ("TreeModel").GetComponent<NutTree> ();
		currentScene = SceneManager.GetActiveScene ();
		allowToSelect ();
	}
	
	// Update is called once per frame
	void Update () {
		if (tree.isCompleted) {
			if (currentScene.name.Equals ("Tutorial")) {
				Debug.Log ("Level One Unlocked");
				levels [0] = true;
				allowToSelect ();
			}
			if (currentScene.name.Equals ("LevelOne")) {
				Debug.Log ("Level Two Unlocked");
				levels [1] = true;
				allowToSelect ();
			}
			if (currentScene.name.Equals ("LevelTwo")) {
				Debug.Log ("Level Three Unlocked");
				levels [2] = true;
				allowToSelect ();
			}
			if (currentScene.name.Equals ("LevelThree")) {
				Debug.Log ("Level Four Unlocked");
				levels [3] = true;
				allowToSelect ();
			}
		} else if (currentScene.name.Equals("MainMenu")){
			allowToSelect ();
		}
	}

	void allowToSelect(){
		for (int i = 0; i < levels.Length; i++) {
			if (levels [i] == true)
				buttons [i].SetActive (true);
		}
	}
}
