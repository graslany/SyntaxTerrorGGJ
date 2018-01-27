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

    [Command]
    public void CmdSignalVariableChangeToServer<T>(SimpleValueSource<T> source)
    {
        ServerVariables.instance.SignalVariableChangeToServer(source);
    }
}
