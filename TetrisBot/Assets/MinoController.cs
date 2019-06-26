using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.D)) {
//			print ("D");
			this.gameObject.transform.eulerAngles += new Vector3 (0, 0, -90);
		}
		if (Input.GetKeyDown (KeyCode.A)) {
//			print ("A");
			this.gameObject.transform.eulerAngles += new Vector3 (0, 0, 90);
		}
	}
}
