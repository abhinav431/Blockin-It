using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling_blocks : MonoBehaviour {

	public GameObject[] shapes;
	public Vector3 pos;
	GameObject gb,current;
	public float tym=0, oldtym=0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		tym += Time.deltaTime;
		if (tym >= 0.3) {
			oldtym = tym;
			pos = new Vector3 (Random.Range (-7, 7), 20, Random.Range (3, 7));
			gb = shapes [Random.Range (0, 5)];

			current = Instantiate (gb, pos, Quaternion.identity) as GameObject;//////////////////////////////////////////////instancing
			current.transform.parent = transform;
			tym = 0;
		}
		foreach(Transform array in transform)
			if (array.position.y < 0) {
				Destroy (array.gameObject);
			}
	}
}
