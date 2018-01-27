using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Detector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected virtual void OnTriggerEnter() {
		PlayerObject player = GameObject.FindObjectOfType<PlayerObject> ();
		player.OnSecene1ColliderEnter ();
		Debug.Log ("Detector in");
	}

	protected virtual void OnTriggerExit() {
		PlayerObject player = GameObject.FindObjectOfType<PlayerObject> ();
		player.OnSecene1ColliderExit ();
		Debug.Log ("Detector out");
	}
}
