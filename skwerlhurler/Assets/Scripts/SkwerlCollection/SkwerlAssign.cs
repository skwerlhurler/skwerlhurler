using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkwerlAssign : MonoBehaviour {

	public int obtained;
	private int numOfSkwerls;
	SkwerlCollect skwerl;
	GameObject mainSkwerl;
	SkwerlCollect[] all;

	// Use this for initialization
	void Start () {
		numOfSkwerls = FindObjectsOfType (typeof(SkwerlCollect)).Length;
		all = new SkwerlCollect[numOfSkwerls];
		mainSkwerl = this.gameObject;
	}

	void OnTriggerEnter(Collider friend){
		skwerl = friend.gameObject.GetComponentInChildren<SkwerlCollect>();
		if (skwerl != null) {
			if (skwerl.isCollected == false) {
				skwerl.isCollected = true;
				if (obtained == 0)
					skwerl.squirrelToFollow = mainSkwerl;
				else if (obtained > 0)
					skwerl.squirrelToFollow = all[obtained - 1].gameObject;
				all[obtained] = skwerl;
				obtained += 1;
				//skwerl.id = obtained;
			}
		}

	}

}
