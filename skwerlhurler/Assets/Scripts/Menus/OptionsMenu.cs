using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

	public bool musicOn;
	public AudioSource music;
	public Slider volSli;

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

	public void volume(Slider s){
		music.volume = s.value/100;
	}
}
