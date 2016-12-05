using UnityEngine;
using System.Collections;

public class acorn : MonoBehaviour {


	private GameObject acornOb;
	private Vector3 startPos;
	private float startY;
	public float speed;
	public float amplitude;

	// Use this for initialization
	void Start () {
		acornOb = this.gameObject;
		startPos = acornOb.transform.position;
		startY = acornOb.transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		float yPos = startY + amplitude * Mathf.Sin (speed*Time.time);
		startPos.Set (startPos.x,yPos,startPos.z);
		acornOb.transform.position = startPos;
	}
}
