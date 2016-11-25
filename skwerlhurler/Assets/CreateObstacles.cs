using UnityEngine;
using System.Collections;

public class CreateObstacles : MonoBehaviour {
	public Transform fountain;

	// Use this for initialization
	void Start () {
		// Create a fountain object at (100,25,100) and with rotation (270,0,0)
		Instantiate(fountain, new Vector3(100,25,100), Quaternion.Euler(270,0,0));

		// Scale the fountain object by 50 on all x, y, z
		fountain.localScale = new Vector3(50, 50, 50);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
