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
}
