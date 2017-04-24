using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NutTree : MonoBehaviour {

	public NutSystem ns;
	public int nutsStoredInTree;
	public int nutsToWin;
	public bool isCompleted;
	public AudioSource winSound;

	void OnTriggerEnter(Collider giver){
		if(giver.name.Equals("Squirrel")){
			nutsStoredInTree += ns.nutsObtained;
			ns.nutsObtained = 0;
			check (giver);
		}
	}

	void check(Collider giver){
		if (nutsStoredInTree >= nutsToWin) {
			Debug.Log ("You Win");
			//In the future we should stop LevelMusic here
			winSound.Play ();
			giver.gameObject.SetActive (false);
			Canvas uiMain = GameObject.Find ("UI").GetComponent<Canvas> ();
			uiMain.enabled = false;
			Canvas winScreen = GameObject.Find ("WinScreen").GetComponent<Canvas> ();
			winScreen.enabled = true;
			isCompleted = true;
		}
	}

}
