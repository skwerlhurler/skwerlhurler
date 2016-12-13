using UnityEngine;
using System.Collections;

public class nutCollect : MonoBehaviour {

	public NutSystem skwerl;
	public AudioSource collectSound;

	void OnTriggerEnter(Collider collector){
		if (collector.name.Equals ("Squirrel")) {
			skwerl.nutsObtained += 1;
			collectSound.Play ();
			Destroy(this.gameObject);
		}
	}
}
