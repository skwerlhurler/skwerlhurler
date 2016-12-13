using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NutTree : MonoBehaviour {

	public NutSystem skwerl;
	public int nutsStoredInTree;
	public int nutsToWin;
	public Text display;
	public bool isCompleted;

	void OnTriggerEnter(Collider giver){
		if(giver.name.Equals("Squirrel")){
			nutsStoredInTree += skwerl.nutsObtained;
			skwerl.nutsObtained = 0;
			display.text = "Number of Acorns: " + nutsStoredInTree;
			check ();
		}
	}

	void check(){
		if (nutsStoredInTree >= nutsToWin) {
			Debug.Log ("You Win");
			skwerl.gameObject.SetActive (false);
			Canvas uiMain = GameObject.Find ("UI").GetComponent<Canvas> ();
			uiMain.enabled = false;
			Canvas winScreen = GameObject.Find ("WinScreen").GetComponent<Canvas> ();
			winScreen.enabled = true;
			isCompleted = true;
		}
	}

}
