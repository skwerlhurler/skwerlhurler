using UnityEngine;
using System.Collections;

public class TapDetection : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}

	private int tapcount; 
	private float doubleTapTimer;
	public float doubleTapTimerMax;
	
	// Update is called once per frame
	void Update () {

		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Began) {
			tapcount++;
		}
		if (tapcount > 0) {
			doubleTapTimer += Time.deltaTime; 
		}
		if (tapcount >= 2) {
			// do our thing
			doubleTapTimer = 0.0f;
			tapcount = 0;
		}
		if (doubleTapTimer > doubleTapTimerMax) {
			doubleTapTimer = 0f;
			tapcount = 0; 
		}
		print (tapcount);

		/*if (Input.touchCount == 2) 
		{
			Touch touch = Input.touches [0];

			switch (touch.phase) 
			{

			case TouchPhase.Began: 
				startPos = touch.position; 
				break; 
			case TouchPhase.Ended:
				Vector2 PlayerSwipe = new Vector2 ((touch.position.x - startPos.x), (touch.position.y - startPos.y));
				float swipeDistance = PlayerSwipe.magnitude;

				if (swipeDistance > minSwipeDist) { // if it meets the swipe distance that meets our threshold

					swipeVector = PlayerSwipe; // change a vector of the swipe (used later to control skwerl movement in game
				}
				break;
			}
		}
	}*/
	}
}
