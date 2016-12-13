using UnityEngine;
using System.Collections;

public class CatChase : MonoBehaviour {

	public CatsMovement cat;

	void OnTriggerStay(Collider victim){
	//Every collider on every frame in the box
		if (victim.GetComponentInChildren<SkwerlCollect> () != null) {
			if (victim.GetComponentInChildren<SkwerlCollect> ().isThrown
					&& !victim.GetComponentInChildren<ParticleSystem> ().isPlaying) {
				cat.victim = victim.gameObject;
				victim.GetComponentInChildren<SkwerlCollect> ().isAttacked = true;
				cat.isAttacking = true;
			}
		} else if (victim.name.Equals ("Squirrel") && cat.victim == null) {
			//won't chase the main squirrel if he is already chasing someone
			cat.victim = victim.gameObject;
			cat.isAttacking = true;
		}
		
	}

	void OnTriggerExit(Collider victim){
	//Upon exit of the box
		if (victim.GetComponentInChildren<SkwerlCollect>()) {
			cat.victim = null;
			cat.isAttacking = false;
			cat.goBack = true;
		} else if (victim.name.Equals("Squirrel")){
			cat.victim = null;
			cat.isAttacking = false;
			cat.goBack = true;
		}
	}

}
