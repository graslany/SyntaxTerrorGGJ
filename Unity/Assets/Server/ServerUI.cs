using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ServerUI : MonoBehaviour {

	public ServerVariables variablesPrefab;
	private ServerVariables variablesInstance;

	public MyNetworkManager nwManager;

	private bool isRunning;

	void OnGUI() {
		if (variablesPrefab != null) {
			string switchCommandName = (isRunning ? "Arrêter le serveur" : "Démarrer le serveur");
			if (GUI.Button (new Rect (50, 50, 200, 30), switchCommandName)) {
				if (isRunning)
					StopServer ();
				else
					StartServer ();
			}
		} else {
			GUI.Label (new Rect (50, 50, 400, 30), "Le prefab des variables n'est pas spécifié !");
		}
	}

	private void StartServer() {
		if (variablesPrefab != null) {
			nwManager.PrepareLevelStart ();
			nwManager.StartServer ();
			variablesInstance = Instantiate(variablesPrefab.gameObject).GetComponent<ServerVariables>();
			NetworkServer.Spawn (variablesInstance.gameObject);
			isRunning = true;
		}
	}

	private void StopServer() {
		if (variablesInstance != null) {
			NetworkServer.Destroy (variablesInstance.gameObject);
			variablesInstance = null;
		}
		nwManager.StopServer ();
		isRunning = false;
	}
}
