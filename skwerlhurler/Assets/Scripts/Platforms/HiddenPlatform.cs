using UnityEngine;
using System.Collections;

public class HiddenPlatform : MonoBehaviour {

	GameObject platform;
	public NutTree tree;
	// Use this for initialization
	void Start () {
		platform = this.gameObject.GetComponentInChildren<MovingPlatform>().gameObject;
		platform.SetActive (false);
		tree = GameObject.Find ("MainTree").GetComponentInChildren<NutTree>();
	}

	// Update is called once per frame
	void Update () {
		int gained = tree.nutsStoredInTree;
		int needed = tree.nutsToWin;
		if (gained == needed - 1) 
			platform.SetActive (true);
	}
}
