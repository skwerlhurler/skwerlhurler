using UnityEngine;
using System.Collections;

public class TapJump : MonoBehaviour {

	GameObject go;
	GameObject arrow; 
	Renderer arrowRenderer;
	SwipeFree swipefree;
	public bool isJumping;
	private Quaternion facing;

	// Use this for initialization
	void Start () {
		go = GameObject.Find("Sphere"); 
		arrow = GameObject.Find ("arrow");
		swipefree = go.GetComponent<SwipeFree>();
		isJumping = false;
		facing = transform.rotation;
		arrowRenderer = arrow.GetComponent<Renderer> ();
		arrowRenderer.enabled = false;
	}
		
	public float minSwipeDist; 
	public Vector2 swipeVector = Vector2.zero; 
	private Vector2 startPos; 
	private Vector2 currentPos;
	private Vector3 delta; 
	private Quaternion rotationVector;
	
	// Update is called once per frame
	void Update () {
		
		if (Input.touchCount == 2 && !isJumping ) // if we have had a touch and the skwerl is not jumping
		{
			arrow.transform.parent = go.transform; // move arrow object to the position of the skwerl
			arrowRenderer.enabled = true; // Show arrow

			Touch touch = Input.touches [0];

			switch (touch.phase) 
			{
			case TouchPhase.Began: 
				startPos = touch.position; 
				break; 
			case TouchPhase.Ended:
				Vector2 PlayerSwipe = new Vector2 ((touch.position.x - startPos.x), (touch.position.y - startPos.y));
				float swipeDistance = (PlayerSwipe.magnitude);

				if (swipeDistance > minSwipeDist) { // if it meets the swipe distance that meets our threshold
					isJumping = true;
					swipeVector = PlayerSwipe; // change a vector of the swipe (used later to control skwerl movement in game
				}
				arrowRenderer.enabled = false; // hide arrow
				break;
			default:
				// Arrow movement
				currentPos = touch.position;
				delta.x = startPos.x - currentPos.x;
				delta.z = startPos.y - currentPos.y;
				delta = Quaternion.Euler (0, 315, 0) * delta;
				rotationVector = Quaternion.LookRotation (delta);
				rotationVector *= facing;
				arrow.transform.rotation = rotationVector;
				arrow.transform.Rotate (Vector3.right, 90);

				break; 
			}
		}

		else{
			arrowRenderer.enabled = false;
		}
	}

}
