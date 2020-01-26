using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GoogleMobileAds.Api;
*/

public class menu_script : MonoBehaviour {

	public Material[] skyboxx;
	//public BannerView bv;
	public Slider sl;
	public Toggle tg,tg2,tg3,tg4;
	public AudioSource ass;
	public Dropdown dd;
	public GameObject menu_panel,credits_panel,leaderboard_panel,how_panel,options_panel;
	public Text score,time,points;
	// Use this for initialization
	void Start () {


		if (PlayerPrefs.GetInt ("audio", 1) == 1) {
			ass.Play ();
			tg.isOn = true;
		} else {
			ass.Pause ();
			tg.isOn = false;
		}

		if (PlayerPrefs.GetInt ("swipe", 0) == 1) {
			tg2.isOn = true;
		} else {
			tg2.isOn = false;
		}

		if (PlayerPrefs.GetInt ("htilt", 0) == 1) {
			tg3.isOn = true;
		} else {
			tg3.isOn = false;
		}

		if (PlayerPrefs.GetInt ("vtilt", 1) == 1) {
			tg4.isOn = true;
		} else {
			tg4.isOn = false;
		}

		//*******************admob***********************************
		/*
		MobileAds.Initialize("ca-app-pub-7207006250938599~6506936244");
		bv = new BannerView ("ca-app-pub-7207006250938599/6896876530",AdSize.Banner,AdPosition.Top);

		AdRequest ar = new AdRequest.Builder ().Build ();
		bv.LoadAd (ar);
		*/


		score.text = "Score : " + PlayerPrefs.GetInt ("score", 0).ToString ();
		points.text = "Points : " + PlayerPrefs.GetInt ("points", 0).ToString ();
		time.text = "Time : " + PlayerPrefs.GetInt ("time", 0).ToString ();

		RenderSettings.skybox=skyboxx[PlayerPrefs.GetInt ("skybox",1)];

		dd.value = PlayerPrefs.GetInt ("skybox", 0);
		sl.normalizedValue = PlayerPrefs.GetFloat ("ssy", 1f);
		//**********************google play*********************************
		//highscore.text = PlayerPrefs.GetInt ("highscore", 0).ToString();
		/*
		PlayGamesClientConfiguration pg = new PlayGamesClientConfiguration.Builder ().Build ();
		PlayGamesPlatform.InitializeInstance (pg);
		PlayGamesPlatform.Activate ();
		Social.localUser.Authenticate (success => { });


		//print(PlayerPrefs.GetFloat ("ssy", 1f));

		Social.ReportScore (PlayerPrefs.GetInt ("score", 0),GPGSIds.leaderboard_scores //"CgkIwN2ZtMMOEAIQAA"//, success => { });
		*/
	}

	public void htilt(bool x)
	{
		if (x == false) {
			PlayerPrefs.SetInt ("htilt", 0);
		} else {
			PlayerPrefs.SetInt ("htilt", 1);
		}
	}
	public void vtilt(bool x)
	{
		if (x == false) {
			PlayerPrefs.SetInt ("vtilt", 0);
		} else {
			PlayerPrefs.SetInt ("vtilt", 1);
		}
	}
	public void music(bool x)
	{
		if (x == false) {
			PlayerPrefs.SetInt ("audio", 0);
			ass.Pause ();
		} else {
			PlayerPrefs.SetInt ("audio", 1);
			ass.Play ();
		}
	}
	public void swipe(bool x)
	{
		if (x == false) {
			PlayerPrefs.SetInt ("swipe", 0);
		} else {
			PlayerPrefs.SetInt ("swipe", 1);
		}
	}
	public void sensitivity(float x)
	{
		if(x==0)
			PlayerPrefs.SetFloat ("ssy", 0.1f);
		else
			PlayerPrefs.SetFloat ("ssy", x);
	}

	public static void unlock_achievement(string id)
	{
		//Social.ReportProgress (id, 100, success => { });
	}

	public static void showAchievementUI()
	{
		//Social.ShowAchievementsUI ();
	}

	/*
	public static void send_score(string leaderboard_id,int score)
	{
		Social.ReportScore (score, leaderboard_id, success => {	});
	}*/
	// Update is called once per frame
	void Update () {
		
	}
	public void goto_play()
	{
		SceneManager.LoadScene ("game");
	}
	public void goto_exit()
	{
		Application.Quit ();
	}
	public void goto_credits()
	{
		menu_panel.SetActive (false);
		credits_panel.SetActive (true);
	}
	public void goto_options()
	{
		menu_panel.SetActive (false);
		options_panel.SetActive (true);
	}
	public void goto_leaderboard()
	{
		menu_panel.SetActive (false);
		leaderboard_panel.SetActive (true);
	}
	public void goto_how_to()
	{
		menu_panel.SetActive (false);
		how_panel.SetActive (true);
	}
	public void credits_to_menu()
	{
		credits_panel.SetActive (false);
		menu_panel.SetActive (true);
	}
	public void leaderboard_to_menu()
	{
		leaderboard_panel.SetActive (false);
		menu_panel.SetActive (true);
	}
	public void how_to_menu()
	{
		menu_panel.SetActive (true);
		how_panel.SetActive (false);
	}
	public void options_to_menu()
	{
		menu_panel.SetActive (true);
		options_panel.SetActive (false);
	}
	public void skyboxes(int x)
	{
		PlayerPrefs.SetInt ("skybox",x);
		RenderSettings.skybox=skyboxx[x];
	}

	public void difficulty(int x)
	{
		PlayerPrefs.SetInt ("difficulty", x);

		if (PlayerPrefs.GetInt ("difficulty", 1) == 0)
			Time.timeScale = 0.5f;
		else if (PlayerPrefs.GetInt ("difficulty", 1) == 1)
			Time.timeScale = 0.75f;
		else
			Time.timeScale = 1f;
		
	}

}
