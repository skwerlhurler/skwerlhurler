using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	GameObject cube;
	Vector3 offset;

	// Use this for initialization
	void Start () {
		cube = GameObject.Find("Cube");
		offset = transform.position - cube.transform.position;
	}
	
	void LateUpdate() {
		Camera.main.transform.position = cube.transform.position + offset;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
