using UnityEngine;
using System.Collections;

// player direction enum
public enum PlayerDirection{
	northeast = 0,
	southeast = 1,
	southwest = 2, 
	northwest = 3
}

public class PlayerMovement : MonoBehaviour {

	public PlayerDirection direction {set;get;}
	private Vector2 temp = Vector2.zero;
	private Vector3 moveDir = Vector3.zero; 
	private bool initJump = true;
	GameObject go;
	SwipeFree swipefree;
	TapJump tapJump;

	void Start () {
		go = GameObject.Find("Sphere"); 
		swipefree = go.GetComponent<SwipeFree>();
		tapJump = GetComponent<TapJump> ();
		direction = PlayerDirection.southeast; // initialize direction to southeast
	}

	public float decayConstant;
	public float decayThreshold;
	public float speed;
	public float jumpHeight;
	public float jumpDistance;
	public float gravity;


	// Update is called once per frame
	void Update () {
		CharacterController controller = gameObject.GetComponent<CharacterController> (); 

		// Check for new user input vector
		if (tapJump.isJumping == false && moveDir.x == 0 && moveDir.z == 0) {

			// Run if swipe input changes
			if (temp != swipefree.swipeVector) {
				
				temp = swipefree.swipeVector;
				moveDir = new Vector3 (temp.x, 0, temp.y);
				moveDir = Quaternion.Euler (0, 45, 0) * moveDir; 
				moveDir *= speed;

				// figure out player direction based on last swipe input
				if (Mathf.Abs (moveDir.x) >= Mathf.Abs (moveDir.z)) {
					if (moveDir.x >= 0) {
						direction = PlayerDirection.northeast;
						print ("ne");
					} else {
						direction = PlayerDirection.southwest;
						print ("sw");
					}
				} else {
					if (moveDir.z >= 0) {
						direction = PlayerDirection.northwest;
						print ("nw");
					} else {
						direction = PlayerDirection.southeast;
						print ("se");
					}
				}		
			}
		}

		// Ground movement decay
		if (tapJump.isJumping == false) {
			if (Mathf.Abs (moveDir.x) > 0 || Mathf.Abs (moveDir.z) > 0) {
				if (Mathf.Abs (moveDir.x / decayConstant) < decayThreshold) {
					moveDir.x = 0; 
				} else {
					moveDir.x = moveDir.x / decayConstant;
				} 
				if (Mathf.Abs (moveDir.z / decayConstant) < decayThreshold) {
					moveDir.z = 0; 
				} else {
					moveDir.z = moveDir.z / decayConstant;
				} 
			}
		}

		// if player is jumping
		else {

			if(initJump == true){

					// calculate jump multipliers based on player direction
					int xdir, zdir;
					if (direction == PlayerDirection.northeast) {
						xdir = 1;
						zdir = 0;

					}
					else if (direction == PlayerDirection.northwest) {
						xdir = 0;
						zdir = 1;

					}
					else if (direction == PlayerDirection.southeast) {
						xdir = 0;
						zdir = -1;

					}
					else{
						xdir = -1;
						zdir = 0;

					}

					// set jump vector
					moveDir = new Vector3 (jumpDistance*xdir, jumpHeight, jumpDistance*zdir); // change when facing is implemented
					initJump = false;
				
			}
		}

		moveDir.y -= gravity * Time.deltaTime; 
		controller.Move (moveDir*Time.deltaTime);

		// landing from a jump
		if (controller.isGrounded && tapJump.isJumping == true) {

			moveDir = new Vector3(0,0,0);
			tapJump.isJumping = false;
			initJump = true;
		}
		
	}
}
