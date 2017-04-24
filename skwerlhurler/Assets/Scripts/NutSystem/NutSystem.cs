using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NutSystem : MonoBehaviour {

	public int nutsObtained { set; get;}
	public Text display;
	//public nutCollect[] allNuts;
	int numOfNuts;

	// Use this for initialization
	void Start () {
		nutsObtained = 0;
		numOfNuts = FindObjectsOfType (typeof(nutCollect)).Length;
//		for (int i = 0; i < numOfNuts; i++)
//			allNuts [i].id = i;
		if (display != null)
			display.text = "Nuts Gathered: " + nutsObtained;
	}

	void Update(){
		if (display != null)
			display.text = "Nuts Gathered: " + nutsObtained;
	}

}
