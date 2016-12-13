using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HoldCharacter : MonoBehaviour {

	public Collider x;

	void Start(){
		Physics.IgnoreLayerCollision (11,11);
	}

	void OnTriggerEnter(Collider col){
		if (col.name == "Squirrel") {
			x = col;
			col.transform.parent = gameObject.transform;
		}
	}

	void OnTriggerExit(Collider col){
		if(col.name == "Squirrel")col.transform.parent = null;
	}
}
