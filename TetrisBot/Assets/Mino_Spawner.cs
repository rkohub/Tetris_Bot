using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino_Spawner : MonoBehaviour {

	public bool minoIsSpawned;
	public GameObject[] minos;

	// Use this for initialization
	void Start () {
//		minoIsSpawned = false;
		Instantiate(minos[0], minos[0].transform.position, Quaternion.identity);
		minoIsSpawned = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
