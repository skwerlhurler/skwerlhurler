using UnityEngine;
using System.Collections;

public class SkwerlDie : MonoBehaviour {


	CatsMovement cat;
	GameObject skwerl;
	GameObject ob;
	// Use this for initialization
	void Start () {
		skwerl = this.gameObject;
	}

	void OnTriggerEnter(Collider kill){
		ob = kill.gameObject;
		//Debug.Log (ob);
		if (kill.GetComponent<CatsMovement>()) {
			cat = kill.gameObject.GetComponent<CatsMovement> ();
			Debug.Log ("Killed Squirrel");
			cat.isAttacking = false;
			cat.goBack = true;
			skwerl.SetActive(false);
			Canvas uiMain = GameObject.Find ("UI").GetComponent<Canvas>();
			uiMain.enabled = false;
			Canvas killScreen = GameObject.Find ("DeathScreen").GetComponent<Canvas> ();
			killScreen.enabled = true;
		}
//		if (ob.name.Equals ("River")) {
//			Debug.Log ("Drowned");
//			skwerl.SetActive (false);
//			Canvas uiMain = GameObject.Find ("UI").GetComponent<Canvas> ();
//			uiMain.enabled = false;
//			Canvas killScreen = GameObject.Find ("DeathScreen").GetComponent<Canvas> ();
//			killScreen.enabled = true;
//		}
	}

}
