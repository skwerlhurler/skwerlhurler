using UnityEngine;
using System.Collections;

public class SkwerlDrown : MonoBehaviour {

	public GameObject skwerl;
	GameObject ob;
	LifeRaft lifeRaft;
	public Animator anim;
	Animator skwerlanim;

	void Start(){
		skwerlanim = GameObject.Find ("Skwerl").GetComponent<Animator>();
		if (GameObject.Find("Liferaft"))
			lifeRaft = GameObject.Find ("Liferaft").GetComponent<LifeRaft> ();
	}

	void OnTriggerEnter(Collider kill){
		ob = kill.gameObject;
		if (ob.name.Equals ("Squirrel")) {
			if (lifeRaft == null) 
				drown ();
			else if (lifeRaft && !lifeRaft.onRaft) 
				drown ();
		}
	}

	void drown(){
		//skwerl.SetActive (false);
		skwerl.GetComponent<CharacterController> ().enabled = false;
		skwerlanim.SetBool ("isdead", true); 
		Canvas uiMain = GameObject.Find ("UI").GetComponent<Canvas> ();
		uiMain.enabled = false;
		Canvas killScreen = GameObject.Find ("DeathScreen").GetComponent<Canvas> ();
		killScreen.enabled = true;
		anim.SetBool ("GameOver", true);
	}
}
