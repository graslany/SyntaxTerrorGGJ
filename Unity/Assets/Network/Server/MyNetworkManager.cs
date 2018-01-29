using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class MyNetworkManager : NetworkManager {

	public int nextClientId;

	// Etat de l'interface utilisateur
	public bool AssignSceneToLoad {
		get { return assignSceneToLoad; }
		set { assignSceneToLoad = value; }
	}
	private bool assignSceneToLoad = true;

	public void PrepareLevelStart() {
		nextClientId = 1;
	}

	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId)
	{
		PlayerObject playerObject = GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<PlayerObject>();
		if (playerObject == null)
			throw new ArgumentException ("Prefab du playerObject invalide");

		if (AssignSceneToLoad) {
			switch (nextClientId) {
			case 1:
				playerObject.PlayerScene = SceneEnum.Level1Player1;
				break;
			case 2:
				playerObject.PlayerScene = SceneEnum.Level1Player2;
				break;
			case 3:
				playerObject.PlayerScene = SceneEnum.Level1Player3;
				break;
			default:
				playerObject.PlayerScene = null;
				Debug.LogError ("Trop de joueurs !");
				return;
			}
			nextClientId++;
		} else
			playerObject.PlayerScene = null;

		NetworkServer.AddPlayerForConnection(conn, playerObject.gameObject, playerControllerId);
	}
}
