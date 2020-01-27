using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
/*
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GoogleMobileAds;
using GoogleMobileAds.Api;
*/

public class simple : MonoBehaviour {

	public GameObject[] shapes;
	//public RewardBasedVideoAd rbva;
	public Image[] i_next=new Image[3];
	public GameObject red_cube,shado;
	public GameObject[,] redline=new GameObject[10,10];
	public GameObject shadow_array;
	public Text mytext,highscore,points,print_time;
	public int[,,] matrix=new int[15,15,15];
	public int[] filled = new int[19];
	int[] next = new int[4];
	public Vector3 pos;
	public float tym = 0,game_over_tym=0;
	public float oldtym = 0,s_tym=0,speed;
	public int si,score = 0, c_cleared = 0,testing=0,xtran=20,xx,xtras=0,temp,i,min_range,max_range,j,k,x,y;
	public static int number_paused=0;
	public bool doit,goin_down,goin_down_function,add_new_gameobj,keep,range_i,goto_label,up,down,right,left,dont_do_it;  
	public GameObject current,Empty,faltu_me_udega;
	public GameObject gb,game_over_panel;
	//public GameObject box;
	public GameObject all,axis;
	public GameObject[] xtra;
	public float hor, ver, mx, my;

	void Start () {
		/*
		MobileAds.Initialize ("ca-app-pub-7207006250938599~6506936244");
		rbva = RewardBasedVideoAd.Instance;
		rbva.LoadAd (new AdRequest.Builder ().Build (), "ca-app-pub-7207006250938599/9733145014");
		*/
		game_over_tym = 0;
		up = false;
		down = false;
		right = false;
		left = false;
		Time.timeScale = 1;
		tym = 0;
		goin_down_function = false;
		oldtym = 0;
		xtra=new GameObject[20];
		tym=0;
		Empty = all;
		min_range = 3;
		max_range = 8;
		dont_do_it = false;

		for (i = 0; i < 3; i++) {
			next [i] = Random.Range (0, 6);
			if (next [i] == 0)
				i_next [i].color = new Color (0, 1, 0);
			else if (next [i] == 1)
				i_next [i].color = new Color (0,0, 1);
			else if (next [i] == 2)
				i_next [i].color = new Color (1,1, 0);
			else if (next [i] == 3)
				i_next [i].color = new Color (1,0, 1);
			else if (next [i] == 4)
				i_next [i].color = new Color (0,1, 1);
			else if (next [i] == 5)
				i_next [i].color = new Color (1,0, 0);
		}



		if (PlayerPrefs.GetInt ("difficulty", 1) == 0)
			Time.timeScale = 0.5f;
		else if (PlayerPrefs.GetInt ("difficulty", 1) == 1)
			Time.timeScale = 0.75f;
		else
			Time.timeScale = 1f;

		for (i = min_range; i <= max_range; i++)
			for (j = min_range; j <= max_range; j++) {
				redline [i, j] = Instantiate (red_cube, new Vector3 (i, 8f-0.45f, j), Quaternion.identity) as GameObject;
				redline [i, j].SetActive (false);
			}
		}

	public void go_down()
	{
		goin_down_function = true;
	}


