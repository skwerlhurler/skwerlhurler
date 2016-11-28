using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelReturn : MonoBehaviour {

	public Button failedButton;
	public Button oneStar;
	public Button twoStars;
	public Button threeStars;

	string sceneName;
	int sceneNumber;

	// Use this for initialization
	void Start () {
		Debug.Log("Loaded level: " + SceneManager.GetActiveScene().name);

		// Make button objects
		failedButton = failedButton.GetComponent<Button> ();
		oneStar = oneStar.GetComponent<Button> ();
		twoStars = twoStars.GetComponent<Button> ();
		threeStars = threeStars.GetComponent<Button> ();

		// Name Buttons
		failedButton.GetComponentsInChildren<Text>()[0].text = "Failed";
		oneStar.GetComponentsInChildren<Text>()[0].text = "One Star";
		twoStars.GetComponentsInChildren<Text>()[0].text = "Two Star";
		threeStars.GetComponentsInChildren<Text>()[0].text = "Three Star";

		// Set Call back
		failedButton.onClick.AddListener (ReturnToLevelSelection);
		oneStar.onClick.AddListener (() => GiveStars(1));
		twoStars.onClick.AddListener (() => GiveStars(2));
		threeStars.onClick.AddListener (() => GiveStars(3));

		// Get current scene number
		sceneName = SceneManager.GetActiveScene().name;
		sceneNumber = int.Parse (sceneName.Substring (sceneName.Length - 1));
	}

	void GiveStars (int numStars) {
		Debug.Log (numStars + " stars given");

		// Set current scene's stars: 
		// 	0 for none and locked, 1 for none and locked
		//  11 for one star, 12 for two stars, 13 for three stars
		numStars += 10;
		if (PlayerPrefs.GetInt ("Level" + sceneNumber) < numStars) {
			PlayerPrefs.SetInt ("Level" + sceneNumber, numStars);
		}

		// Unlock the next stage if locked
		if (PlayerPrefs.GetInt ("Level" + (sceneNumber+1)) == 0) {
			PlayerPrefs.SetInt ("Level" + (sceneNumber+1), 1);
		}

		ReturnToLevelSelection ();
	}
		
	void ReturnToLevelSelection () {SceneManager.LoadScene ("LevelSelection");}
}
