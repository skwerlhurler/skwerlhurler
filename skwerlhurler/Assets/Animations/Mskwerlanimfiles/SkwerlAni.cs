using UnityEngine;
using System.Collections;

public class SkwerlAni : MonoBehaviour {
	Animator anim;
	JoyStick myjs;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		myjs = GameObject.Find("JoyStickContainer").GetComponent<JoyStick>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement_vector = myjs.InputDirection;
		
		if (movement_vector != Vector3.zero){
			anim.SetBool("iswalking", true);
			anim.SetFloat("input_x", movement_vector.x);
			anim.SetFloat("input_y", movement_vector.z);
		} else {
			anim.SetBool("iswalking", false);
		}
	}
}
