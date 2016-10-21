using UnityEngine;
using System.Collections;

public class TapJump : MonoBehaviour {

	GameObject go;
	SwipeFree swipefree;
	public bool isJumping;

	// Use this for initialization
	void Start () {
		go = GameObject.Find("Sphere"); 
		swipefree = go.GetComponent<SwipeFree>();
		isJumping = false;
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
			isJumping = true;



			doubleTapTimer = 0.0f;
			tapcount = 0;
		}
		if (doubleTapTimer > doubleTapTimerMax) {
			doubleTapTimer = 0f;
			tapcount = 0; 
		}

	}
}
