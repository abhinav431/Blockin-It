using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class axis : MonoBehaviour {

	public Text mytext;
	float y,sensitivity;
	public Transform cam;
	bool htilt,vtilt,veri,hori;
	int xx,yy;
	// Use this for initialization
	void Start () {	
		veri = false;
		hori = false;
		sensitivity = PlayerPrefs.GetFloat ("ssy", 1f);

		if (PlayerPrefs.GetInt ("htilt", 0) == 0) {
			htilt = true;
		} else {
			htilt = false;
		}
		if (PlayerPrefs.GetInt ("vtilt", 0) == 0) {
			vtilt = true;
		} else {
			vtilt = false;
		}
	}

	public void right()
	{
		if (transform.eulerAngles.y <= 90 && transform.eulerAngles.y !=0 ) {
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, 0, transform.eulerAngles.z);
			//mytext.text = "180 "+transform.rotation.y.ToString();
		}
			//transform.Rotate(new Vector3(transform.rotation.x,-90,transform.rotation.z));
		else if (transform.eulerAngles.y <=180 && transform.eulerAngles.y !=0 ) {
			//mytext.text = "-90 "+transform.rotation.y.ToString();
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, 90, transform.eulerAngles.z);
		}	//transform.Rotate(new Vector3(transform.rotation.x,-90,transform.rotation.z));
		else if (transform.eulerAngles.y <= 270 && transform.eulerAngles.y !=0 ){
			//mytext.text = "0 "+transform.rotation.y.ToString();
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, 180, transform.eulerAngles.z);
		}
			//transform.Rotate(new Vector3(transform.rotation.x,-90,transform.rotation.z));
		else {
			//mytext.text = "90 "+transform.rotation.y.ToString();
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, 270, transform.eulerAngles.z);
		}
		//else
		//	mytext.text = "wtf"+transform.rotation.y.ToString();
			//transform.Rotate(new Vector3(transform.rotation.x,-90,transform.rotation.z));
	}

	public void left()
	{
		if (transform.eulerAngles.y < 90 ) {
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, 90, transform.eulerAngles.z);
			//mytext.text = "-90 "+transform.rotation.y.ToString();
		}
		//transform.Rotate(new Vector3(transform.rotation.x,-90,transform.rotation.z));
		else if (transform.eulerAngles.y < 180) {
			//mytext.text = "0 "+transform.rotation.y.ToString();
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x,180, transform.eulerAngles.z);
		}	//transform.Rotate(new Vector3(transform.rotation.x,-90,transform.rotation.z));
		else if (transform.eulerAngles.y < 270 ){
		//	mytext.text = "90 "+transform.rotation.y.ToString();
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x,270, transform.eulerAngles.z);
		}
		//transform.Rotate(new Vector3(transform.rotation.x,-90,transform.rotation.z));
		else {
			//mytext.text = "180 "+transform.rotation.y.ToString();
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, 0, transform.eulerAngles.z);
		}
		//else
		//	mytext.text = "wtf"+transform.rotation.y.ToString();
		//transform.Rotate(new Vector3(transform.rotation.x,-90,transform.rotation.z));
	}


	public void ver_start(int x)
	{
		yy = x;
		veri = true;
	}

	public void hor_start(int x)
	{
		xx = x;
		hori = true;
	}
	public void ver_end(int x)
	{
		yy = x;
		veri = false;
	}
	public void hor_end(int x)
	{
		xx = x;
		hori = false;
	}

	// Update is called once per frame
	void Update () {

		if (veri) {
			if(transform.position.y+yy*0.1f<8f && transform.position.y+yy*0.1f>0f) 
				transform.position = new Vector3 (transform.position.x,transform.position.y + yy*0.1f,transform.position.z);
		}
		if (hori) {
			transform.Rotate (new Vector3 (0, (float)xx * sensitivity * 10, 0));
		}

		if (htilt)
			transform.Rotate (new Vector3 (0, -Input.acceleration.x * sensitivity * Mathf.Abs (Input.acceleration.x) * 10, 0));
			
		if(vtilt){
			y = Input.acceleration.y * 2.0f + 1.2f;

			if (y > 0.5f)
				y = 0.5f;
			else if (y < -0.5f)
				y = -0.5f;
			//mytext.text = y.ToString ();
			if (transform.position.y + y / 5f < 8f && transform.position.y + y / 5f > 0f)
				transform.position = new Vector3 (transform.position.x, transform.position.y + y / 5, transform.position.z);
		}
		//mytext.text = transform.position.y.ToString () + " " + cam.transform.localPosition.z.ToString () + " " + (-2f * Mathf.Sqrt (16 - (transform.position.y - 5) * (transform.position.y - 5))).ToString ();

		if (transform.position.y > 5) {
			if(8 > (transform.position.y - 5))
				cam.transform.localPosition = new Vector3 (cam.transform.localPosition.x, cam.transform.localPosition.y, -2f * Mathf.Sqrt(16-(transform.position.y-5)*(transform.position.y-5)));
			else
				cam.transform.localPosition = new Vector3 (cam.transform.localPosition.x, cam.transform.localPosition.y, -2f * Mathf.Sqrt(16-(transform.position.y-5)*(transform.position.y-5)));

				
		}else
			cam.transform.localPosition =new Vector3(cam.transform.localPosition.x,cam.transform.localPosition.y,-8f);
		cam.LookAt (new Vector3 (5.5f, 3.4f+ (transform.position.y-1)/4/*(transform.position.y-1)*/, 5.5f));

	}
}
