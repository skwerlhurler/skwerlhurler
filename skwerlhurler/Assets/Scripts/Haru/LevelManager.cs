using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// https://www.youtube.com/watch?v=xSDfSDTtUMs

public class LevelManager : MonoBehaviour {

	[System.Serializable]
	public class Level {

		public string LevelText;
		public int UnLocked;
		public bool IsInteractable;

		public Level(string txt, int UL, bool II) { LevelText = txt; UnLocked = UL; IsInteractable = II; 		}
	}

	public GameObject levelButton;
	public Transform Spacer;
	public List<Level> LevelList;

	// Use this for initialization
	void Start () {

		LevelList.Clear ();

		// This function should be called every time you want to start over from level 1
		//DeleteAll ();

		FillList ();
	}
	
	void FillList () {
		
		// First level shold be unlocked
		LevelList.Insert(0, new Level(1.ToString(), 1, true));
		// Pushing back level 2~n
		for (int i = 2; i <= 9; i++) { 
			LevelList.Insert(LevelList.Count, new Level(i.ToString(), 0, false));
		}
	
		// Apply the data from Level List to each Buttons
		foreach (var level in LevelList) {

			GameObject newbutton = Instantiate (levelButton) as GameObject;
			LevelButton button = newbutton.GetComponent<LevelButton> ();
			button.LevelText.text = level.LevelText;

			// Applying the Unlock for new level
			if (PlayerPrefs.GetInt ("Level" + button.LevelText.text) > 0) {
				level.UnLocked = 1;
				level.IsInteractable = true;
			}
				
			button.unlocked = level.UnLocked;
			button.GetComponent<Button> ().interactable = level.IsInteractable;
			button.GetComponent<Button>().onClick.AddListener(() => LoadLevels("Level" + button.LevelText.text));

			// Change stars to yellow for each achievement
			if (PlayerPrefs.GetInt ("Level" + button.LevelText.text) > 10) {
				button.Star1.SetActive (true);
			}
			if (PlayerPrefs.GetInt ("Level" + button.LevelText.text) > 11) {
				button.Star2.SetActive (true);
			}
			if (PlayerPrefs.GetInt ("Level" + button.LevelText.text) > 12) {
				button.Star3.SetActive (true);
			}

			newbutton.transform.SetParent (Spacer);
		}
	}

	void DeleteAll() {
		PlayerPrefs.DeleteAll ();
	}

	void LoadLevels(string value) {

		SceneManager.LoadScene (value);
	}
}
