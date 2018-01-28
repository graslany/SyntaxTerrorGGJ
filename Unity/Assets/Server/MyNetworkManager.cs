using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class MyNetworkManager : NetworkManager {

	public int nextClientId;

	public void PrepareLevelStart() {
		nextClientId = 1;
	}

	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId)
	{
		PlayerObject playerObject = GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<PlayerObject>();
		if (playerObject == null)
			throw new ArgumentException ("Prefab du playerObject invalide");

		switch (nextClientId) {
		case 1:
			playerObject.playerScene = SceneEnum.Level1Player1;
			break;
		case 2:
			playerObject.playerScene = SceneEnum.Level1Player2;
			break;
		case 3:
			playerObject.playerScene = SceneEnum.Level1Player3;
			break;
		default:
			Debug.LogError ("Trop de joueurs !");
			return;
		}
		nextClientId++;

		NetworkServer.AddPlayerForConnection(conn, playerObject.gameObject, playerControllerId);
	}
}
