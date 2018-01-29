using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ClientUI : MonoBehaviour {

	private NetworkManager nwManager;

	protected virtual void Start() {
		nwManager = GetComponent<NetworkManager>();
	}

	protected virtual void OnGUI() {
		if (nwManager != null) {
			if (GUI.Button (new Rect (50, 50, 200, 30), "Rejoindre une partie")) {
				Join ();
				Destroy (this);
			}
		} else {
			GUI.Label (new Rect (50, 50, 200, 30), "Aucun NetworkManager trouvé pour lancr le client");
		}
	}

	private void Join() {
		nwManager.StartClient ();
	}
}
