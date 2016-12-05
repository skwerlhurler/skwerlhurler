using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class CatsMovement : MonoBehaviour {

	// Use this for initialization
	private GameObject cat;
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

	void Start () {
		cat = this.gameObject;
		catController = cat.GetComponent<CharacterController> ();
		isAttacking = false;
		goBack = false;
		saveCatPos = cat.transform.position;
		//saveSkwerlPos = skwerl.transform.position;
		recordTime = 0;
	}

	void OnTriggerExit(Collider character){
		string temp = character.name;
		if (temp.Equals("Squirrel") || temp.Equals("PatrolAreaTom")) {
			victim = null;
			isAttacking = false;
			if (temp.Equals("PatrolAreaTom")){
				goBack = true;
			}
		}
	}

	void OnCollisionEnter(Collision kill){
		Debug.Log (kill);
		if (kill.collider.name.Equals ("SkwerlKillBox")) {
			Debug.Log ("Killed Squirrel");
			isAttacking = false;
			goBack = true;
			skwerl.SetActive(false);
			Canvas uiMain = GameObject.Find ("UI").GetComponent<Canvas>();
			uiMain.enabled = false;
			Canvas killScreen = GameObject.Find ("DeathScreen").GetComponent<Canvas> ();
			killScreen.enabled = true;
		}
	}

	void Update () {
		if (isAttacking) {
			goBack = false;
			attack ();
		}
		if (goBack) {
			recordTime += (Time.smoothDeltaTime * 60)/100;
			catReturn ();
			if (recordTime > 2){
				goBack = false;
				cat.transform.position = saveCatPos;
				SpriteRenderer skwerlSprite = catController.gameObject.GetComponentInChildren<SpriteRenderer>();
				skwerlSprite.sprite = sprite4;
				recordTime = 0;
			}
		}
	}

	void attack(){
		Vector3 catPos = cat.transform.position;
		Vector3 victimPos = victim.transform.position;
		Vector3 attack = victimPos - catPos;
		turnSkwerl (attack);
		attack.y = 0;
		attack.Normalize();
		attack *= speed;
		//Debug.Log (attack);
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
		SpriteRenderer skwerlSprite = catController.gameObject.GetComponentInChildren<SpriteRenderer>();
		if (dir.x < 0 && dir.z > 0) skwerlSprite.sprite = sprite1;
		if (dir.x < 0 && dir.z < 0) skwerlSprite.sprite = sprite3;
		if (dir.x > 0 && dir.z > 0) skwerlSprite.sprite = sprite2;
		if (dir.x > 0 && dir.z < 0) skwerlSprite.sprite = sprite4;
	}
}
