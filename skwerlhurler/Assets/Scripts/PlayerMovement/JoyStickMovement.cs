using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

/*public enum PlayerDirection{
	northeast = 0,
	southeast = 1,
	southwest = 2, 
	northwest = 3
}*/

public class JoyStickMovement : MonoBehaviour {
	
	//public PlayerDirection direction {set;get;}

	Vector3 moveDir; 
	GameObject temp;
	public JoyStick myjs; 
	public bool isJumping;
	public bool isFalling;
	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite sprite4;
	private CharacterController controller;
	private float x_val;
	private float y_val;
	private float fall_intercept;

	void Start () {
		//myjs = GameObject.Find ("JoyStickContainer").GetComponent<JoyStick>();
		isJumping = false; 
		controller = this.GetComponent<CharacterController> ();
	}
		
	public float speed;
	public float jump_c; // C value of jump to find x intercept					//Working value: 100
	public float fall_c; // C value of fall to find x intercept					//Working value: 100
	public float slope; // Slope of the jump function; should be small			//Working value: .005
	public float jf_const; // Constant to add to jump and fall					//Working value: 120
	public float x_const; // const to change x values							//Working value: 5

	//Update is called once per frame
	void Update () {

		Vector3 moveDir = myjs.InputDirection;

		turnSkwerl (moveDir);

		moveDir = Quaternion.Euler (0, 45, 0) * moveDir; 
		moveDir *= speed;

		// Jumping code
		if (isJumping) {

			// Initialize jump
			if(controller.isGrounded){
				x_val = quadratic (-slope, 0, jump_c);
			}

			y_val = jumpFunction (x_val, jump_c) + jf_const;

			moveDir.y += y_val;
			x_val-= x_const;

			// Jump until parameters are met
			if (x_val <1) {
				isJumping = false;
				x_val = quadratic (-slope, 0, fall_c);
			}

		}

		// Falling
		if(!isJumping){
			if (x_val == 0 || controller.isGrounded) // sets values if fall wasn't after a jump
				x_val = quadratic (-slope, 0, fall_c);
			y_val = jumpFunction (x_val, fall_c) + jf_const;

			if (y_val < 0) // after a while, turn quadratic fall into linear; this is so positive values don't turn negative
				y_val = -2 * x_val;
			moveDir.y -= y_val;
			x_val-= x_const;


		}


		if(!isJumping){
			if (!controller.isGrounded)
				isFalling = true;
			else if (controller.isGrounded)
				isFalling = false;
		}

//		if (isFalling) {
//			moveDir.x /= airResistance;
//			moveDir.z /= airResistance;
//		}

			
		//Debug.Log (moveDir.y);
		controller.Move (moveDir * Time.deltaTime);

	}

	public void jump(){
		if(!isFalling){
			if (!isJumping) {
				isJumping = true;
			} else if (isJumping) {
				isJumping = false;
			}
		}
	}

	public float jumpFunction(float x, float intercept){
		return (-1*slope*x*x)+(intercept);
	}

	public float quadratic(float a, float b, float c){
		float root1 = ((-1 * b) + Mathf.Sqrt ((b * b) - (4 * a * c))) / (2 * a);
		float root2 = (-1 * b - Mathf.Sqrt (b * b - 4 * a * c)) / (2 * a);
		if (root1 > 0)
			return root1;
		return root2;
	}

	void turnSkwerl(Vector3 dir){
		SpriteRenderer skwerlSprite = controller.gameObject.GetComponentInChildren<SpriteRenderer>();
		if (dir.x < 0 && dir.z > 0) skwerlSprite.sprite = sprite1;
		if (dir.x < 0 && dir.z < 0) skwerlSprite.sprite = sprite3;
		if (dir.x > 0 && dir.z > 0) skwerlSprite.sprite = sprite2;
		if (dir.x > 0 && dir.z < 0) skwerlSprite.sprite = sprite4;
	}
}
