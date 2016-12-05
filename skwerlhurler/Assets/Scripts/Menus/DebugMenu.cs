using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class DebugMenu : MonoBehaviour {


	public GameObject squirrel;
	public Text feedback;
	private JoyStickMovement jsm;
	private InputField x;
	private int numOfSkwerls;
	SkwerlCollect[] all;
	CatsMovement[] allCats;
	float jf;
	float fc;
	float jc;
	float xc;
	float ss;
	float slope;
	int[] minDists;
	float[] speeds;
	float[] catSpeeds;

	// Use this for initialization
	void Start () {
		jsm = squirrel.GetComponent<JoyStickMovement> ();
		jc = jsm.jump_c;
		fc = jsm.fall_c;
		jf = jsm.jf_const;
		xc = jsm.x_const;
		ss = jsm.speed;
		slope = jsm.slope;
		all = FindObjectsOfType(typeof(SkwerlCollect)) as SkwerlCollect[];
		allCats = FindObjectsOfType(typeof(CatsMovement)) as CatsMovement[];
		minDists = new int[all.Length];
		speeds = new float[all.Length];
		catSpeeds = new float[allCats.Length];
		int i = 0;
		foreach (SkwerlCollect sc in all) {
			minDists[i] = sc.minDist;
			speeds [i] = sc.speed;
			i++;
		}
		i = 0;
		foreach (CatsMovement cm in allCats) {
			catSpeeds [i] = cm.speed;
			i++;
		}
	}


	public void setInputField(InputField inf){ x = inf; }

	public void setSquirrelValues(String wat){
		if (wat != null) {
			string valueString = x.text;
			float value = float.Parse (valueString);
			int value2 = Int32.Parse (valueString);
			if (wat == "JCValue") { 
				jsm.jump_c = value;
				feedback.text = "jump_c Changed";
			}
			if (wat == "FCValue") {
				jsm.fall_c = value;
				feedback.text = "fall_c Changed";
			}
			if (wat == "JFValue") {
				jsm.jf_const = value;
				feedback.text = "jf_const Changed";
			}
			if (wat == "XCValue") {
				jsm.x_const = value;
				feedback.text = "X_Const Changed";
			}
			if (wat == "SSValue") {
				jsm.speed = value;
				feedback.text = "SquirrelSpeedChanged";
			}
			if (wat == "SlopeValue") {
				jsm.slope = value;
				feedback.text = "SlopeChanged";
			}
			if (wat == "FollowVal") {
				foreach (SkwerlCollect sc in all)
					sc.minDist = value2;
				feedback.text = "FollowDistChanged";
			}
			if (wat == "FSVal") {
				foreach (SkwerlCollect sc in all)
					sc.speed = value;
				feedback.text = "FollowSpeedChanged";
			}
			if (wat == "CSValue") {
				foreach (CatsMovement cm in allCats)
					cm.speed = value;
				feedback.text = "CatSpeedChanged";
			}
		}
	}
	public void reset(){
		jsm.jump_c = jc;
		jsm.fall_c = fc;
		jsm.jf_const = jf;
		jsm.x_const = xc;
		jsm.speed = ss;
		jsm.slope = slope;
		int i = 0;
		foreach (SkwerlCollect sc in all) {
			sc.minDist = minDists[i];
			sc.speed = speeds [i];
			i++;
		}
		i = 0;
		foreach (CatsMovement cm in allCats) {
			cm.speed = catSpeeds [i];
			i++;
		}
		feedback.text = "ValuesReset";
	}
}
