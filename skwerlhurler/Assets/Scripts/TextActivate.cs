using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextActivate : MonoBehaviour {

	public GameObject textBox;
	public Text actualText;
	public TextAsset file;
	private string message;

	void Start(){
		//actualText = textBox.GetComponent<Text> ();
		textBox.SetActive (false);
		message = "Nothing was loaded";
	}

	void OnTriggerEnter(Collider character){
		if (character.name.Equals("Squirrel")) {
			message = file.text;
			if (textBox.activeSelf == false) {
				textBox.SetActive (true);
				actualText.text = message; 
			}
		}
	}

	void OnTriggerExit(){
		if(textBox.activeSelf == true) textBox.SetActive (false);
	}
}
