using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkwerlAssign : MonoBehaviour {

	public int obtained;
	private int numOfSkwerls;
	SkwerlCollect skwerl;
	GameObject mainSkwerl;
	public SkwerlCollect[] all { set; get; }
	public Queue<SkwerlCollect> skwerlQueue = new Queue<SkwerlCollect>();

	// Use this for initialization
	void Start () {
		numOfSkwerls = FindObjectsOfType (typeof(SkwerlCollect)).Length;
		all = new SkwerlCollect[numOfSkwerls];
		mainSkwerl = this.gameObject;
	}

	void OnTriggerEnter(Collider friend){
		skwerl = friend.gameObject.GetComponentInChildren<SkwerlCollect>();
		if (skwerl != null) {
			if (skwerl.isCollected == false) {
				skwerl.isCollected = true;
				if (obtained == 0)
					skwerl.squirrelToFollow = mainSkwerl;
				else if (obtained > 0)
					skwerl.squirrelToFollow = all[obtained - 1].gameObject;
				skwerlQueue.Enqueue (skwerl);
				all[obtained] = skwerl;
				obtained += 1;
				//skwerl.id = obtained;
			}
		}

	}

	public void reorder(){
		numOfSkwerls = FindObjectsOfType (typeof(SkwerlCollect)).Length;
		all = new SkwerlCollect[numOfSkwerls];
		if (skwerlQueue.Count != 0) {
			SkwerlCollect[] temp = skwerlQueue.ToArray ();
			for (int i = 0; i < numOfSkwerls; i++) {
				if (i < temp.Length){
					if (i == 0)
						temp [i].squirrelToFollow = mainSkwerl;
					else
						temp [i].squirrelToFollow = temp [i - 1].gameObject;
					all [i] = temp [i];
				}
				else
					all [i] = null;
			}
		}
	}

}
