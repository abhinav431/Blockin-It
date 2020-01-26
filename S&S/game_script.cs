using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class game_script : MonoBehaviour {

	public AudioSource ass;
	public GameObject swipe_drag,swipe_button,htilt,vtilt;
	public Material[] skybox;
	public GameObject pause_panel,game_over_panel;
	public static int n_paused=0;
	// Use this for initialization
	void Start () {

		if (PlayerPrefs.GetInt ("swipe", 0) == 0) {
			swipe_button.SetActive (true);
			swipe_drag.SetActive (false);
		} else {
			swipe_button.SetActive (false);
			swipe_drag.SetActive (true);
		}

		if (PlayerPrefs.GetInt ("htilt", 0) == 0) {
			htilt.SetActive (false);
		} else {
			htilt.SetActive (true);
		}

		if (PlayerPrefs.GetInt ("vtilt", 1) == 0) {
			vtilt.SetActive (false);
		} else {
			vtilt.SetActive (true);
		}

		if (PlayerPrefs.GetInt ("audio", 1) == 0)
			ass.Pause ();
		else
			ass.Play ();
		RenderSettings.skybox=skybox[PlayerPrefs.GetInt ("skybox",1)];
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void goto_menu()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene ("main");
	}
	public void goto_exit()
	{
		Application.Quit ();
	}
	public void goto_restart()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene ("game");
	}
	public void goto_resume()
	{
		Time.timeScale = 1;
		pause_panel.SetActive (false);
	}
	public void goto_pause()
	{
		n_paused++;
		Time.timeScale = 0;
		pause_panel.SetActive (true);
	}
}
