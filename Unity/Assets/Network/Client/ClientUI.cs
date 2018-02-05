using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ClientUI : MonoBehaviour {

	private NetworkManager nwManager;
	private string serverAddress = "localhost";

	protected virtual void Start() {
		nwManager = GetComponent<NetworkManager>();
	}

	protected virtual void OnGUI() {
		if (nwManager != null) {
			GUI.Label (new Rect (50, 50, 120, 30), "Adresse du serveur : ");
			serverAddress = GUI.TextField (new Rect (170, 50, 120, 30), serverAddress);
			if (GUI.Button (new Rect (50, 90, 200, 30), "Rejoindre une partie")) {
				Join ();
				Destroy (this);
			}
		} else {
			GUI.Label (new Rect (50, 50, 200, 30), "Aucun NetworkManager trouvé pour lancer le client");
		}
	}

	private void Join() {
		nwManager.networkAddress = serverAddress;
		nwManager.StartClient ();
	}
}
