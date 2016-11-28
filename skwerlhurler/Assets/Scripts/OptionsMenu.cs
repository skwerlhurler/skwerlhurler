using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

	public bool musicOn;
	public AudioSource music;

	void Start(){
		music.Play ();
	}

	public void musicToggle(){
		if (!musicOn) {
			music.Play ();
			musicOn = true;
		}
		else if (musicOn) {
			music.Stop ();
			musicOn = false;
		}
	}
}
