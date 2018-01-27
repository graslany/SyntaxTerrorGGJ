﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	public Server variablesPrefab;

	public NetworkManager nwManager;


	void OnGUI() {
		if (GUI.Button (new Rect (10, 70, 50, 30), "Start server")) {
			//Debug.Log ("Server start");

			//nwManager.StartHost ();

			//GameObject vars = Instantiate(variablesPrefab.gameObject);
			//DontDestroyOnLoad (vars);
			//NetworkServer.Spawn (vars);

			//SceneManager.LoadScene ("Room 1");
		}

		if (GUI.Button (new Rect (50, 70, 50, 30), "Join")) {
			Debug.Log ("Client start");
			nwManager.StartClient ();
			SceneManager.LoadScene ("Room 2");
		}

	}
}
