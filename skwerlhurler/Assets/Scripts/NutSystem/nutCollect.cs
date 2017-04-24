using UnityEngine;
using System.Collections;

public class nutCollect : MonoBehaviour {

	public NutSystem skwerl;
	public AudioSource collectSound;
	public int id { set; get;}
	public static bool inTree;
	public static bool collected;

	void Start(){
		inTree = false;
		collected = false;
	}

	void OnTriggerEnter(Collider collector){
		if (collector.name.Equals ("Squirrel")) {
			skwerl.nutsObtained += 1;
			collectSound.Play ();
			collected = true;
			inTree = true;
			Destroy(this.gameObject);
		}
	}
}
