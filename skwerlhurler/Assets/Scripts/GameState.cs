using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameState : MonoBehaviour {

	NutTree tree;
	Scene currentScene;
	public static bool[] levels = new bool[3];
	public GameObject[] buttons = new GameObject[3];

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
