using UnityEngine;
using System.Collections;

public class LifeRaft : MonoBehaviour {


	public bool onRaft { set; get; }
	public JoyStickMovement skwerl;
	public CharacterController swim { set; get;}
	public JoyStick joyStick;
	public CharacterController resetCont { set; get; }

	// Use this for initialization
	void Start () {
		onRaft = false;
		swim = this.GetComponentInParent<CharacterController> ();
		resetCont = skwerl.GetComponent<CharacterController> ();
	}
		
	void OnTriggerEnter(Collider passenger){
		if (passenger.name.Equals ("Squirrel")) {
				passenger.transform.parent = gameObject.transform;
				skwerl.enabled = false;
				onRaft = true;
				swim.enabled = true;
				Physics.IgnoreLayerCollision (9, 12, true);
				Physics.IgnoreLayerCollision (9,4, true);
		}
	}

	public void leaveRaft(){
		if (onRaft == true) {
			skwerl.enabled = true;
			swim.enabled = true;
			//lifeRaft.onRaft = false;
			gameObject.transform.parent = null;
			Physics.IgnoreLayerCollision (9, 4, false);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (onRaft && !skwerl.isJumping) {
			skwerl.transform.position = this.gameObject.transform.position;
			Vector3 moveDir = joyStick.InputDirection;
			moveDir = Quaternion.Euler (0, 45, 0) * moveDir; 
			moveDir *= skwerl.speed;
			moveDir.y += skwerl.gravity;
			swim.Move (moveDir * Time.smoothDeltaTime);
		}
	
	}
}
