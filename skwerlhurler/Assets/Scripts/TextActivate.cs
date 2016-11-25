using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextActivate : MonoBehaviour {

	GameObject textBox;
	public Text actualText;
	public TextAsset file;
	public Collider npc;
	private string message;

	void Start(){
		textBox = GameObject.Find ("TextBox");
		//actualText = textBox.GetComponent<Text> ();
		textBox.SetActive (false);
		npc = GetComponent<Collider> ();
		message = "Nothing was loaded";
	}

	void OnTriggerEnter(Collider character){
		if (npc.name.Equals ("Broseph")) {
			message = file.text;
		} else {
			Debug.Log ("Text Not activated");
		}
		if (textBox.activeSelf == false) {
			textBox.SetActive (true);
			//actualText.
			actualText.text = message; 
		}
	}

	void OnTriggerExit(){
		if(textBox.activeSelf == true) textBox.SetActive (false);
	}
}
