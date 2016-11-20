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
	private Vector2 tempJump = Vector2.zero;
	public Vector3 moveDir = Vector3.zero; 
	private bool initJump = true;
	GameObject go;
	SwipeFree swipefree;
	TapJump tapJump;
	private float propX; 
	private float propY;

	void Start () {
		go = GameObject.Find("Sphere"); 
		swipefree = go.GetComponent<SwipeFree>();
		tapJump = GetComponent<TapJump> ();
		direction = PlayerDirection.southeast; // initialize direction to southeast

	}

	public float decayConstant;
	public float decayThreshold;
	public float speed;
	public float maxVector;
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


				// If over max vector, set to max vector (sets maximum input speed)
				if (Mathf.Abs (moveDir.x) > maxVector) {
					if (moveDir.x > 0) {
						moveDir.x = maxVector;
					} else {
						moveDir.x = -1 * maxVector;
					}
				}
				if (Mathf.Abs (moveDir.z) > maxVector) {
					if (moveDir.z > 0) {
						moveDir.z = maxVector;
					} else {
						moveDir.z = -1 * maxVector;
					}
				}

				moveDir = Quaternion.Euler (0, 45, 0) * moveDir; 

				propX = Mathf.Abs(moveDir.x / moveDir.magnitude); // setting the proportions of the X & Y vector mag for 
				propY = Mathf.Abs(moveDir.z / moveDir.magnitude); // decay later, this saves operations... i guess

				moveDir *= speed;

				// figure out player direction based on last swipe input
				if (Mathf.Abs (moveDir.x) >= Mathf.Abs (moveDir.z)) {
					if (moveDir.x >= 0) {
						direction = PlayerDirection.northeast;
						//print ("ne");
					} else {
						direction = PlayerDirection.southwest;
						//print ("sw");
					}
				} else {
					if (moveDir.z >= 0) {
						direction = PlayerDirection.northwest;
					//	print ("nw");
					} else {
						direction = PlayerDirection.southeast;
						//print ("se");
					}
				}		
			}
		}

		// Ground movement decay (annoying and ugly)
		/*if (tapJump.isJumping == false) {
			if (Mathf.Abs (moveDir.x) > 0 || Mathf.Abs (moveDir.z) > 0) {
				if (moveDir.x < -1 * decayThreshold) {
					moveDir.x += decayConstant;
				} else if (moveDir.x > decayThreshold) {
					moveDir.x -= decayConstant;

				} else {
					moveDir.x = 0;
				} 
				if (moveDir.z < -1 * decayThreshold) {
					moveDir.z += decayConstant;
				} else if (moveDir.z > decayThreshold) {
					moveDir.z -= decayConstant;

				} else {
					moveDir.z = 0;
				} 

				//print (moveDir.x + " " + moveDir.z);
			}
		}*/

		if (tapJump.isJumping == false) {
			if (Mathf.Abs (moveDir.x) > 0 || Mathf.Abs (moveDir.z) > 0) {
				if (moveDir.x < -1 * decayThreshold) {
					moveDir.x += (propX * decayConstant);
				} else if (moveDir.x > decayThreshold) {
					moveDir.x -= (propX * decayConstant);
				}
				if (moveDir.z < -1 * decayThreshold) {
					moveDir.z += (propY * decayConstant);
				} else if (moveDir.z > decayThreshold) {
					moveDir.z -= (propY * decayConstant);
				}
				if (Mathf.Abs (moveDir.x) < decayThreshold && Mathf.Abs (moveDir.z) < decayThreshold) {
					moveDir.x = 0; moveDir.z = 0; 
				}
					
			}
		}
		// if player is jumping
		else {

			if (initJump == true ) {

				tempJump = Vector2.ClampMagnitude(tapJump.swipeVector, jumpDistance);

					
				moveDir = new Vector3 (-1* tempJump.x, jumpHeight ,-1* tempJump.y); // change when facing is implemented

				moveDir = Quaternion.Euler (0, 45, 0) * moveDir; 
				// set jump vector
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
