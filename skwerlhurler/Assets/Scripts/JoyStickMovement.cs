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
	public bool isJumping;
	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite sprite4;

	void Start () {
		player = GameObject.Find("Squirrel"); 
		myjs = GameObject.Find ("JoyStickContainer").GetComponent<JoyStick>();
		isJumping = false; 
		//direction = PlayerDirection.southeast; // initialize direction to southeast
	}

	public float speed;
	public float jumpHeight;
	public float gravity;
	public float jumpforce;

	// Update is called once per frame
	void Update () {
		CharacterController controller = player.GetComponent<CharacterController> (); 
		Vector3 moveDir = myjs.InputDirection;

		turnSkwerl (moveDir);

		moveDir = Quaternion.Euler (0, 45, 0) * moveDir; 
		moveDir *= speed;

		if (isJumping) {
			
			moveDir.y += (jumpforce); 

		}
		else{

			moveDir.y -= (gravity);
		}
			
		Debug.Log (moveDir.y);
		controller.Move (moveDir * Time.deltaTime);

	}

	public void jump(){
		isJumping = true;
	}

	void turnSkwerl(Vector3 dir){
		SpriteRenderer skwerlSprite = GameObject.Find("Skwerl").GetComponent<SpriteRenderer>();
		if (dir.x < 0 && dir.z > 0) skwerlSprite.sprite = sprite1;
		if (dir.x < 0 && dir.z < 0) skwerlSprite.sprite = sprite3;
		if (dir.x > 0 && dir.z > 0) skwerlSprite.sprite = sprite2;
		if (dir.x > 0 && dir.z < 0) skwerlSprite.sprite = sprite4;
	}
}
