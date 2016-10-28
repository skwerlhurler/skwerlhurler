using UnityEngine;
using System.Collections;

public class SwipeFree : MonoBehaviour {

	GameObject go;
	TapJump tapJump;

	void Start () {
		go = GameObject.Find("Sphere"); 
		tapJump = go.GetComponent<TapJump>();
	}

	public float minSwipeDist; 
	public Vector2 swipeVector = Vector2.zero; 
	private Vector2 startPos; 

	// Update is called once per frame
	void Update () 
	{
		
		if (Input.touchCount == 1 && !tapJump.isJumping ) // if we have had a touch and the skwerl is not jumping
		{
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
					
					swipeVector = PlayerSwipe; // change a vector of the swipe (used later to control skwerl movement in game
				}
				break;
			}
		}
	}

}
