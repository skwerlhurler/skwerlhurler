using UnityEngine;
using System.Collections;

public class SkwerlThrow : MonoBehaviour {

	public JoyStick joystickDir;
	public float throwForce;
	public int intercept;
	JoyStickMovement jsm;
	public SkwerlCollect[] allSkwerls;
	private bool isThrowing;
	Sprite sprite1;
	Sprite sprite2;
	Sprite sprite3;
	Sprite sprite4;
	Vector3 lastSavedDir;
	public Vector3 copyThrow { set; get;}
	SkwerlAssign info;
	public float skwerlGravity;
	private bool isFalling;
	private AudioSource throwsound;

	public float throwSlope;
	public float throwCval;
	public float throwFlowRate;

	// Use this for initialization
	void Start () {
		jsm = GetComponent<JoyStickMovement> ();
		sprite1 = jsm.sprite1;
		sprite2 = jsm.sprite2;
		sprite3 = jsm.sprite3;
		sprite4 = jsm.sprite4;
		int numOfSkwerls = FindObjectsOfType (typeof(SkwerlCollect)).Length;
		allSkwerls = new SkwerlCollect[numOfSkwerls];
		joystickDir =jsm.myjs;
		isThrowing = false;
		info = this.GetComponent<SkwerlAssign> ();
		throwsound = GameObject.Find ("ThrowSound").GetComponent<AudioSource> (); 
	}

	// Update is called once per frame
	void Update () {
		Vector3 direction = joystickDir.InputDirection;
		//Debug.Log (direction);
		if (direction.magnitude > 0)
			turnSkwerl (direction);
		//put all skwerls here for troubleshoot

		if (isThrowing && info.skwerlQueue.Count != 0) {
			allSkwerls = this.GetComponent<SkwerlAssign> ().all;
			//int collected = info.obtained;
			info.obtained -= 1;
			SkwerlCollect skwerlThrown = info.skwerlQueue.Peek ();
			skwerlThrown.isThrown = true;

			Vector3 stf = skwerlThrown.squirrelToFollow.transform.position; //Replace with animation
			stf.y += 15; //Replace with animation
			skwerlThrown.followSquirrel.transform.position = stf; //Replace with animation

			lastSavedDir.Normalize ();
			lastSavedDir *= throwForce;
			copyThrow = lastSavedDir;


			StartCoroutine(remover(info)); //allows to remove & reorder
			isThrowing = false;
			jsm.enabled = true;
		}
	
	}

	public void throwSkwerl(){
		if (!isThrowing && info.skwerlQueue.Count !=0) {
			throwsound.Play ();
			isThrowing = true;
			jsm.enabled = false;
		} 
	}

	void turnSkwerl(Vector3 dir){
		SpriteRenderer skwerlSprite = GetComponentInChildren<SpriteRenderer>();
		lastSavedDir.y = 0;
		if (dir.x < 0 && dir.z > 0) {
			skwerlSprite.sprite = sprite1;
			lastSavedDir.x = -1;
			lastSavedDir.z = 1;
		}
		if (dir.x < 0 && dir.z < 0) {
			skwerlSprite.sprite = sprite3;
			lastSavedDir.x = -1;
			lastSavedDir.z = -1;
		}
		if (dir.x > 0 && dir.z > 0) {
			skwerlSprite.sprite = sprite2;
			lastSavedDir.x = 1;
			lastSavedDir.z = 1;
		}
		if (dir.x > 0 && dir.z < 0) {
			skwerlSprite.sprite = sprite4;
			lastSavedDir.x = 1;
			lastSavedDir.z = -1;
		}
//		lastSavedDir.x *= 5;
//		lastSavedDir.z *= 5;
		lastSavedDir = Quaternion.Euler (0, 45, 0) * lastSavedDir;
	}
		
	IEnumerator remover(SkwerlAssign sa){
		sa.skwerlQueue.Dequeue(); //This is where we would launch skwerl
		yield return null;
		sa.reorder(); //Reorders immediately
	}
}
