using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SkwerlCollect : MonoBehaviour {

	//It should be noted that this script should be in child of desired gameObject

	public bool isCollected;
	public float speed;
	public int minDist;
	public int maxDist;
	public int id { set; get;}
	public GameObject squirrelToFollow { set; get;}
	private GameObject followSquirrel;

	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite sprite4;
	CharacterController fscontrol;

	Vector3 leadPos;
	Vector3 followPos;
	Vector3 path;
	float diffX;
	float diffZ;
	float diffY;

	// Use this for initialization
	void Start () {
		isCollected = false;
		followSquirrel = this.gameObject.transform.parent.gameObject; 
		fscontrol = followSquirrel.GetComponent<CharacterController> ();
	}

	void Update(){
		if (squirrelToFollow != null) {
			leadPos = squirrelToFollow.transform.position;
			followPos = followSquirrel.transform.position;
			diffX = Math.Abs (leadPos.x - followPos.x);
			diffZ = Math.Abs (leadPos.z - followPos.z);
			diffY = Math.Abs (leadPos.y - followPos.y);

			if (isCollected && (diffX >= maxDist || diffZ >= maxDist || diffY >= maxDist))
				lostSkwerl ();

			if (isCollected && (diffX >= minDist || diffZ >= minDist)
				&& diffX < maxDist && diffZ < maxDist && diffY < maxDist) 
			{
				path = leadPos - followPos;
				//helper = helper/helper.magnitude;
				turnSkwerl(path);
				path *= speed;
				fscontrol.Move (path * Time.deltaTime);
			}

			if (isCollected && diffX < minDist && diffZ < minDist && diffY > 0) {
				path = new Vector3(0,0,0);
				path.y = leadPos.y - followPos.y;
				turnSkwerl (path);
				path *= speed;
				fscontrol.Move (path * Time.deltaTime);
			}
		} 
	}

	void lostSkwerl(){
		JoyStick myjs = GameObject.Find ("JoyStickContainer").GetComponent<JoyStick>();
		Vector3 joy = myjs.InputDirection;
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

		followPos.y = leadPos.y;
		followSquirrel.transform.position = followPos;
	}

	void turnSkwerl(Vector3 dir){
		SpriteRenderer skwerlSprite = followSquirrel.GetComponentInChildren<SpriteRenderer>();
		if (dir.x < 0 && dir.z > 0) skwerlSprite.sprite = sprite1;
		if (dir.x < 0 && dir.z < 0) skwerlSprite.sprite = sprite3;
		if (dir.x > 0 && dir.z > 0) skwerlSprite.sprite = sprite2;
		if (dir.x > 0 && dir.z < 0) skwerlSprite.sprite = sprite4;
	}
}
