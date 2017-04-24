
﻿using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class CatsMovement : MonoBehaviour {

	// Use this for initialization
	public GameObject cat {set;get;}
	public GameObject skwerl;
	public GameObject victim { set; get; }
	private CharacterController catController;
	public bool isAttacking { set; get;}
	public bool goBack;
	public float speed;
	private string nameOfDead;
	private Vector3 saveCatPos;
	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite sprite4;
	//private Vector3 saveSkwerlPos;
	private double recordTime;
	public float gravity;

	void Start () {
		cat = this.gameObject;
		catController = cat.GetComponent<CharacterController> ();
		isAttacking = false;
		goBack = false;
		saveCatPos = cat.transform.position;
		//saveSkwerlPos = skwerl.transform.position;
		recordTime = 0;
		Physics.IgnoreLayerCollision (10,12,true);
	}

	void OnTriggerExit(Collider character){
		if (character.name.Equals("PatrolAreaTom")) {
			victim = null;
			isAttacking = false;
			if (character.name.Equals("PatrolAreaTom")){
				goBack = true;
			}
		}
	}

	void Update () {
		if (isAttacking) {
			goBack = false;
			if (victim != null) attack ();
			if (victim == null)
				goBack = true;
		}
		if (goBack) {
			recordTime += (Time.smoothDeltaTime * 60)/100;
			catReturn ();
			if (recordTime > 2){
				goBack = false;
				cat.transform.position = saveCatPos;
				if (catController.gameObject.GetComponentInChildren<SpriteRenderer> ()) {
					SpriteRenderer skwerlSprite = catController.gameObject.GetComponentInChildren<SpriteRenderer> ();
					skwerlSprite.sprite = sprite4;
				}
				recordTime = 0;
			}
		}
	}

	void attack(){
		Vector3 catPos = cat.transform.position;
		Vector3 victimPos = victim.transform.position;
		Vector3 attack = victimPos - catPos;
		turnSkwerl (attack);
		//attack.y = saveCatPos.y;
		attack.Normalize();
		attack *= speed;
		//Debug.Log (attack);
		attack.y += gravity;
		catController.Move (attack);
	}

	void catReturn(){
		Vector3 catPos = cat.transform.position;
		Vector3 attack = saveCatPos - catPos;
		turnSkwerl (attack);
		attack *= speed;
		catController.Move (attack * Time.smoothDeltaTime);
	}

	void turnSkwerl(Vector3 dir){
		if (catController.gameObject.GetComponentInChildren<SpriteRenderer> ()) {
			SpriteRenderer skwerlSprite = catController.gameObject.GetComponentInChildren<SpriteRenderer> ();
			if (dir.x < 0 && dir.z > 0) {
				//cat.transform.Rotate (0, 0, 180); //Would like to rotate in the future
				skwerlSprite.sprite = sprite1;
			}
			if (dir.x < 0 && dir.z < 0) {
				//cat.transform.Rotate (0, 0, 270);
				skwerlSprite.sprite = sprite3;
			}
			if (dir.x > 0 && dir.z > 0) {
				//cat.transform.Rotate (0, 0, 0);
				skwerlSprite.sprite = sprite2;
			}
			if (dir.x > 0 && dir.z < 0) {
				//cat.transform.Rotate (0, 0, 90);
				skwerlSprite.sprite = sprite4;
			}
		}
	}
}