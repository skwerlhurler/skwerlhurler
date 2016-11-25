using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SkwerlCollect : MonoBehaviour {

	public bool isCollected;
	public float speed;
	public GameObject squirrelToFollow;
	public SkwerlCollect[] allSkwerls;
	public GameObject followSquirrel;
	public int minDist;
	public int maxDist;
	public int id { set; get;}
	int friendId;
//	public Sprite sprite1;
//	public Sprite sprite2;
//	public Sprite sprite3;
//	public Sprite sprite4;
	CharacterController fscontrol;
	JoyStick myjs;

	Vector3 leadPos;
	Vector3 followPos;
	Vector3 joy;
	Vector3 path;
	float diffX;
	float diffZ;

	// Use this for initialization
	void Start () {
		isCollected = false;
		fscontrol = followSquirrel.GetComponent<CharacterController> ();
		myjs = GameObject.Find ("JoyStickContainer").GetComponent<JoyStick>();
		allSkwerls = gameObject.GetComponents<SkwerlCollect>();
	}

	void Update(){
		if (squirrelToFollow != null) {
			leadPos = squirrelToFollow.transform.position;
			followPos = followSquirrel.transform.position;
			diffX = Math.Abs (leadPos.x - followPos.x);
			diffZ = Math.Abs (leadPos.z - followPos.z);

			if (isCollected && (diffX >= maxDist || diffZ >= maxDist))
				dirFind ();

			if (isCollected && (diffX >= minDist || diffZ >= minDist)
			    && diffX < maxDist && diffZ < maxDist) {
				path = leadPos - followPos;
				//helper = helper/helper.magnitude;
				path *= speed;
				fscontrol.Move (path * Time.deltaTime);
			}
		} 
	}

	void dirFind(){
		joy = myjs.InputDirection;
		Vector3 followPos = squirrelToFollow.transform.position;
		if (joy.x < 0 && joy.z > 0) {
			followPos.x += minDist;
			followPos.z -= minDist;
		}
		if (joy.x < 0 && joy.z < 0){
			followPos.x += minDist;
			followPos.z += minDist;
		}
		if (joy.x > 0 && joy.z > 0){
			followPos.x -= minDist;
			followPos.z -= minDist;
		}
		if (joy.x > 0 && joy.z < 0){
			followPos.x -= minDist;
			followPos.z += minDist;
		}

		followSquirrel.transform.position = followPos;
	}
}
