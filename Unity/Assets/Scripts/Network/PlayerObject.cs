using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour
{
    private void Awake()
    {
        instance = this;
    }

    public static PlayerObject instance;
    
    public void CmdSignalVariableChangeToServer<T>(SimpleValueSource<T> source)
    {
        if (source == null || string.IsNullOrEmpty(source.Identifier))
            return;

        // En RPC, on ne peut pas passer n'importe quels pramètres, donc il faut ruser un peu
        Type vType = typeof(T);
        if (vType == typeof(bool))
            CmdSignalBoolVariableChangeToServer(source.Identifier, (bool)(object)source.StoredValue);
        else if (vType == typeof(int))
            CmdSignalIntVariableChangeToServer(source.Identifier, (int)(object)source.StoredValue);
        else if (vType == typeof(float))
            CmdSignalFloatVariableChangeToServer(source.Identifier, (float)(object)source.StoredValue);
        else
            Debug.LogError("Type de données non supporté : " + vType.ToString());
    }

    [Command]
    private void CmdSignalBoolVariableChangeToServer(string variableName, bool newValue)
    {
        ServerVariables.instance.RpcSignalBoolVariableChangeToClient(variableName, newValue);
    }

    [Command]
    private void CmdSignalIntVariableChangeToServer(string variableName, int newValue)
    {
        ServerVariables.instance.RpcSignalIntVariableChangeToClient(variableName, newValue);
    }

    [Command]
    private void CmdSignalFloatVariableChangeToServer(string variableName, float newValue)
    {
        ServerVariables.instance.RpcSignalFloatVariableChangeToClient(variableName, newValue);
    }
}
