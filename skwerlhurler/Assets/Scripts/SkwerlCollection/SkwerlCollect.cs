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
	public float gravity;
	public int id { set; get;}
	public GameObject squirrelToFollow { set; get;}
	private GameObject followSquirrel;
	public Collider fsc;
	public Collider stfc;

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
		fsc = followSquirrel.GetComponent<Collider> ();
		Physics.IgnoreLayerCollision (8,8); //Skwerls ignore each other
		Physics.IgnoreLayerCollision (8,9); //Skwerls ignore main skwerl
	}

	void Update(){
		if (squirrelToFollow != null) {
			stfc = squirrelToFollow.GetComponent<Collider> ();
			leadPos = squirrelToFollow.transform.position;
			followPos = followSquirrel.transform.position;
			diffX = Math.Abs (leadPos.x - followPos.x);
			diffZ = Math.Abs (leadPos.z - followPos.z);
			diffY = Math.Abs (leadPos.y - followPos.y);

			if(isCollected){
				if ((diffX >= maxDist || diffZ >= maxDist || diffY >= maxDist))
					lostSkwerl ();

				if ((diffX >= minDist || diffZ >= minDist)
					&& diffX < maxDist && diffZ < maxDist && diffY < maxDist) 
				{
					path = leadPos - followPos;
					turnSkwerl(path);
					path *= speed;
					if (!fscontrol.isGrounded) {
						//path.y = 1;  //Doing this and making gravity negative
						//path.y *= speed;  //Keeps them grounded
						path.y *= gravity;
					}
				}

				if (diffX < minDist && diffZ < minDist) {
					if (!fscontrol.isGrounded) {
						path = new Vector3 (0, 1, 0);
						//path.y = leadPos.y - followPos.y;
						turnSkwerl (path);
						path.y *= speed;
						path.y *= -gravity;
					} else if (fscontrol.isGrounded) {
						path *= 0;
					}
				}

				fscontrol.Move (path * Time.smoothDeltaTime);
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
