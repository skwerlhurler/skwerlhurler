using UnityEngine;
using System.Collections;
using System.Collections.Generic; // for Queue

public class MoveScript : MonoBehaviour {

	GameObject cube;
	Vector3 velocity;

	private PositionQueue pastPositions; // user-defined
	void Awake(){
		pastPositions = new PositionQueue(10);
	}

	void Start() {
		cube = GameObject.Find("Cube");
		Rigidbody cubeRigidBody = cube.AddComponent<Rigidbody>(); // Add the rigidbody.
		cubeRigidBody.mass = 5; // Set the GO's mass to 5 via the Rigidbody.
	}

	// Update is called once per frame
	void Update () {
		/*if (Input.touchCount > 0) {
			transform.Translate (Input.touches [0].deltaPosition.x * .04f,
				Input.touches [0].deltaPosition.y * .04f,
				0);
		}*/

		for (int i = 0; i < Input.touchCount; i++) {
			Debug.Log ("Hi" + i);
			//transform.Translate (Input.touches [i].deltaPosition.x * .04f, Input.touches [i].deltaPosition.y * .04f, 0);
			/*
			switch (Input.GetTouch (i).phase) {
				case TouchPhase.Canceled:
					Debug.Log ("cancelled");
					break;
				case TouchPhase.Ended:
					Debug.Log("ended");
					break;
				case TouchPhase.Moved:
					Debug.Log("moved");
					break;
				case TouchPhase.Stationary:
					Debug.Log("stationary");
					break;

			}*/

			if (Input.GetTouch (i).phase == TouchPhase.Began ||
			   Input.GetTouch (i).phase == TouchPhase.Moved) {
				Debug.Log ("began");

				Ray screenRay = Camera.main.ScreenPointToRay (Input.GetTouch (i).position);

				RaycastHit hit;
				if (Physics.Raycast (screenRay, out hit) && hit.collider.name == "Cube") {
					Debug.Log ("hit" + i + "_" + hit.collider.name);
					transform.Translate (Input.touches [i].deltaPosition.x * .2f, Input.touches [i].deltaPosition.y * .2f, 0);

				}

				//transform.Translate (Input.touches [0].deltaPosition.x * .04f,
				//	Input.touches [0].deltaPosition.y * .04f,
				//	0);
			} else if (Input.GetTouch (i).phase == TouchPhase.Ended) {

			}
		}

		//CAMERA SHOULD FOLLOW THE CUBE
		//TERRAIN SHOULD HAVE ITS BOUNDARY

		/*
		Vector3 touchPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 newPosition = new Vector3 (touchPosition.x, touchPosition.y, transform.position.z);

		if(Input.GetMouseButtonDown(0)){ // 0 means left click (vs right 1 or middle 2)
			transform.GetComponent<Rigidbody2D>().isKinematic=true;  
			pastPositions.Clear();
		}
		// User is dragging the object around
		if(Input.GetMouseButton(0)) {   
			pastPositions.Enqueue(newPosition);
			transform.position = newPosition;                    
			transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime * 25f);
			transform.GetComponent<Rigidbody2D>().velocity = (newPosition - transform.position) * 10;
			Debug.Log ("GMB");
		}
		if (Input.GetMouseButtonUp (0)){
			transform.GetComponent<Rigidbody2D>().isKinematic = false;   
			velocity = (newPosition - pastPositions.Peek()) / pastPositions.Count;
			transform.GetComponent<Rigidbody2D>().velocity = velocity * 20; // Also tried with transform.GetComponent<Rigidbody2D>().velocity = velocity; and transform.position += velocity; // The later works but doesn't allow for physics later on
		}*/



		/* 
		 // THIS WORKS! - moving the cube
		if (Input.touchCount == 1) {
			transform.Translate (Input.touches[0].deltaPosition.x * .04f,
				Input.touches[0].deltaPosition.y * .04f,
				0);
		}
			*/

			/*
		Touch touch = Input.GetTouch(0);

		float x = -7.5f + 15 + touch.position.x / Screen.width;
		float y = -4.5f + 9 + touch.position.y / Screen.height;

		//Debug.Log ("touch x" + touch.position.x);

		transform.position = new Vector3 (x, y, 0);
		*/
	}
}

class PositionQueue{
	private Queue<Vector3> _queue;
	private int _maxSize;
	public int Count {
		get {
			return _queue.Count;
		}
	}
	public PositionQueue(int maxSize){
		_maxSize = maxSize;
		_queue = new Queue<Vector3>();
	}
	public void Enqueue(Vector3 v){
		if(_queue.Count>=_maxSize){
			_queue.Dequeue();
		}
		_queue.Enqueue(v);
	}
	public Vector3 Peek(){
		return _queue.Peek();
	}
	public void Clear(){
		_queue.Clear();
	}
}