using UnityEngine;
using System.Collections;

public class nutCollect : MonoBehaviour {

	public NutSystem skwerl;

	void OnTriggerEnter(Collider collector){
		if (collector.name.Equals ("Squirrel")) {
			skwerl.nutsObtained += 1;
			Destroy(this.gameObject);
		}
	}
}
