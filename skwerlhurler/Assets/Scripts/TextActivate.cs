using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextActivate : MonoBehaviour {

	public GameObject textBox;
	public Text actualText;
	public TextAsset file;
	public Animator textAnim;
	private string message;

	void Start(){
		message = "Nothing was loaded";
	}

	void OnTriggerEnter(Collider character){
		if (character.name.Equals("Squirrel")) {
			message = file.text;
			textAnim.SetBool ("isTalking",true);
			actualText.text = message; 
		}
	}

	void OnTriggerExit(){
		textAnim.SetBool ("isTalking",false);
	}
}
