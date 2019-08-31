using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mino_Board : MonoBehaviour {

//	public bool[][] ocupied = new bool[20][];
	public bool[,] ocupied = new bool[23,10];
	public bool spaceIsOpen;

	public Vector2[,] wallKickData = new Vector2[4,5];
	public Vector2[,] wallKickDataI = new Vector2[4,5];


	// Use this for initialization
	void Start () {
//		ocupied [18, 5] = true;
//		print (ocupied [0, 0]);
//		print (ocupied [18, 5]);
		wallKickData = new Vector2[,] {	
			{ new Vector2 (0,0), new Vector2 (-1,0), new Vector2 (-1,1) , new Vector2 (0,-2), new Vector2 (-1,-2) },
			{ new Vector2 (0,0), new Vector2 (1,0) , new Vector2 (1,-1) , new Vector2 (0,2) , new Vector2 (1,2)   },
			{ new Vector2 (0,0), new Vector2 (1,0) , new Vector2 (1,1)  , new Vector2 (0,-2), new Vector2 (1,-2)  },
			{ new Vector2 (0,0), new Vector2 (-1,0), new Vector2 (-1,-1), new Vector2 (0,2) , new Vector2 (-1,2)  }
		};
		wallKickDataI = new Vector2[,] {	
			{ new Vector2 (0,0), new Vector2 (-2,0), new Vector2 (1,0) , new Vector2 (-2,-1), new Vector2 (1,2)  },
			{ new Vector2 (0,0), new Vector2 (-1,0), new Vector2 (2,0) , new Vector2 (-1,2) , new Vector2 (2,-1) },
			{ new Vector2 (0,0), new Vector2 (2,0) , new Vector2 (-1,0), new Vector2 (2,1)  , new Vector2 (-1,-2)},
			{ new Vector2 (0,0), new Vector2 (1,0) , new Vector2 (-2,0), new Vector2 (1,-2) , new Vector2 (-2,1) }
		};
	}
	
	// Update is called once per frame
	void Update () {
//		print ("Count: " + count);

		//Lines weren't updating correctly.
//		checkLineClear();
	}

	public bool minoSpawned(int minoID, GameObject activeMino){
//		activeMino.GetComponent<MinoController>().enabled
//		ocupied[0,3] = true;
		return isCurrentSpaceOK(activeMino);
	}

	public bool isCurrentSpaceOK(GameObject activeMino){
		try{
			spaceIsOpen = true;
			for (int i = 0; i < activeMino.transform.childCount; i++) {
//				print("I: " + i);
				Vector2 tilePos = new Vector2 (activeMino.transform.GetChild (i).transform.position.x,activeMino.transform.GetChild (i).transform.position.y); 
	//			print (tilePos);
				spaceIsOpen &= !(ocupied [(int) Math.Round(tilePos.y),(int) Math.Round(tilePos.x)]);
				if((ocupied [(int) Math.Round(tilePos.y),(int) Math.Round(tilePos.x)])){
					//print((int) Math.Round(tilePos.y) + ", " + (int) Math.Round(tilePos.x));
				}
	//			print (ocupied [(int)tilePos.y, (int)tilePos.x]);

	//			print (ocupied [(int)tilePos.y, (int)tilePos.x] = true);
			}
//			print("SIO: " + spaceIsOpen);
			return spaceIsOpen;
		}catch{
//			print (e);
			return false;
		}
	}
	public void removeCurrentSpot(GameObject activeMino){
		for (int i = 0; i < activeMino.transform.childCount; i++) {
			Vector2 tilePos = new Vector2 (activeMino.transform.GetChild (i).transform.position.x, activeMino.transform.GetChild (i).transform.position.y); 
			ocupied [(int) Math.Round(tilePos.y),(int) Math.Round(tilePos.x)] = false;
		}
	}
	public void setCurrentSpot(GameObject activeMino){
//		print ("B4: " + ocupied [1, 4]);
		for (int i = 0; i < activeMino.transform.childCount; i++) {
			Vector2 tilePos = new Vector2 (activeMino.transform.GetChild (i).transform.position.x, activeMino.transform.GetChild (i).transform.position.y); 
			ocupied [(int) Math.Round(tilePos.y),(int) Math.Round(tilePos.x)] = true;
		}
//		print ("AFTR: " + ocupied [1, 4]);
	}

	public void rotate(int rotateFrom, int rotateTo, GameObject activeMino, bool rotateRight){
		//print ("RF: " + rotateFrom + ", RR: " + rotateRight);
		if (activeMino.GetComponent<MinoController> ().ID == 3) {
			return;
		}
		for (int i = 0; i < 5; i++) {
			/*
			if (i > 1) {
				print("That_One_Overlap_Spot: " + ocupied
			}*/
			if (rotateRight) {
				if (activeMino.gameObject.GetComponent<MinoController> ().ID == 0) {
					activeMino.transform.position += (Vector3)wallKickDataI [rotateFrom, i];
				} else {
					activeMino.transform.position += (Vector3)wallKickData [rotateFrom, i];
				}

				if (isCurrentSpaceOK (activeMino)) {
//					print (i);
//					activeMino.gameObject.GetComponent<MinoController>().rotationDirection = (activeMino.gameObject.GetComponent<MinoController>().rotationDirection + 1) % 4;
					return;
				} else {
					if (activeMino.gameObject.GetComponent<MinoController> ().ID == 0) {
						activeMino.transform.position -= (Vector3)wallKickDataI [rotateFrom, i];
					} else {
						activeMino.transform.position -= (Vector3)wallKickData [rotateFrom, i];
					}
				}
			} else {
				if (activeMino.gameObject.GetComponent<MinoController> ().ID == 0) {
					activeMino.transform.position -= (Vector3)wallKickDataI [rotateTo, i];//Rotate From??
				} else {
					activeMino.transform.position -= (Vector3)wallKickData [rotateTo, i];
				}
				//print ("Pos: " + activeMino.transform.position);
				if (isCurrentSpaceOK (activeMino)) {
//					activeMino.gameObject.GetComponent<MinoController>().rotationDirection = (activeMino.gameObject.GetComponent<MinoController>().rotationDirection + 3) % 4;
					return;
				} else {
					if (activeMino.gameObject.GetComponent<MinoController> ().ID == 0) {
						activeMino.transform.position += (Vector3)wallKickDataI [rotateTo, i];
					} else {
						activeMino.transform.position += (Vector3)wallKickData [rotateTo, i];
					}
				}
			}
		}
		///*
		print(":(");
//		printOcu ();
		if (rotateRight) {
			activeMino.transform.eulerAngles += new Vector3 (0, 0, 90);
		} else {
			activeMino.transform.eulerAngles += new Vector3 (0, 0, -90);
		}//*/

//		print (wallKickData [rotateFrom, 4]);
//		print(wallKickData [rotateFrom, 4] * -1);

//		print ("Rotate");
//		print (activeMino.GetComponent<MinoController> ().ID);
	}

	public void checkLineClear(){
		ArrayList linesCleared = new ArrayList ();
		for (int i = 0; i < 23; i++) {
			bool linecleared = true;
			for(int j = 0; j < 10; j++){
				linecleared &= ocupied [i, j];
			}
			if (linecleared) {
//				print ("Line: " + i + " cleared");
				linesCleared.Add (i);
			}
		}
		adjustOtherLines (linesCleared);
	}

	public void removeLine(int lineNum){
		if (lineNum >= 0) {
//			print (lineNum + " is Cleared");
			for (int i = 0; i < this.gameObject.transform.childCount; i++) {
				GameObject child = this.gameObject.transform.GetChild (i).gameObject;
				if (child.tag != "Border") {
					for (int j = 0; j < child.gameObject.transform.childCount; j++) {
						GameObject tile = child.gameObject.transform.GetChild (j).gameObject;
						if ((int)Math.Round (tile.transform.position.y) == lineNum) {
							Destroy (tile);
						}
					}
				}
			}
		}
	}

	public void adjustOtherLines(ArrayList linesCleared){
//		foreach (int i in linesCleared) {
		for(int c = 0; c < linesCleared.Count; c++){
			int i = (int)linesCleared [c];
//			print ("NEXT I WILL CLEAR: " + i);
			removeLine (i);
			for (int j = 0; j < 10; j++) {
				ocupied [i, j] = false;
			}
			for (int j = i+1; j < 23; j++) {
				for (int k = 0; k < 10; k++) {
					ocupied [j - 1, k] = ocupied [j, k];
				}
				moveLineDown (j);
			}
			///*
//			print ("Before");
//			printAR (linesCleared);
			linesCleared = decremenAROverValue (linesCleared, i);
//			print ("After");
//			printAR (linesCleared);
//			print (linesCleared [0]);
//			print ("LENGTH: " + linesCleared.Count);
			//*/
			/*
			int len = linesCleared.Count;
			for (int j = 0; j < len; j++) {
				if ((int)linesCleared[j] > i) {
					(int)linesCleared[j]--;
				}
			}*/
		}
	}

	public void moveLineDown(int lineNum){
		if (lineNum >= 0) {
			for (int i = 0; i < this.gameObject.transform.childCount; i++) {
				GameObject child = this.gameObject.transform.GetChild (i).gameObject;
				if (child.tag != "Border") {
					for (int j = 0; j < child.gameObject.transform.childCount; j++) {
						GameObject tile = child.gameObject.transform.GetChild (j).gameObject;
						if ((int)Math.Round (tile.transform.position.y) == lineNum) {
							tile.transform.position += new Vector3 (0, -1, 0);
						}
					}
				}
			}
		}
	}

	public ArrayList decremenAROverValue(ArrayList a, int val){
		ArrayList newAR = new ArrayList();
		foreach (int i in a) {
			if (i > val) {
				newAR.Add (i - 1);
			} else {
				newAR.Add (i);
			}
		}
		return newAR;
	}

	public void printAR(ArrayList a){
		String s = "";
		foreach (int i in a) {
			s += "" + i + ", ";
		}
		print (s);
	}

	public void printOcu(){
		for (int i = 0; i < 23; i++) {
			String s = "";
			for (int j = 0; j < 10; j++) {
				s += ocupied [i, j] + ", ";
			}
			print ("Array " + i + ":" + s);
		}
	}
}