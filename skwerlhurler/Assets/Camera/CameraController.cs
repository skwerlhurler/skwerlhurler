using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;       //Public variable to store a reference to the player game object
	private Vector3 offset;         //Private variable to store the offset distance between the player and camera
	public Vector3 velocity; 
	public float inSize = 100f; 
	public float outSize = 250f;
	public bool  toggleZoom = false;

	// Use this for initialization
	void Start () 
	{
		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
		offset = transform.position - player.transform.position;
		Camera.main.orthographicSize = outSize;
		Camera.main.transform.rotation = Quaternion.Euler (30 ,45,0);

	}


	public float smoothTimeX;
	public float smoothTimeY; 
	public float smoothTimeZ; 


	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
		if (toggleZoom) {
			if (Camera.main.orthographicSize == inSize) {
				Camera.main.orthographicSize = outSize;

			} else {
				Camera.main.orthographicSize = inSize;

			}
			toggleZoom = false;
		} 
		float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + offset.x, ref velocity.x, smoothTimeX); 
		float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + offset.y, ref velocity.y, smoothTimeY);
		float posZ = Mathf.SmoothDamp(transform.position.z, player.transform.position.z + offset.z, ref velocity.z, smoothTimeZ);

		transform.position = new Vector3 (posX, posY, posZ);
		//transform.position = player.transform.position + offset;
	}
}
