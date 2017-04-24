using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SkwerlCollect : MonoBehaviour {

	//It should be noted that this script should be in child of desired gameObject

	public bool isCollected;
	public bool isThrown;
	public bool needsToReset;
	public bool isAttacked { set; get; }
	public float speed;
	public int minDist;
	public int maxDist;
	public float gravity;
	public int id { set; get;}
	public GameObject squirrelToFollow { set; get;}
	public GameObject followSquirrel { set; get; }
	public Collider fsc;
	public Collider stfc;
	private AudioSource popsound;
	ParticleSystem poof;

	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite sprite4;
	CharacterController fscontrol;
	public SkwerlThrow thrower;

	JoyStick myjs;
	Vector3 joy;

	Vector3 leadPos;
	Vector3 followPos;
	Vector3 path;
	public Vector3 startPos { set; get; }
	float diffX;
	float diffZ;
	float diffY;
	public int waitTime { set; get; }

	float slope;
	float jump_c;
	private float xCount;
	float xFlowRate;

	// Use this for initialization
	void Start () {
		thrower = GameObject.Find("Squirrel").GetComponent<SkwerlThrow> ();
		isCollected = false;
		isThrown = false;
		isAttacked = false;
		needsToReset = false;
		followSquirrel = this.gameObject.transform.parent.gameObject; 
		startPos = followSquirrel.transform.position;
		poof = followSquirrel.GetComponent<ParticleSystem> ();
		fscontrol = followSquirrel.GetComponent<CharacterController> ();
		fsc = followSquirrel.GetComponent<Collider> ();
		myjs = GameObject.Find ("JoyStickContainer").GetComponent<JoyStick>();
		Physics.IgnoreLayerCollision (8,8); //Skwerls ignore each other
		Physics.IgnoreLayerCollision (8,9); //Skwerls ignore main skwerl
		waitTime = 0;
		popsound = GameObject.Find ("PopSound").GetComponent<AudioSource> (); 

		slope = thrower.throwSlope;
		jump_c = thrower.throwCval;
		xFlowRate = thrower.throwFlowRate;
		xCount = 0;
	}

	void Update(){
		if (squirrelToFollow != null) {
			stfc = squirrelToFollow.GetComponent<Collider> ();
			leadPos = squirrelToFollow.transform.position;
			followPos = followSquirrel.transform.position;
			diffX = Math.Abs (leadPos.x - followPos.x);
			diffZ = Math.Abs (leadPos.z - followPos.z);
			diffY = Math.Abs (leadPos.y - followPos.y);

			if (needsToReset) {
				if (!poof.isPlaying)
					resetSkwerl ();
			}

			if(isCollected && !isThrown){
			//This handles the squirrels' following function
				joy = myjs.InputDirection;
				if ((diffX >= maxDist || diffZ >= maxDist || diffY >= maxDist))
					lostSkwerl ();

				if ((diffX >= minDist || diffZ >= minDist)
					&& diffX < maxDist && diffZ < maxDist && diffY < maxDist) 
				{
					path = leadPos - followPos;
					path *= speed;
					if (!fscontrol.isGrounded) {
						//Must do more experimentation here
						//path.y = 1;  //Doing this and making gravity negative
						//path.y *= speed;  //Keeps them grounded
						//path.y += gravity;
					}
					turnSkwerl (joy);
				}

				if (diffX < minDist && diffZ < minDist) {
					if (!fscontrol.isGrounded) {
						path = new Vector3 (0, 1, 0);
						//path.y = leadPos.y - followPos.y;
						turnSkwerl (path);
						path.y *= speed;
						path.y *= gravity;
					} else if (fscontrol.isGrounded) {
						path *= 0;
					}
				}

				fscontrol.Move (path * Time.smoothDeltaTime);
			}

			if (isThrown && !needsToReset) {
				waitTime += 1;
				float tf = thrower.throwForce;
				Vector3 mom = thrower.copyThrow;

				if (!fscontrol.isGrounded) {
					mom.y += yThrowFunction(xCount, jump_c);
					xCount = xCount + xFlowRate;
					fscontrol.Move (mom * tf);
				}
				if (fscontrol.isGrounded) {
					fscontrol.SimpleMove(new Vector3(0,0,0));
					if (waitTime > 75) {
						//use !isAttacked here to wait for cat to kill squirrel
						disappear ();
					}
				}
			}
		} 
	}

	void lostSkwerl(){
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

	public float yThrowFunction(float x, float jump_c){
		return (-1*slope*x)+(Mathf.Abs(jump_c));
	}

	public void disappear(){
		waitTime = 0;
		xCount = 0;
		followSquirrel.GetComponentInChildren<SpriteRenderer> ().enabled = false;
		popsound.Play ();
		poof.Play ();
		needsToReset = true;
	}

	public void resetSkwerl(){
		followSquirrel.transform.position = startPos;
		followSquirrel.GetComponentInChildren<SpriteRenderer> ().enabled = true;
		isAttacked = false;
		isThrown = false;
		isCollected = false;
		needsToReset = false;
	}

	void turnSkwerl(Vector3 dir){
		SpriteRenderer skwerlSprite = followSquirrel.GetComponentInChildren<SpriteRenderer>();
		if (dir.x < 0 && dir.z > 0) skwerlSprite.sprite = sprite1;
		if (dir.x < 0 && dir.z < 0) skwerlSprite.sprite = sprite3;
		if (dir.x > 0 && dir.z > 0) skwerlSprite.sprite = sprite2;
		if (dir.x > 0 && dir.z < 0) skwerlSprite.sprite = sprite4;
	}
		
}
