using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoController : MonoBehaviour {

	public GameObject minoSpawner;
	public int rotationDirection;
	private int oldNum;
	public int ID;
	public float timeToFall = 1.0f;
	public float timeTillFall;
	public float timeForFullMove;// = 0.25f;
	public float timeHealdKeyDown;
	public bool moveRight;
	public GameObject dropClone;

	// Use this for initialization
	void Start () {
		rotationDirection = 0;
		for (int i = 0; i < this.gameObject.transform.parent.gameObject.transform.childCount; i++) {
			//print (this.gameObject.transform.parent.gameObject.transform.GetChild (i));
			if (this.gameObject.transform.parent.gameObject.transform.GetChild (i).gameObject.tag == "DropLocation") {
				dropClone = this.gameObject.transform.parent.gameObject.transform.GetChild (i).gameObject;
			}
		}
		updateGhost ();
	}
	
	// Update is called once per frame
	void Update () {
		timeTillFall += Time.deltaTime;
		///*
		if (timeToFall < timeTillFall) {
			this.gameObject.transform.position += new Vector3 (0, -1, 0);
			if (!validLocation ()) {
				this.gameObject.transform.position += new Vector3 (0, 1, 0);
				print ("Lock?");
			}
			timeTillFall = 0;
		}
		//*/
		if (Input.GetKeyDown (KeyCode.J)) {
			this.gameObject.transform.eulerAngles += new Vector3 (0, 0, 90);
			//Add 3 here because -1 % 4 doesn't give 3.
			oldNum = rotationDirection;
			rotationDirection = (rotationDirection + 3) % 4;
			this.gameObject.GetComponentInParent<Mino_Board> ().rotate (oldNum, rotationDirection, this.gameObject, false);
			updateGhost ();
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			this.gameObject.transform.eulerAngles += new Vector3 (0, 0, -90);
			oldNum = rotationDirection;
			rotationDirection = (rotationDirection + 1) % 4;
			this.gameObject.GetComponentInParent<Mino_Board> ().rotate (oldNum, rotationDirection, this.gameObject, true);
			updateGhost ();
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			timeHealdKeyDown = 0;
			moveRight = true;
			this.gameObject.transform.position += new Vector3 (1, 0, 0);
			if (!validLocation ()) {
				this.gameObject.transform.position += new Vector3 (-1, 0, 0);
			}
			updateGhost ();
		}
		if (Input.GetKey (KeyCode.D) && moveRight) {
			timeHealdKeyDown += Time.deltaTime;
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			moveRight = false;
			timeHealdKeyDown = 0;
			this.gameObject.transform.position += new Vector3 (-1, 0, 0);
			if (!validLocation ()) {
				this.gameObject.transform.position += new Vector3 (1, 0, 0);
			}
			updateGhost ();
		}
		if (Input.GetKey (KeyCode.A) && !moveRight) {
			timeHealdKeyDown += Time.deltaTime;
		}
		///*
		if (timeHealdKeyDown > timeForFullMove) {
			if (moveRight) {
				while (true) {
					this.gameObject.transform.position += new Vector3 (1, 0, 0);
					if (!validLocation ()) {
						this.gameObject.transform.position += new Vector3 (-1, 0, 0);
						timeHealdKeyDown = 0;
						updateGhost ();
						break;
					}
				}
			} else {
				while (true) {
					this.gameObject.transform.position += new Vector3 (-1, 0, 0);
					if (!validLocation ()) {
						this.gameObject.transform.position += new Vector3 (1, 0, 0);
						timeHealdKeyDown = 0;
						updateGhost ();
						break;
					}
				}
			}
		}
		//*/
		if (Input.GetKeyDown (KeyCode.S)) {
			this.gameObject.transform.position += new Vector3 (0, -1, 0);
			if (!validLocation ()) {
				this.gameObject.transform.position += new Vector3 (0, 1, 0);
			}
		}
		if (Input.GetKeyDown (KeyCode.Y)) {
			minoSpawner.GetComponent<Mino_Spawner> ().minoIsSpawned = false;
			Destroy (this.gameObject);
		}
		if (Input.GetKeyDown (KeyCode.W)) {
			bool movedOnce = false;
			while(validLocation()){
				movedOnce = true;
//				this.gameObject.transform.GetComponentInParent<Mino_Board> ().setCurrentSpot (this.gameObject);
				this.gameObject.transform.position += new Vector3 (0, -1, 0);
			}
			if (movedOnce) {
				this.gameObject.transform.position += new Vector3 (0, 1, 0);
			}
//			print ("Moved Down!");
			minoSpawner.GetComponent<Mino_Spawner>().setTile ();
//			this.gameObject.transform.GetComponentInParent<Mino_Board> ().setCurrentSpot (this.gameObject);
		}
			
	}

	public bool validLocation(){
		return this.gameObject.transform.GetComponentInParent<Mino_Board> ().isCurrentSpaceOK (this.gameObject);
	}

	public void updateGhost(){
		for (int i = dropClone.gameObject.transform.childCount-1; i >= 0; i--) {
			Destroy(dropClone.gameObject.transform.GetChild(i).gameObject);
		}
		GameObject ghost = Instantiate(this.gameObject, this.gameObject.transform.position, this.gameObject.transform.rotation, dropClone.transform);
		MinoController m = ghost.GetComponent<MinoController> ();
		Destroy (m);
		while (this.gameObject.transform.GetComponentInParent<Mino_Board> ().isCurrentSpaceOK (ghost)) {
			ghost.gameObject.transform.position += new Vector3(0, -1, 0);
		}
		ghost.gameObject.transform.position += new Vector3(0, 1, 0);
	}
}
