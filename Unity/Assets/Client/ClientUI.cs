using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ClientUI : MonoBehaviour {

	public NetworkManager nwManager;

	private bool isRunning;

	void OnGUI() {
		if (GUI.Button (new Rect (50, 50, 200, 30), "Rejoindre une partie")) {
			Join ();
			Destroy (this);
		}
	}

	private void Join() {
		nwManager.StartClient ();
	}
}
