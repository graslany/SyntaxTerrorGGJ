using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Display : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Player1EnteredZone() {
		GetComponent<TextMesh> ().text = "Le joueur 1 est entré";
	}

	public void Player1ExitedZone() {
		GetComponent<TextMesh> ().text = "Le joueur 1 est sorti";
	}
}
