using UnityEngine;
using System.Collections;

public class LifeRaft : MonoBehaviour {
	
	public bool onRaft;
	public float slopeLimit;
	public GameObject skwerl;
	public bool fromRaft { set; get; }
	JoyStickMovement skwerlJsm;
	public AudioSource jumpSound;
	float reset;
	// Use this for initialization
	void Start () {
		onRaft = false;
		fromRaft = false;
		skwerlJsm = skwerl.GetComponent<JoyStickMovement> ();
		reset = skwerlJsm.gravity;
	}
		
	void OnTriggerEnter(Collider passenger){
		if (passenger.name.Equals ("Squirrel") && !onRaft) {
			//The code below catches the skwerl and places him in the raft
			passenger.gameObject.transform.position = this.transform.position;
			skwerl.GetComponent<Rigidbody> ().velocity.Equals(Vector3.zero);
			skwerlJsm.isJumping = false;
			skwerlJsm.gravity = 0;
			skwerlJsm.x_const = -30;
			onRaft = true;
			//Let's not fall through water
			Physics.IgnoreLayerCollision (9, 4, false);
			//Now edit controller values
			skwerlJsm.GetComponent<CharacterController>().slopeLimit = slopeLimit;
		} 
	}
	
	// Update is called once per frame
	void Update () {
		if (onRaft && !skwerlJsm.isJumping) {
			//Update position
			this.transform.position = skwerl.transform.position;
			skwerlJsm.gravity = reset;
		} 
	}
}
