using UnityEngine;
using System.Collections;

public class SkwerlDie : MonoBehaviour {


	CatsMovement cat;
	GameObject skwerl;
	public Animator anim;
	public Animator skwerlanim;
	LifeRaft lifeRaft;
	// Use this for initialization
	void Start () {
		skwerl = this.gameObject;
		if (GameObject.Find("Liferaft"))
			lifeRaft = GameObject.Find ("Liferaft").GetComponent<LifeRaft> ();
	}

	void OnTriggerEnter(Collider kill){
		if (kill.GetComponent<CatsMovement>()) {
			cat = kill.gameObject.GetComponent<CatsMovement> ();
			if (skwerl.name.Equals ("Squirrel")) {
				if (lifeRaft == null)
					die (cat,false);
				if (lifeRaft)
					die (cat,true);
			}
			else if (skwerl.GetComponentInChildren<SkwerlCollect>() && 
				skwerl.GetComponentInChildren<SkwerlCollect>().isThrown){
				SkwerlCollect friend = skwerl.GetComponentInChildren<SkwerlCollect>();
				skwerl.GetComponent<ParticleSystem> ().Play ();
				friend.disappear ();
				cat.victim = null;
				cat.isAttacking = false;
				cat.goBack = true;
			}

		}

	}

	void die(CatsMovement cat, bool raft){
		if (raft == true) {
			Debug.Log ("Lifeboat Exists");
			Physics.IgnoreLayerCollision (9,4,false);
			Physics.IgnoreLayerCollision (9, 12, false);
		} else {
			Debug.Log ("Lifeboat Does not Exist");
		}
		cat.victim = null;
		cat.isAttacking = false;
		cat.goBack = true;
		skwerl.GetComponent<CharacterController>().enabled = false;
		skwerlanim.SetBool ("isdead", true); 
		Canvas uiMain = GameObject.Find ("UI").GetComponent<Canvas> ();
		uiMain.enabled = false;
		Canvas killScreen = GameObject.Find ("DeathScreen").GetComponent<Canvas> ();
		killScreen.enabled = true;
		anim.SetBool ("GameOver",true);
	}

}
