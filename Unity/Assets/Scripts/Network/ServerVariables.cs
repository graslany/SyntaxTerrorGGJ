using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Reflection;

/// <summary>
/// Découple les soruces de valeurs de leurs envois au serveur
/// </summary>
public class ServerVariables : NetworkBehaviour
{
	protected virtual void Awake() {
        instance = this;
	}

    public static ServerVariables instance;

	[ClientRpc]
	public void RpcSignalVariableChange(string serializedVariable)
	{
		// Désérialisation
		IEventSource deserializedVariable = null;
		try {
			deserializedVariable = ValueSourceSerialization.Deserialize(serializedVariable);
			SimpleValueSource<bool> boolVar = (deserializedVariable as SimpleValueSource<bool>);
			SimpleValueSource<int> intVar = (deserializedVariable as SimpleValueSource<int>);
			SimpleValueSource<float> floatVar = (deserializedVariable as SimpleValueSource<float>);

			if (boolVar != null) {
				SignalVariableChangeToClient(boolVar);
			} else if (intVar != null) {
				SignalVariableChangeToClient(intVar);
			} else if (floatVar != null) {
				SignalVariableChangeToClient(floatVar);
			} else {
				throw new ArgumentException("Type de données non supporté");
			}
		}
		catch(Exception e) {
			Debug.LogError(e.ToString());
		}
	}

	private void SignalVariableChangeToClient<T>(SimpleValueSource<T> variable) {
		if (variable == null)
			return;
		ValueReceivers.Instance.SendValueToReceivers (variable.Identifier, variable.StoredValue);
	}
}

