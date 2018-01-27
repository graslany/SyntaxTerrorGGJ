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
	public void RpcSignalBoolVariableChangeToClient(string variableName, bool newValue) {
		SignalVariableChangeToClient(variableName, newValue);
	}

	[ClientRpc]
    public void RpcSignalIntVariableChangeToClient(string variableName, int newValue) {
		SignalVariableChangeToClient(variableName, newValue);
	}

	[ClientRpc]
    public void RpcSignalFloatVariableChangeToClient(string variableName, float newValue) {
		SignalVariableChangeToClient(variableName, newValue);
	}

	private void SignalVariableChangeToClient<T>(string variableName, T newValue) {
		if (string.IsNullOrEmpty(variableName))
			return;
		ValueReceivers.Instance.SendValueToReceivers (variableName, newValue);
	}
}

