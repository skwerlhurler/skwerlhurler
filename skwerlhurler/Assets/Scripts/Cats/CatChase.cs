using UnityEngine;
using System.Collections;

public class CatChase : MonoBehaviour {

	public CatsMovement cat;

	void OnTriggerEnter(Collider victim){
		if (victim.GetComponent<SkwerlDie>()) {
			cat.victim = victim.gameObject;
			cat.isAttacking = true;
		}
	}

}
