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
		ValueSourcesSender.SetRemoteVariablesContainer (this);
	}

	/// <summary>
	/// Indique au serveur qu'une valeur a changé dans une variable
	/// </summary>
	public void SignalVariableChangeToServer<T>(SimpleValueSource<T> source) {
		if (source == null || string.IsNullOrEmpty(source.Identifier))
			return;

		// En RPC, on ne peut pas passer n'importe quels pramètres, donc il faut ruser un peu
		Type vType = typeof(T);
		if (vType == typeof(bool))
			CmdSignalBoolVariableChangeToServer (source.Identifier, (bool) (object) source.StoredValue);
		else if (vType == typeof(int))
			CmdSignalIntVariableChangeToServer (source.Identifier, (int) (object) source.StoredValue);
		else if (vType == typeof(float))
			CmdSignalFloatVariableChangeToServer (source.Identifier, (float) (object) source.StoredValue);
		else
			Debug.LogError("Type de données non supporté : " + vType.ToString());
	}

	[Command]
	private void CmdSignalBoolVariableChangeToServer(string variableName, bool newValue) {
		RpcSignalBoolVariableChangeToClient (variableName, newValue);
	}

	[Command]
	private void CmdSignalIntVariableChangeToServer(string variableName, int newValue) {
		RpcSignalIntVariableChangeToClient (variableName, newValue);
	}

	[Command]
	private void CmdSignalFloatVariableChangeToServer(string variableName, float newValue) {
		RpcSignalFloatVariableChangeToClient (variableName, newValue);
	}

	[ClientRpc]
	private void RpcSignalBoolVariableChangeToClient(string variableName, bool newValue) {
		SignalVariableChangeToClient(variableName, newValue);
	}

	[ClientRpc]
	private void RpcSignalIntVariableChangeToClient(string variableName, int newValue) {
		SignalVariableChangeToClient(variableName, newValue);
	}

	[ClientRpc]
	private void RpcSignalFloatVariableChangeToClient(string variableName, float newValue) {
		SignalVariableChangeToClient(variableName, newValue);
	}

	private void SignalVariableChangeToClient<T>(string variableName, T newValue) {
		if (string.IsNullOrEmpty(variableName))
			return;
		ValueReceivers.Instance.SendValueToReceivers (variableName, newValue);
	}
}

