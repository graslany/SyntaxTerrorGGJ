using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	public ServerVariables variablesPrefab;

	public NetworkManager nwManager;


	void OnGUI() {
		bool choseOption = false;
		
		if (GUI.Button (new Rect (10, 70, 100, 30), "Start server")) {
			Debug.Log ("Server start");

			nwManager.StartHost ();

			GameObject vars = Instantiate(variablesPrefab.gameObject);
			DontDestroyOnLoad (vars);
			NetworkServer.Spawn (vars);

			SceneManager.LoadScene ("Room 1");
			choseOption = true;
		}

		if (GUI.Button (new Rect (10, 110, 100, 30), "Join")) {
			Debug.Log ("Client start");
			nwManager.StartClient ();
			SceneManager.LoadScene ("Room 2");
			choseOption = true;
		}

		if (choseOption) {
			Destroy (this);
		}
	}
}
