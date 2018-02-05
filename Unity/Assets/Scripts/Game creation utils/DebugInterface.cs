using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInterface : MonoBehaviour {

	public PlayerObject playerObjectPrefab;
	public GameObject[] networkManagerPrefabs;
	private string serverAddress = "localhost";
	private MyNetworkManager nwManager;

	protected virtual void OnGUI() {

		// Ne pas montrer cette interface en prod
		#if !DEVELOPMENT_BUILD
		if (!Application.isEditor) {
			Destroy(this);
			return;
		}
		#endif

		// Permettre de join un serveur si ce n'est pas encore fait.
		if (PlayerObject.Instance == null) {
			GUI.Label(new Rect(20, 30, 500, 20), "Il semble que le jeu ne soit pas connecté à un serveur");

			nwManager = (nwManager ?? FindObjectOfType<MyNetworkManager> ());
			if (nwManager == null && playerObjectPrefab != null) {
				nwManager = gameObject.AddComponent<MyNetworkManager> ();
				nwManager.playerPrefab = playerObjectPrefab.gameObject;
				if (networkManagerPrefabs != null) {
					nwManager.spawnPrefabs.AddRange (networkManagerPrefabs);
				}
			}
			if (nwManager != null) {
				GUI.Label (new Rect (20, 50, 120, 30), "Adresse du serveur : ");
				serverAddress = GUI.TextField (new Rect (140, 50, 200, 30), serverAddress);
				if (GUI.Button (new Rect (20, 90, 200, 30), "Connexion")) {
					nwManager.networkAddress = serverAddress;
					nwManager.StartClient ();
				}
			} else {
				GUI.Label (new Rect (20, 50, 500, 30), "Aucun PlayerObject ne peut être localisé ou créé.");
			}
		}
	}
}