	void Update () {

		if (game_over_panel.activeSelf)
			game_over_tym += Time.deltaTime;
		/*
		if (game_over_tym >= 0.5f) {
			//show reward video****************************************************************************************************
			if (rbva.IsLoaded ())
				rbva.Show ();
		}
		*/
		mytext.text = score.ToString();
	//	mytext.text=Joystick.strt.x.ToString();
			//hor.ToString();
	//	mytext.text=mx.ToString();
		for (i = min_range; i <= max_range; i++)
			for (j = min_range; j <= max_range; j++)
						redline [i, j].SetActive (false);
		
		for (i = min_range; i <= max_range; i++)
			for (j = min_range; j <= max_range; j++)
				for (k = 8; k > 4; k--)
					if (matrix [i, k, j] == 1)
						redline [i, j].SetActive (true);

		//************************************* delete shadow**********************************************************
		foreach(Transform sd in shadow_array.transform)
			Destroy(sd.gameObject);

		//*************************************************************isatancing**************************************************************

		doit = true;
		if (tym == 0) {
			dont_do_it = false;
			goin_down_function = false;
			score += 10;
			oldtym = tym;
			pos = new Vector3 (Random.Range(5,6), 8,Random.Range(5,6));
			gb = shapes [next[0]];
			next [0] = next [1];
			next [1] = next [2];
			next [2] =  Random.Range (0, 6);

			for (i = 0; i < 3; i++) {
				if (next [i] == 0)
					i_next [i].color = new Color (0, 1, 0);
				else if (next [i] == 1)
					i_next [i].color = new Color (0,0, 1);
				else if (next [i] == 2)
					i_next [i].color = new Color (1,1, 0);
				else if (next [i] == 3)
					i_next [i].color = new Color (1,0, 1);
				else if (next [i] == 4)
					i_next [i].color = new Color (0,1, 1);
				else if (next [i] == 5)
					i_next [i].color = new Color (1,0, 0);
			}

			current = Instantiate (gb, pos, Quaternion.identity )as GameObject;//////////////////////////////////////////////instancing
			current.transform.parent = transform;

			//print (filled [0] + " " + filled [1] + " " + filled [2] + " " + filled [3] + " " + filled [4] + " " + filled [5] + " " + filled [6] + " " + filled [7] + " " + filled [8]);
		}

		//************************************************************controls***************************************************************

		//*********************************************************end move in horizontal plane****************************************************

		//*************************************************************one down**************************************************************

		tym +=Time.deltaTime;
		s_tym += Time.deltaTime;
		if (tym - oldtym > 1f)
		{

			oldtym = tym;
			doit = true;
			foreach (Transform child in current.transform)																			/////////////////can go down?
				if (matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y) - 1, (int)Mathf.RoundToInt(child.position.z)] == 1 || (int)Mathf.RoundToInt(child.position.y) <= 2) {
					doit = false;
					break;
				}
			if (doit) {																				

				current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x), (int)Mathf.RoundToInt(current.transform.position.y) - 1, (int)Mathf.RoundToInt(current.transform.position.z));

			} else {
				dont_do_it = true;
				foreach (Transform child in current.transform)
					print (child.position.x.ToString() + child.position.y.ToString() + child.position.z.ToString());
				foreach (


					Transform child in current.transform) {
					if ((int)Mathf.RoundToInt(child.position.y) > 7) {
						//*************************************GAME OVER***************************************************


						highscore.text = "Score : "+((int)score*10/((int)s_tym+5*number_paused)).ToString();
						points.text = "Points : "+score.ToString();
						print_time.text = "Time : "+((int)s_tym+5*number_paused).ToString();


						tym=10;
						Time.timeScale = 0;
						number_paused = game_script.n_paused;
						if((int)score*10/((int)s_tym+5*number_paused)>PlayerPrefs.GetInt("score",0))
							{
							PlayerPrefs.SetInt ("score", (int)score*10/((int)s_tym+5*number_paused));
							PlayerPrefs.SetInt ("points", score);
							PlayerPrefs.SetInt ("time", (int)s_tym+5*number_paused);
							}
						game_over_panel.SetActive (true);
						return;
						//DISPLAY GAME OVER BANNER PANEL


					}
					matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] = 1;
					filled [(int)Mathf.RoundToInt(child.position.y)] += 1;  
					tym = 0;

				}


				//*************************************************************check line removal**************************************************************

				label:
				//print ("testing"+testing);
				for ( i = 0; i < 10; i++)
				{
					//print ("filled" + i + filled [i]);
					if (filled [i]>=36) {	

						//***********************************************instancing empty parent*************************************

						xtras = 0;
						Empty = all;
						for (int k = 0; k < xtran; k++) {
							pos = new Vector3 (10, 10, 10);
							xtra[k] = Instantiate (Empty, pos, Quaternion.identity) as GameObject;//////////////////////////////////////////////instancing
							xtra[k].transform.parent = transform;
						}

						//	print ("filled is greater tha 8" + i + filled [i]);	
						//print (score);		
						c_cleared++;
						score += 200+ i*c_cleared;

						//		print ("transform" + transform.name);
						foreach (Transform array in transform) {
							if (array.childCount == 0)
								continue;
							//	print ("array " + array.name);
							add_new_gameobj = true;


							//**************************************************************check if the gameobject is in tha range of i******************
							range_i=false;
							foreach (Transform child in array)
								if ((int)Mathf.RoundToInt(child.position.y) == i)
									range_i = true;

							if (range_i == false)
								continue;
							//****************************************************************************************************************************

							//	while (array.childCount > 0) {{{{{
							Transform[] Array=array.GetComponentsInChildren<Transform>();

							foreach (Transform child in Array) {
								if (child.tag != "box")
									continue;
																////// less than i is not destroyed.....its kept.......
								if ((int)Mathf.RoundToInt(child.position.y) == i) {
									
									matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] = 0;
									//print ("destroyed" +child.name +(int)Mathf.RoundToInt(child.position.x)+" "+ ((int)Mathf.RoundToInt(child.position.y)) + " " +(int)Mathf.RoundToInt(child.position.z)+" "+ matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y) - 1, (int)Mathf.RoundToInt(child.position.z)]);

									//	print ("make0" + (int)Mathf.RoundToInt(child.position.x) + " " + (int)Mathf.RoundToInt(child.position.z) + " ");
									filled [i] -= 1;
									DestroyImmediate (child.gameObject);

									//Destroy (child.gameObject);
								} else if ((int)Mathf.RoundToInt(child.position.y) > i) {
									//	print ("greater" + child.name);
									if (add_new_gameobj) {
										xtra [xtras].transform.position = array.position;
										add_new_gameobj = false;
									}
									child.transform.parent = xtra [xtras].transform;
								}
							}
							if (add_new_gameobj == false)
								xtras++;
							
						}
						//********************************destroy empty***********************************
						foreach (Transform array in transform)
							if (array.childCount == 0)
								DestroyImmediate (array.gameObject);
						/////////////////////////////////////check if possible/////////////////////
						tym = 0;
					}
				}

				/////////////*******************************************goin_down while loop***************************************//////////////
				/// 
				goto_label=false;
				goin_down=true;
				while (goin_down) {

					goin_down = false;
					//	print ("goin down");
					foreach (Transform array in transform) {
						/*
						for (int p = 0; p < 10; p++)
							for (int q = 0; q < 10; q++)
								temp+=matrix [p, i, q];
						print("no of 1 in "+i+" "+temp);
*/
						if (array.childCount == 0 || array.tag!="shape")
							continue;

						foreach (Transform child in array) {
							matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] = 0;
							filled [(int)Mathf.RoundToInt(child.position.y)] -= 1;
						}
						doit = true;
						foreach (Transform child in array)																				/////////////////can go down?
							if (matrix [(int)Mathf.RoundToInt(child.position.x), ((int)Mathf.RoundToInt(child.position.y) - 1), (int)Mathf.RoundToInt(child.position.z)] == 1 || (int)Mathf.RoundToInt(child.position.y) <= 2) {
							//	print ("child name "+array.name+child.name +(int)Mathf.RoundToInt(child.position.x)+" "+ ((int)Mathf.RoundToInt(child.position.y) - 1) + " " +(int)Mathf.RoundToInt(child.position.z)+" "+ matrix [(int)Mathf.RoundToInt(child.position.x), ((int)Mathf.RoundToInt(child.position.y) - 1), (int)Mathf.RoundToInt(child.position.z)]);
							//	print ("cant go down "+array.name);
								doit = false;
								break;
							}
						if (doit) {												/////////////// go down
							//goin_down=true;
					//		print ("goin down "+array.name);
							goto_label=true;

							///////errror
							array.position = new Vector3 ((int)Mathf.RoundToInt(array.position.x), (int)Mathf.RoundToInt(array.position.y) - 1, (int)Mathf.RoundToInt(array.position.z));
							goin_down = true;

						} 
						foreach (Transform child in array) {
					//		print ("child to 1  "+array.name+child.name +(int)Mathf.RoundToInt(child.position.x)+" "+ ((int)Mathf.RoundToInt(child.position.y)) + " " +(int)Mathf.RoundToInt(child.position.z)+" "+ matrix [(int)Mathf.RoundToInt(child.position.x), ((int)Mathf.RoundToInt(child.position.y)), (int)Mathf.RoundToInt(child.position.z)]);
							matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] = 1;
							filled [(int)Mathf.RoundToInt(child.position.y)] += 1;                                        
						}
					
					}
				}

			//	if (goto_label == true)
			//		goto label;
				//***************************************************while end**********************goin down***************************

				//	for (int i = 0; i < 10; i++)
				//		print ("filled after while" + i + filled [i]);
				print ("score"+score);
			}
		}
		if (dont_do_it == true)
			return;
		if (tym == 0)
			goto shadow;

		//********************************************* tym==0 return;********************************************************


		hor = CrossPlatformInputManager.GetAxis ("Horizontal");
		ver = CrossPlatformInputManager.GetAxis ("Vertical");
		mx = CrossPlatformInputManager.GetAxis ("Mouse X");
		my = CrossPlatformInputManager.GetAxis ("Mouse Y");

		//(************************************************************rotation***************************************************************
		if (Mathf.Abs (mx) >200f  && current.gameObject.name!="BigCube(Clone)"/* 0.05f*Screen.dpi*/) {


			print ("dragging x");

			TouchPad.resetx = true;
			CrossPlatformInputManager.SetAxis ("Mouse X", 0f);


			if (axis.transform.position.y >= 8.90) {
				if ((mx > 0 && (axis.transform.localEulerAngles.y < 45 || axis.transform.localEulerAngles.y > 315)) || (mx < 0 && (axis.transform.localEulerAngles.y > 135 && axis.transform.localEulerAngles.y < 225))) {
					current.transform.Rotate (0, 0, -90, Space.World);
					foreach (Transform child in current.transform)																				/////////////////can go down?
						if (matrix [(int)Mathf.RoundToInt (child.position.x), (int)Mathf.RoundToInt (child.position.y), (int)Mathf.RoundToInt (child.position.z)] == 1 || (int)Mathf.RoundToInt (child.position.x) > max_range || (int)Mathf.RoundToInt (child.position.x) < min_range || (int)Mathf.RoundToInt (child.position.z) > max_range || (int)Mathf.RoundToInt (child.position.z) < min_range || (int)Mathf.RoundToInt (child.position.y) <= 2) {
							current.transform.Rotate (0, 0, 90, Space.World);
							break;
						}
				} else if ((mx < 0 && (axis.transform.localEulerAngles.y < 45 || axis.transform.localEulerAngles.y > 315)) || (mx > 0 && (axis.transform.localEulerAngles.y > 135 && axis.transform.localEulerAngles.y < 225))) {
					current.transform.Rotate (0, 0, 90, Space.World);
					foreach (Transform child in current.transform)																				/////////////////can go down?
						if (matrix [(int)Mathf.RoundToInt (child.position.x), (int)Mathf.RoundToInt (child.position.y), (int)Mathf.RoundToInt (child.position.z)] == 1 || (int)Mathf.RoundToInt (child.position.x) > max_range || (int)Mathf.RoundToInt (child.position.x) < min_range || (int)Mathf.RoundToInt (child.position.z) > max_range || (int)Mathf.RoundToInt (child.position.z) < min_range || (int)Mathf.RoundToInt (child.position.y) <= 2) {
							current.transform.Rotate (0, 0, -90, Space.World);
							break;
						}
				} else if ((mx > 0 && axis.transform.localEulerAngles.y > 45 && axis.transform.localEulerAngles.y < 135) || (mx < 0 && axis.transform.localEulerAngles.y > 225 && axis.transform.localEulerAngles.y < 315)) {
					current.transform.Rotate (-90, 0, 0, Space.World);
					foreach (Transform child in current.transform)																				/////////////////can go down?
						if (matrix [(int)Mathf.RoundToInt (child.position.x), (int)Mathf.RoundToInt (child.position.y), (int)Mathf.RoundToInt (child.position.z)] == 1 || (int)Mathf.RoundToInt (child.position.x) > max_range || (int)Mathf.RoundToInt (child.position.x) < min_range || (int)Mathf.RoundToInt (child.position.z) > max_range || (int)Mathf.RoundToInt (child.position.z) < min_range || (int)Mathf.RoundToInt (child.position.y) <= 2) {
							current.transform.Rotate (90, 0,0, Space.World);
							break;
						}
				} else if ((mx < 0 && axis.transform.localEulerAngles.y > 45 && axis.transform.localEulerAngles.y < 135) || (mx > 0 && axis.transform.localEulerAngles.y > 225 && axis.transform.localEulerAngles.y < 315)) {
					current.transform.Rotate (90, 0, 0, Space.World);
					foreach (Transform child in current.transform)																				/////////////////can go down?
						if (matrix [(int)Mathf.RoundToInt (child.position.x), (int)Mathf.RoundToInt (child.position.y), (int)Mathf.RoundToInt (child.position.z)] == 1 || (int)Mathf.RoundToInt (child.position.x) > max_range || (int)Mathf.RoundToInt (child.position.x) < min_range || (int)Mathf.RoundToInt (child.position.z) > max_range || (int)Mathf.RoundToInt (child.position.z) < min_range || (int)Mathf.RoundToInt (child.position.y) <= 2) {
							current.transform.Rotate (-90, 0, 0, Space.World);
							break;
						}
				} 
			}else {	

				if (mx > 0) {
					current.transform.Rotate (0, -90, 0, Space.World);
					foreach (Transform child in current.transform)																				/////////////////can go down?
							if (matrix [(int)Mathf.RoundToInt (child.position.x), (int)Mathf.RoundToInt (child.position.y), (int)Mathf.RoundToInt (child.position.z)] == 1 || (int)Mathf.RoundToInt (child.position.x) > max_range || (int)Mathf.RoundToInt (child.position.x) < min_range || (int)Mathf.RoundToInt (child.position.z) > max_range || (int)Mathf.RoundToInt (child.position.z) < min_range || (int)Mathf.RoundToInt (child.position.y) <= 2) {
							current.transform.Rotate (0, 90, 0, Space.World);
							break;
						}
				} else {
					current.transform.Rotate (0, 90, 0, Space.World);
					foreach (Transform child in current.transform)																				/////////////////can go down?
							if (matrix [(int)Mathf.RoundToInt (child.position.x), (int)Mathf.RoundToInt (child.position.y), (int)Mathf.RoundToInt (child.position.z)] == 1 || (int)Mathf.RoundToInt (child.position.x) > max_range || (int)Mathf.RoundToInt (child.position.x) < min_range || (int)Mathf.RoundToInt (child.position.z) > max_range || (int)Mathf.RoundToInt (child.position.z) < min_range || (int)Mathf.RoundToInt (child.position.y) <= 2) {
							current.transform.Rotate (0, -90, 0, Space.World);
							break;
						}
				}
			}
		}
		if (Mathf.Abs (my) > 200f  && current.gameObject.name!="BigCube(Clone)") {
			TouchPad.resety = true;
			print ("dragging y");

			CrossPlatformInputManager.SetAxis ("Mouse Y", 0f);

			if ((my > 0 && (axis.transform.localEulerAngles.y < 45 || axis.transform.localEulerAngles.y > 315 )) || (my < 0 && (axis.transform.localEulerAngles.y >135 &&  axis.transform.localEulerAngles.y < 225 ))) {
				current.transform.Rotate (90, 0, 0,Space.World);
				foreach (Transform child in current.transform)																				/////////////////can go down?
					if (matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] == 1  ||(int)Mathf.RoundToInt(child.position.x)>max_range ||(int)Mathf.RoundToInt(child.position.x)<min_range ||(int)Mathf.RoundToInt(child.position.z)>max_range ||(int)Mathf.RoundToInt(child.position.z)<min_range || (int)Mathf.RoundToInt(child.position.y) <= 2) {
						current.transform.Rotate (-90, 0, 0,Space.World);
						break;
					}
			} else if ((my < 0 && (axis.transform.localEulerAngles.y < 45 || axis.transform.localEulerAngles.y > 315 )) || (my > 0 && (axis.transform.localEulerAngles.y >135 &&  axis.transform.localEulerAngles.y < 225 ))) {
				current.transform.Rotate (-90, 0, 0,Space.World);
				foreach (Transform child in current.transform)																				/////////////////can go down?
					if (matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] == 1 ||(int)Mathf.RoundToInt(child.position.x)>max_range ||(int)Mathf.RoundToInt(child.position.x)<min_range ||(int)Mathf.RoundToInt(child.position.z)>max_range ||(int)Mathf.RoundToInt(child.position.z)<min_range || (int)Mathf.RoundToInt(child.position.y) <= 2) {
						current.transform.Rotate (+90, 0, 0,Space.World);
						break;
					}
			} else if ((my > 0 && axis.transform.localEulerAngles.y > 45 && axis.transform.localEulerAngles.y <135 ) || (my < 0 && axis.transform.localEulerAngles.y > 225 &&  axis.transform.localEulerAngles.y < 315 )) {
				current.transform.Rotate (0, 0, -90,Space.World);
				foreach (Transform child in current.transform)																				/////////////////can go down?
					if (matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] == 1 ||(int)Mathf.RoundToInt(child.position.x)>max_range ||(int)Mathf.RoundToInt(child.position.x)<min_range ||(int)Mathf.RoundToInt(child.position.z)>max_range ||(int)Mathf.RoundToInt(child.position.z)<min_range || (int)Mathf.RoundToInt(child.position.y) <= 2) {
						current.transform.Rotate (0, 0, 90,Space.World);
						break;
					}
			} else if ((my < 0 && axis.transform.localEulerAngles.y > 45 && axis.transform.localEulerAngles.y < 135 ) || (my > 0 && axis.transform.localEulerAngles.y > 225 &&  axis.transform.localEulerAngles.y < 315 )) {
				current.transform.Rotate (0, 0, 90,Space.World);
				foreach (Transform child in current.transform)																				/////////////////can go down?
					if (matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] == 1 ||(int)Mathf.RoundToInt(child.position.x)>max_range ||(int)Mathf.RoundToInt(child.position.x)<min_range ||(int)Mathf.RoundToInt(child.position.z)>max_range ||(int)Mathf.RoundToInt(child.position.z)<min_range || (int)Mathf.RoundToInt(child.position.y) <= 2) {
						current.transform.Rotate (0, 0, -90,Space.World);
						break;
					}
			}
		}
		//*******************************************************end rotate****************************************************************************

		//mytext.text = axis.transform.rotation.y).ToString();
		/*
		if (-3 > -4)
			mytext.text = "-3>-4";
		else
			mytext.text = "-3<-4";
		*/
		//*********************************************************move in horizontal plane**************************************************
		if (Mathf.Abs (hor) > 0.5f) {
			if (hor > 0) {
				Joystick.xx = 1;
				Joystick.yy = 0;
				Joystick.reset = true;
			}
			else{
				Joystick.xx = -1;
				Joystick.yy = 0;
				Joystick.reset = true;
			}

			CrossPlatformInputManager.SetAxis ("Horizontal", 0f);

			if ((hor > 0 && (axis.transform.localEulerAngles.y < 45 || axis.transform.localEulerAngles.y > 315 )) || (hor < 0 && (axis.transform.localEulerAngles.y > 135 &&  axis.transform.localEulerAngles.y < 225 ))) {
				current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x)+1, (int)Mathf.RoundToInt(current.transform.position.y), (int)Mathf.RoundToInt(current.transform.position.z));

				//	mytext.text = "+x"+hor.ToString();
				foreach (Transform child in current.transform)																				/////////////////can go down?
							if (matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] == 1   ||(int)Mathf.RoundToInt(child.position.x)>max_range ||(int)Mathf.RoundToInt(child.position.x)<min_range ||(int)Mathf.RoundToInt(child.position.z)>max_range ||(int)Mathf.RoundToInt(child.position.z)<min_range) {
						current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x)-1, (int)Mathf.RoundToInt(current.transform.position.y), (int)Mathf.RoundToInt(current.transform.position.z));
						break;
					}
			} else if ((hor < 0 && (axis.transform.localEulerAngles.y < 45 || axis.transform.localEulerAngles.y > 315 )) || (hor > 0 && (axis.transform.localEulerAngles.y > 135 &&  axis.transform.localEulerAngles.y < 225 ))) {
				current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x)-1, (int)Mathf.RoundToInt(current.transform.position.y), (int)Mathf.RoundToInt(current.transform.position.z));

				//mytext.text = "-x"+hor.ToString();
				foreach (Transform child in current.transform)																				/////////////////can go down?
							if (matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] == 1  ||(int)Mathf.RoundToInt(child.position.x)>max_range ||(int)Mathf.RoundToInt(child.position.x)<min_range ||(int)Mathf.RoundToInt(child.position.z)>max_range ||(int)Mathf.RoundToInt(child.position.z)<min_range) {
						current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x)+1, (int)Mathf.RoundToInt(current.transform.position.y), (int)Mathf.RoundToInt(current.transform.position.z));
						break;
					}
			} else if ((hor > 0 && axis.transform.localEulerAngles.y > 45 && axis.transform.localEulerAngles.y <135 ) || (hor < 0 && axis.transform.localEulerAngles.y > 225 &&  axis.transform.localEulerAngles.y < 315 )) {
				current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x), (int)Mathf.RoundToInt(current.transform.position.y) , (int)Mathf.RoundToInt(current.transform.position.z)-1);

				//	mytext.text = "-z"+hor.ToString();
				foreach (Transform child in current.transform)																				/////////////////can go down?
							if (matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] == 1   ||(int)Mathf.RoundToInt(child.position.x)>max_range ||(int)Mathf.RoundToInt(child.position.x)<min_range ||(int)Mathf.RoundToInt(child.position.z)>max_range ||(int)Mathf.RoundToInt(child.position.z)<min_range) {
						current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x), (int)Mathf.RoundToInt(current.transform.position.y), (int)Mathf.RoundToInt(current.transform.position.z)+1);
						break;
					}
			} else if ((hor < 0 && axis.transform.localEulerAngles.y > 45 && axis.transform.localEulerAngles.y < 135 ) || (hor > 0 && axis.transform.localEulerAngles.y > 225 &&  axis.transform.localEulerAngles.y < 315 )) {
				current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x), (int)Mathf.RoundToInt(current.transform.position.y) , (int)Mathf.RoundToInt(current.transform.position.z)+1);

				//	mytext.text = "+z"+hor.ToString();
				foreach (Transform child in current.transform)																				/////////////////can go down?
							if (matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] == 1   ||(int)Mathf.RoundToInt(child.position.x)>max_range ||(int)Mathf.RoundToInt(child.position.x)<min_range ||(int)Mathf.RoundToInt(child.position.z)>max_range ||(int)Mathf.RoundToInt(child.position.z)<min_range) {
						current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x), (int)Mathf.RoundToInt(current.transform.position.y), (int)Mathf.RoundToInt(current.transform.position.z)-1);
						break;
					}
			}
		}

		//***************************************vertical****************************************
		if (Mathf.Abs (ver) > 0.5f) {

			if (ver > 0) {
				Joystick.xx = 0;
				Joystick.yy = 1;
				Joystick.reset = true;
			}
			else{
				Joystick.xx = 0;
				Joystick.yy = -1;
				Joystick.reset = true;
			}
			CrossPlatformInputManager.SetAxis ("Vertical", 0f);

			if ((ver > 0 && (axis.transform.localEulerAngles.y < 45 || axis.transform.localEulerAngles.y > 315 )) || (ver < 0 && (axis.transform.localEulerAngles.y >135 &&  axis.transform.localEulerAngles.y < 225 ))) {
				current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x), (int)Mathf.RoundToInt(current.transform.position.y) , (int)Mathf.RoundToInt(current.transform.position.z)+1);

				//mytext.text = "+z";
				foreach (Transform child in current.transform)																				/////////////////can go down?
							if (matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] == 1   ||(int)Mathf.RoundToInt(child.position.x)>max_range ||(int)Mathf.RoundToInt(child.position.x)<min_range ||(int)Mathf.RoundToInt(child.position.z)>max_range ||(int)Mathf.RoundToInt(child.position.z)<min_range) {
						current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x), (int)Mathf.RoundToInt(current.transform.position.y), (int)Mathf.RoundToInt(current.transform.position.z)-1);
						break;
					}
			} else if ((ver < 0 && (axis.transform.localEulerAngles.y < 45 || axis.transform.localEulerAngles.y > 315 )) || (ver > 0 && (axis.transform.localEulerAngles.y > 135 &&  axis.transform.localEulerAngles.y < 225 ))) {
				current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x), (int)Mathf.RoundToInt(current.transform.position.y) , (int)Mathf.RoundToInt(current.transform.position.z)-1);

				//mytext.text = "-z";
				foreach (Transform child in current.transform)																				/////////////////can go down?
							if (matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] == 1   ||(int)Mathf.RoundToInt(child.position.x)>max_range ||(int)Mathf.RoundToInt(child.position.x)<min_range ||(int)Mathf.RoundToInt(child.position.z)>max_range ||(int)Mathf.RoundToInt(child.position.z)<min_range) {
						current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x), (int)Mathf.RoundToInt(current.transform.position.y), (int)Mathf.RoundToInt(current.transform.position.z)+1);
						break;
					}
			} else if ((ver > 0 && axis.transform.localEulerAngles.y > 45 && axis.transform.localEulerAngles.y <  135 ) || (ver < 0 && axis.transform.localEulerAngles.y > 225 &&  axis.transform.localEulerAngles.y < 315 )) {
				current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x)+1, (int)Mathf.RoundToInt(current.transform.position.y) , (int)Mathf.RoundToInt(current.transform.position.z));

				//	mytext.text = "+x";
				foreach (Transform child in current.transform)																				/////////////////can go down?
							if (matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] == 1   ||(int)Mathf.RoundToInt(child.position.x)>max_range ||(int)Mathf.RoundToInt(child.position.x)<min_range ||(int)Mathf.RoundToInt(child.position.z)>max_range ||(int)Mathf.RoundToInt(child.position.z)<min_range) {
						current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x)-1, (int)Mathf.RoundToInt(current.transform.position.y), (int)Mathf.RoundToInt(current.transform.position.z));
						break;
					}
			} else if ((ver < 0 && axis.transform.localEulerAngles.y >45 && axis.transform.localEulerAngles.y < 135 ) || (ver > 0 && axis.transform.localEulerAngles.y > 225  &&  axis.transform.localEulerAngles.y < 315 )) {
				current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x)-1, (int)Mathf.RoundToInt(current.transform.position.y) , (int)Mathf.RoundToInt(current.transform.position.z));

				//	mytext.text = "-x";
				foreach (Transform child in current.transform)																				/////////////////can go down?
							if (matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] == 1   ||(int)Mathf.RoundToInt(child.position.x)>max_range ||(int)Mathf.RoundToInt(child.position.x)<min_range ||(int)Mathf.RoundToInt(child.position.z)>max_range ||(int)Mathf.RoundToInt(child.position.z)<min_range) {
						current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x)+1, (int)Mathf.RoundToInt(current.transform.position.y), (int)Mathf.RoundToInt(current.transform.position.z));
						break;
					}
			}
		}

		//*********************************button rotate***********************************************

		if (up == true || down==true) {
			print ("verticle");
			if (up)
				y = 1;
			else
				y = -1;
			up = false;
			down = false;
			if (current.gameObject.name != "BigCube(Clone)") {
				print (current.gameObject.name);
				if ((y > 0 && (axis.transform.localEulerAngles.y < 45 || axis.transform.localEulerAngles.y > 315)) || (y < 0 && (axis.transform.localEulerAngles.y > 135 && axis.transform.localEulerAngles.y < 225))) {
					print ("1 did it");
					current.transform.Rotate (90, 0, 0,Space.World);
					foreach (Transform child in current.transform)																				/////////////////can go down?
						if (matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] == 1 || (int)Mathf.RoundToInt(child.position.x) > max_range || (int)Mathf.RoundToInt(child.position.x) < min_range || (int)Mathf.RoundToInt(child.position.z) > max_range || (int)Mathf.RoundToInt(child.position.z) < min_range || (int)Mathf.RoundToInt(child.position.y) <= 2) {
							current.transform.Rotate (-90, 0, 0,Space.World);
							break;
						}
				} else if ((y < 0 && (axis.transform.localEulerAngles.y < 45 || axis.transform.localEulerAngles.y > 315)) || (y > 0 && (axis.transform.localEulerAngles.y > 135 && axis.transform.localEulerAngles.y < 225))) {
					print ("2 did it");
					current.transform.Rotate (-90, 0, 0,Space.World);
					foreach (Transform child in current.transform)																				/////////////////can go down?
						if (matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] == 1 || (int)Mathf.RoundToInt(child.position.x) > max_range || (int)Mathf.RoundToInt(child.position.x) < min_range || (int)Mathf.RoundToInt(child.position.z) > max_range || (int)Mathf.RoundToInt(child.position.z) < min_range || (int)Mathf.RoundToInt(child.position.y) <= 2) {
							current.transform.Rotate (+90, 0, 0,Space.World);
							break;
						}
				} else if ((y > 0 && axis.transform.localEulerAngles.y > 45 && axis.transform.localEulerAngles.y < 135) || (y < 0 && axis.transform.localEulerAngles.y > 225 && axis.transform.localEulerAngles.y < 315)) {
					print ("3 did it");
					current.transform.Rotate (0, 0, -90,Space.World);
					foreach (Transform child in current.transform)																				/////////////////can go down?
						if (matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] == 1 || (int)Mathf.RoundToInt(child.position.x) > max_range || (int)Mathf.RoundToInt(child.position.x) < min_range || (int)Mathf.RoundToInt(child.position.z) > max_range || (int)Mathf.RoundToInt(child.position.z) < min_range || (int)Mathf.RoundToInt(child.position.y) <= 2) {
							current.transform.Rotate (0, 0, 90,Space.World);
							break;
						}
				} else if ((y < 0 && axis.transform.localEulerAngles.y > 45 && axis.transform.localEulerAngles.y < 135) || (y > 0 && axis.transform.localEulerAngles.y > 225 && axis.transform.localEulerAngles.y < 315)) {
					print ("4 did it");
					current.transform.Rotate (0, 0, 90,Space.World);
					foreach (Transform child in current.transform)																				/////////////////can go down?
						if (matrix [(int)Mathf.RoundToInt(child.position.x), (int)Mathf.RoundToInt(child.position.y), (int)Mathf.RoundToInt(child.position.z)] == 1 || (int)Mathf.RoundToInt(child.position.x) > max_range || (int)Mathf.RoundToInt(child.position.x) < min_range || (int)Mathf.RoundToInt(child.position.z) > max_range || (int)Mathf.RoundToInt(child.position.z) < min_range || (int)Mathf.RoundToInt(child.position.y) <= 2) {
							current.transform.Rotate (0, 0, -90,Space.World);
							break;
						}
				}
			}
		}
		else if(right==true || left ==true){
			print ("horizontal");
			if (right)
				x = 1;
			else
				x = -1;
			right=false;
			left = false;


			if (current.gameObject.name != "BigCube(Clone)"){
				if (axis.transform.position.y >= 8.90) {
					if ((x > 0 && (axis.transform.localEulerAngles.y < 45 || axis.transform.localEulerAngles.y > 315)) || (x < 0 && (axis.transform.localEulerAngles.y > 135 && axis.transform.localEulerAngles.y < 225))) {
						current.transform.Rotate (0, 0, -90, Space.World);
						foreach (Transform child in current.transform)																				/////////////////can go down?
							if (matrix [(int)Mathf.RoundToInt (child.position.x), (int)Mathf.RoundToInt (child.position.y), (int)Mathf.RoundToInt (child.position.z)] == 1 || (int)Mathf.RoundToInt (child.position.x) > max_range || (int)Mathf.RoundToInt (child.position.x) < min_range || (int)Mathf.RoundToInt (child.position.z) > max_range || (int)Mathf.RoundToInt (child.position.z) < min_range || (int)Mathf.RoundToInt (child.position.y) <= 2) {
								current.transform.Rotate (0, 0, 90, Space.World);
								break;
							}
					} else if ((x < 0 && (axis.transform.localEulerAngles.y < 45 || axis.transform.localEulerAngles.y > 315)) || (x > 0 && (axis.transform.localEulerAngles.y > 135 && axis.transform.localEulerAngles.y < 225))) {
						current.transform.Rotate (0, 0, 90, Space.World);
						foreach (Transform child in current.transform)																				/////////////////can go down?
							if (matrix [(int)Mathf.RoundToInt (child.position.x), (int)Mathf.RoundToInt (child.position.y), (int)Mathf.RoundToInt (child.position.z)] == 1 || (int)Mathf.RoundToInt (child.position.x) > max_range || (int)Mathf.RoundToInt (child.position.x) < min_range || (int)Mathf.RoundToInt (child.position.z) > max_range || (int)Mathf.RoundToInt (child.position.z) < min_range || (int)Mathf.RoundToInt (child.position.y) <= 2) {
								current.transform.Rotate (0, 0, -90, Space.World);
								break;
							}
					} else if ((x > 0 && axis.transform.localEulerAngles.y > 45 && axis.transform.localEulerAngles.y < 135) || (x < 0 && axis.transform.localEulerAngles.y > 225 && axis.transform.localEulerAngles.y < 315)) {
						current.transform.Rotate (-90, 0, 0, Space.World);
						foreach (Transform child in current.transform)																				/////////////////can go down?
							if (matrix [(int)Mathf.RoundToInt (child.position.x), (int)Mathf.RoundToInt (child.position.y), (int)Mathf.RoundToInt (child.position.z)] == 1 || (int)Mathf.RoundToInt (child.position.x) > max_range || (int)Mathf.RoundToInt (child.position.x) < min_range || (int)Mathf.RoundToInt (child.position.z) > max_range || (int)Mathf.RoundToInt (child.position.z) < min_range || (int)Mathf.RoundToInt (child.position.y) <= 2) {
								current.transform.Rotate (90, 0, 0, Space.World);
								break;
							}
					} else if ((x < 0 && axis.transform.localEulerAngles.y > 45 && axis.transform.localEulerAngles.y < 135) || (x > 0 && axis.transform.localEulerAngles.y > 225 && axis.transform.localEulerAngles.y < 315)) {
						current.transform.Rotate (90, 0, 0, Space.World);
						foreach (Transform child in current.transform)																				/////////////////can go down?
							if (matrix [(int)Mathf.RoundToInt (child.position.x), (int)Mathf.RoundToInt (child.position.y), (int)Mathf.RoundToInt (child.position.z)] == 1 || (int)Mathf.RoundToInt (child.position.x) > max_range || (int)Mathf.RoundToInt (child.position.x) < min_range || (int)Mathf.RoundToInt (child.position.z) > max_range || (int)Mathf.RoundToInt (child.position.z) < min_range || (int)Mathf.RoundToInt (child.position.y) <= 2) {
								current.transform.Rotate (-90, 0, 0, Space.World);
								break;
							}
					} 
				} else {	
				
					if (x > 0) {
						print ("5 did it");
						current.transform.Rotate (0, -90, 0, Space.World);
						foreach (Transform child in current.transform)																				/////////////////can go down?
							if (matrix [(int)Mathf.RoundToInt (child.position.x), (int)Mathf.RoundToInt (child.position.y), (int)Mathf.RoundToInt (child.position.z)] == 1 || (int)Mathf.RoundToInt (child.position.x) > max_range || (int)Mathf.RoundToInt (child.position.x) < min_range || (int)Mathf.RoundToInt (child.position.z) > max_range || (int)Mathf.RoundToInt (child.position.z) < min_range || (int)Mathf.RoundToInt (child.position.y) <= 2) {
								current.transform.Rotate (0, 90, 0, Space.World);
								break;
							}
					} else {
						print ("6 did it");
						current.transform.Rotate (0, 90, 0, Space.World);
						foreach (Transform child in current.transform)																				/////////////////can go down?
							if (matrix [(int)Mathf.RoundToInt (child.position.x), (int)Mathf.RoundToInt (child.position.y), (int)Mathf.RoundToInt (child.position.z)] == 1 || (int)Mathf.RoundToInt (child.position.x) > max_range || (int)Mathf.RoundToInt (child.position.x) < min_range || (int)Mathf.RoundToInt (child.position.z) > max_range || (int)Mathf.RoundToInt (child.position.z) < min_range || (int)Mathf.RoundToInt (child.position.y) <= 2) {
								current.transform.Rotate (0, -90, 0, Space.World);
								break;
							}
					}
				}
			}
		}

		//*********************************create shadow*******************************

		if (goin_down_function == true) {

			while (true) {
				current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x), (int)Mathf.RoundToInt(current.transform.position.y) - 1, (int)Mathf.RoundToInt(current.transform.position.z));
				foreach (Transform child in current.transform)																				/////////////////can go down?
					if(matrix [(int)Mathf.RoundToInt(child.position.x),(int)Mathf.RoundToInt(child.position.y)-1,(int)Mathf.RoundToInt(child.position.z)] == 1  ||  (int)Mathf.RoundToInt(child.position.y)<=2)  
					{
						current.transform.position = new Vector3 ((int)Mathf.RoundToInt(current.transform.position.x), (int)Mathf.RoundToInt(current.transform.position.y) + 1, (int)Mathf.RoundToInt(current.transform.position.z));
						//goto label;
						goin_down_function = false;
						tym += 1f;
						dont_do_it = true;
						//tym=0;
						return;
					}
			}
		}
		shadow:

		foreach (Transform child in current.transform) {
			si = (int)Mathf.RoundToInt(child.position.y);
			while (matrix [(int)Mathf.RoundToInt(child.position.x), si-1, (int)Mathf.RoundToInt(child.position.z)] == 0 && si>2) {
				si--;
			}
			gb = Instantiate (shado, new Vector3((int)Mathf.RoundToInt(child.position.x), si-0.45f, (int)Mathf.RoundToInt(child.position.z)), Quaternion.identity) as GameObject;//////////////////////////////////////////////instancing
			gb.transform.parent = shadow_array.transform;
		}
	}

	public void rotating(int x){
		if(x==0)
			up=true;
		else if(x==1)
			right=true;
		else if(x==2)
			down=true;
		else if(x==3)
			left=true;
	}

}
