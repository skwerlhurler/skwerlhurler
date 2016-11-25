using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*public enum PlayerDirection{
	northeast = 0,
	southeast = 1,
	southwest = 2, 
	northwest = 3
}*/

public class JoyStickMovement : MonoBehaviour {
	
	//public PlayerDirection direction {set;get;}

	Vector3 moveDir = Vector3.zero; 
	GameObject player;
	GameObject temp;
	JoyStick myjs; 
	public float y_movenent;
	public bool isJumping;
	public Rigidbody rb;
	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite sprite4;
	public CharacterController controller;
	private float timeCount;
	private float currentTimeCount;

	void Start () {
		//player = GameObject.Find("Squirrel"); 
		myjs = GameObject.Find ("JoyStickContainer").GetComponent<JoyStick>();
		isJumping = false; 
		rb = GetComponent<Rigidbody> ();
		//controller = player.GetComponent<CharacterController> (); 
		//direction = PlayerDirection.southeast; // initialize direction to southeast
	}

	public float speed;
	//public float jumpHeight;
	public float gravity;
	//public float jumpforce;
	//public float startHeight;
	public float intercept; // Intercept of jump function, actually the square root of function's intercept
	public float slope; // Slope of the jump function; should be small

	// Update is called once per frame
	void Update () {
		
		Vector3 moveDir = myjs.InputDirection;

		turnSkwerl (moveDir);

		moveDir = Quaternion.Euler (0, 45, 0) * moveDir; 
		moveDir *= speed;

		if (isJumping) {
			//Debug.Log (jumpFunction(timeCount));
			
			if(controller.isGrounded){
				timeCount = intercept;
			}

			currentTimeCount = jumpFunction (timeCount);

			moveDir.y += currentTimeCount;
			timeCount--;

			if (currentTimeCount <0) { //jumpFunction (timeCount) < -1* x_int
				isJumping = false;
			}

		}
			
		if(!isJumping){
			moveDir.y -= gravity;
		}

			
		//Debug.Log (moveDir.y);
		controller.Move (moveDir * Time.deltaTime);

	}

	public void jump(){
		if (!isJumping) {
			isJumping = true;
		}

	}

	public float jumpFunction(float x){
		return (-1*slope*x*x)+(intercept*intercept);
	}

	void turnSkwerl(Vector3 dir){
		SpriteRenderer skwerlSprite = controller.gameObject.GetComponentInChildren<SpriteRenderer>();
		if (dir.x < 0 && dir.z > 0) skwerlSprite.sprite = sprite1;
		if (dir.x < 0 && dir.z < 0) skwerlSprite.sprite = sprite3;
		if (dir.x > 0 && dir.z > 0) skwerlSprite.sprite = sprite2;
		if (dir.x > 0 && dir.z < 0) skwerlSprite.sprite = sprite4;
	}
}
