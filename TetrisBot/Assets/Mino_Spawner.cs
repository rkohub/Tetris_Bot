using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mino_Spawner : MonoBehaviour {

	public bool minoIsSpawned;
	public GameObject[] minos;
	int nextMino;
	public GameObject minoBoard;
	public GameObject activeMino;
	public bool gameOver;
	bool[] spawnedMinos =  new bool[7];
	int[] nextMinoes = new int[7];

	// Use this for initialization
	void Start () {
//		minoIsSpawned = false;
//		Instantiate(minos[0], minos[0].transform.position, Quaternion.identity);=
		minoIsSpawned = false;
		gameOver = false;
		for (int i = 1; i < 7; i++) {
			nextMino = (int)UnityEngine.Random.Range (0, 7);
			while (spawnedMinos [nextMino]) {
				nextMino = (int)UnityEngine.Random.Range (0, 7);
			}
			nextMinoes [i] = nextMino;
			spawnedMinos [nextMino] = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameOver) {
			if (!minoIsSpawned) {
//				minoBoard.GetComponent<Mino_Board> ().checkLineClear ();
//				printAR (nextMinoes);
				nextMino = determineNextMino ();
//				printAR (nextMinoes);

				activeMino = Instantiate (minos [nextMino], minos [nextMino].transform.position, Quaternion.identity, minoBoard.transform);
				bool okToSpawn = minoBoard.GetComponent<Mino_Board> ().minoSpawned (nextMino, activeMino);
//				print ("Good To Spawn?: " + okToSpawn);
				minoIsSpawned = okToSpawn;
				if (!okToSpawn) {
					print ("Game Over!");
					gameOver = true;
				} else {
					activeMino.GetComponent<MinoController> ().minoSpawner = this.gameObject;
				}

				// else {
//					minoBoard.GetComponent<Mino_Board> ().removeCurrentSpot (activeMino);
//				}
			} 
			if (Input.GetKeyDown (KeyCode.N)) {
				minoIsSpawned = false;
			}
		}
	}
	public void setTile(){
		minoBoard.GetComponent<Mino_Board> ().setCurrentSpot (activeMino);
		minoBoard.GetComponent<Mino_Board> ().checkLineClear ();
		activeMino.GetComponent<MinoController> ().enabled = false;
		minoIsSpawned = false;
	}

	public int determineNextMino(){
		nextMinoes = shiftArray (nextMinoes);
		bool allSpawned = true;
		for (int i = 0; i < spawnedMinos.Length; i++) {
			allSpawned &= spawnedMinos [i];
		}
		if (allSpawned) {
			spawnedMinos =  new bool[7];
		}
		nextMino = (int)UnityEngine.Random.Range (0, 7);
		while (spawnedMinos [nextMino]) {
			nextMino = (int)UnityEngine.Random.Range (0, 7);
		}
		spawnedMinos [nextMino] = true;
		nextMinoes [nextMinoes.Length - 1] = nextMino;

		displayNextMino ();

		return nextMinoes[0];
	}

	public int[] shiftArray(int[] a){
		int[] newArr = new int[a.Length];
		for (int i = 1; i < a.Length; i++) {
			newArr [i-1] = a [i];
		}
		newArr [newArr.Length - 1] = a [0];
		return newArr;
	}

	public void printAR(int[] a){
		String s = "";
		foreach (int i in a) {
			s += "" + i + ", ";
		}
		print (s);
	}

	public void displayNextMino(){
		for (int i = 0; i < this.gameObject.transform.childCount; i++) {
			GameObject child = this.gameObject.transform.GetChild (i).gameObject;
			if (child.transform.childCount > 0) {
//				print ("CC: " + child.transform.childCount);
				for (int j = child.transform.childCount-1; j >= 0; j--) {
					Destroy (child.transform.GetChild (j).gameObject);
				}
			}

		}

		for (int i = 0; i < this.gameObject.transform.childCount; i++) {
			GameObject child = this.gameObject.transform.GetChild (i).gameObject;
			GameObject selectMino = minos [nextMinoes [i + 1]];
			selectMino = Instantiate (selectMino, child.transform.position, Quaternion.identity,child.transform);
			MinoController m = selectMino.GetComponent<MinoController> ();
			Destroy (m);
		}
	}
}
