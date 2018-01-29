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

	protected virtual void Start() {
		nwManager = GetComponent<MyNetworkManager>();
	}

	protected virtual void OnGUI() {
		if (nwManager == null) {
			GUI.Label (new Rect (50, 50, 400, 30), "Aucun NetworkManager configuré, impossible de lancer le serveur.");
			return;
		}
		if (variablesPrefab == null) {
			GUI.Label (new Rect (50, 50, 400, 30), "Le prefab des variables n'est pas spécifié !");
			return;
		}

		if (isRunning)
			OnRunningServerGUI ();
		else
			OnIdleServerGUI ();
	}

	private void OnRunningServerGUI() {
		// Arrêt du serveur
		if (GUI.Button (new Rect (50, 50, 200, 30), "Arrêter le serveur")) {
			if (isRunning)
				StopServer ();
			else
				StartServer ();
		}
	}

	private void OnIdleServerGUI() {
		// Lancement du serveur
		if (GUI.Button (new Rect (50, 50, 200, 30), "Démarrer le serveur")) {
			if (isRunning)
				StopServer ();
			else
				StartServer ();
		}

		// Configuration
		nwManager.AssignSceneToLoad = !GUI.Toggle(new Rect (50, 90, 200, 30), !nwManager.AssignSceneToLoad, "Debug : ne pas faire changer le client de scène");		
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
