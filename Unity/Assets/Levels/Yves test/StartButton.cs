using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	public NetworkManager nwManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI() {
		if (GUI.Button (new Rect (10, 70, 50, 30), "Start server")) {
			Debug.Log ("Server start");

			nwManager.StartHost ();
			SceneManager.LoadScene ("Room 1");
		}

		if (GUI.Button (new Rect (50, 70, 50, 30), "Join")) {
			Debug.Log ("Client start");
			nwManager.StartClient ();
			SceneManager.LoadScene ("Room 2");
		}

	}
}
