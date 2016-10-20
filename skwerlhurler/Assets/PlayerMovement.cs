using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


	private float speed = 1; 
	private float jumpForce = 8f; 
	private float gravity = 30f; 
	private Vector3 moveDir = Vector3.zero; 
	GameObject go;
	SwipeFree swipefree;

	void Start () {
		go = GameObject.Find("Sphere"); 
		swipefree = go.GetComponent<SwipeFree>();
	}
	
	// Update is called once per frame
	void Update () {
		CharacterController controller = gameObject.GetComponent<CharacterController> (); 

		if (controller.isGrounded) {
			
			Vector2 temp = swipefree.swipeVector;
			moveDir = new Vector3 (temp.x, 0 , temp.y);
			moveDir = Quaternion.Euler(0,45,0)*moveDir; 
			//moveDir = transform.TransformDirection (moveDir); // pray that this works
			moveDir *= speed;
		}

		moveDir.y -= gravity * Time.deltaTime; 
		controller.Move (moveDir*Time.deltaTime);
	}
}
