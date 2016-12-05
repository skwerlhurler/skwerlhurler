using UnityEngine;
using System.Collections;

public class SkwerlDrown : MonoBehaviour {

	public GameObject skwerl;
	GameObject ob;
	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter(Collider kill){
		ob = kill.gameObject;
		//Debug.Log (ob);
		if (ob.name.Equals ("Squirrel")) {
			Debug.Log ("Drowned");
			skwerl.SetActive (false);
			Canvas uiMain = GameObject.Find ("UI").GetComponent<Canvas> ();
			uiMain.enabled = false;
			Canvas killScreen = GameObject.Find ("DeathScreen").GetComponent<Canvas> ();
			killScreen.enabled = true;
		}
	}
}
