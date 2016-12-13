using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class JoyStickMovement : MonoBehaviour {
	
	//public PlayerDirection direction {set;get;}

	Vector3 moveDir; 
	GameObject temp;
	public JoyStick myjs; 
	public bool isJumping;
	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite sprite4;
	private CharacterController controller;
	private float x_val;
	private float y_val;
	private float fall_intercept;
	public bool isFalling;
	private AudioSource landSound;
	public LifeRaft lifeRaft;

	void Start () {
		//myjs = GameObject.Find ("JoyStickContainer").GetComponent<JoyStick>();
		isJumping = false; 
		controller = this.GetComponent<CharacterController> ();
		isFalling = true;
		landSound = GameObject.Find ("LandSound").GetComponent<AudioSource>(); 
	}
		
	public float speed;

	public float fall_c; // C value of fall to find x intercept					//Working value: 100

	public float jf_const; // Constant to add to jump and fall					//Working value: 120


	public float slope; // Slope of the jump function; should be small			//Working value: .005
	public float x_const; // const to change x values							//Working value: 5
	public float jumpForce;
	public float flowRate;
	public float jump_c; // C value of jump to find x intercept					//Working value: 100
	public float gravity;


	//Update is called once per frame
	void Update () {

		Vector3 moveDir = myjs.InputDirection;

		turnSkwerl (moveDir);

		moveDir = Quaternion.Euler (0, 45, 0) * moveDir; 
		moveDir *= speed;

		if (!isJumping && lifeRaft == null)
			moveDir.y += gravity;
		else if (!isJumping && !lifeRaft.onRaft)
			moveDir.y += gravity;

		if (controller.isGrounded)
			isFalling = false;

		// Jumping code
		if (isJumping) {

			// Initialize jump
			moveDir.y += jumpFunction (x_const, jump_c);

			x_const = x_const + flowRate;
			moveDir.y *= jumpForce;

			// Jump until parameters are met
			if (controller.isGrounded && x_const > 0) {
				if (lifeRaft != null && lifeRaft.onRaft) {
					lifeRaft.onRaft = false;
					Physics.IgnoreLayerCollision (9, 12, false);
				}
				landSound.Play ();
				isJumping = false;
				x_const = -30;
			}

		}

//		if (isFalling) {
//			moveDir.x /= airResistance;
//			moveDir.z /= airResistance;
//		}

		controller.Move (moveDir * Time.deltaTime);

	}

	public void jump(AudioSource jumpSound){
		if (!isJumping) {
			isJumping = true;
			jumpSound.Play ();
		}
	}

	public float jumpFunction(float x, float intercept){
		return (-1*slope*x)+(Mathf.Abs(intercept));
	}
		
	void turnSkwerl(Vector3 dir){
		SpriteRenderer skwerlSprite = controller.gameObject.GetComponentInChildren<SpriteRenderer>();
		if (dir.x < 0 && dir.z > 0) skwerlSprite.sprite = sprite1;
		if (dir.x < 0 && dir.z < 0) skwerlSprite.sprite = sprite3;
		if (dir.x > 0 && dir.z > 0) skwerlSprite.sprite = sprite2;
		if (dir.x > 0 && dir.z < 0) skwerlSprite.sprite = sprite4;
	}
}