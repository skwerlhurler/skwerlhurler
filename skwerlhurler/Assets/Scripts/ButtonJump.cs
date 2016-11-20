using UnityEngine;
using System.Collections;

public class ButtonJump : MonoBehaviour {

	public bool isJumping;
	public bool isFalling;

	// Use this for initialization
	void Start () {
		isJumping = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void jump(){
		isJumping = true;
	}
	
	public void fall(){
		isFalling = true;
	}
}
