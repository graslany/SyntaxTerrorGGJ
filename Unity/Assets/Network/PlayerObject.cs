using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour
{
	public static PlayerObject Instance {
		get {
			if (instance == null) {
				PlayerObject foundInstance = FindObjectsOfType<PlayerObject>().Where(p => p.isLocalPlayer).FirstOrDefault();
				if (foundInstance != null && foundInstance.isServer)
					throw new InvalidOperationException("Il n'y a pas d'unicité de cette classe sur le serveur, accéder à un singleton n'a pas de sens.");
				instance = foundInstance;
			}
			return instance;
		}
	}
	public static PlayerObject instance;

	/// <summary>
	/// Scène que ce client doit charger.
	/// </summary>
	public SceneEnum? PlayerScene {
		get {
			if (playerSceneHasValue)
				return playerScene;
			else
				return null;
		}
		set {
			if (value.HasValue) {
				playerScene = value.Value;
				playerSceneHasValue = true;
			} else
				playerSceneHasValue = false;
		}
	}
	// Une SyncVar ne peut pas être autre chose qu'un type très basique.
	// Donc on doit stocker au format valeur + flag.
	[SyncVar] private SceneEnum playerScene;
	[SyncVar] private bool playerSceneHasValue;

    public void SignalVariableChangeToServer<T>(SimpleValueSource<T> source)
    {
        if (source == null || string.IsNullOrEmpty(source.Identifier))
            return;

        // En RPC, on ne peut pas passer n'importe quels paramètres, donc il faut ruser un peu
		CmdSignalVariableChange(ValueSourceSerialization.Serialize(source));
	}

	[Command]
	private void CmdSignalVariableChange(string serializedVariable)
	{
		ServerVariables.instance.RpcSignalVariableChange(serializedVariable);
	}

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		DontDestroyOnLoad (this);
		if (PlayerScene.HasValue)
			Scenes.LoadSceneByID (PlayerScene.Value);
	}
}
