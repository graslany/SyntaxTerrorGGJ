using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : MonoBehaviour
{
    public static PlayerObject instance;

    private Queue<String> queue = new Queue<string>();

    private void Update()
    {
        List<String> toSend = new List<string>();
        lock(queue)
        {
            foreach (String elem in queue) {
                toSend.Add(elem);
            }
            queue.Clear();
        }
        foreach (String elem in toSend)
        {
            String[] splitted = elem.Split(':');
            String type = splitted[1];
            switch (type)
            {
                case "b":
                    ValueReceivers.Instance.SendValueToReceivers(splitted[0], bool.Parse(splitted[2]));
                    break;
                case "i":
                    ValueReceivers.Instance.SendValueToReceivers(splitted[0], int.Parse(splitted[2]));
                    break;
                case "f":
                    ValueReceivers.Instance.SendValueToReceivers(splitted[0], float.Parse(splitted[2]));
                    break;
            }
        }
    }

    private Thread thread;
    private TcpClient client;
    private StreamReader reader;
    private Server server;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StartClient(bool isServer)
    {
        if (isServer)
        {
            server = new Server();
            server.Start();
        }

        client = new TcpClient();
        client.Connect("127.0.0.1", 9292);
        reader = new StreamReader(client.GetStream());
        thread = new Thread(Client);
        thread.Start();
    }

    private void OnDestroy()
    {
        client.Close();
        if (server != null)
        {
            server.Stop();
        }
    }


    private void Client()
    {
        while (true)
        {
            String line = reader.ReadLine();
            lock (queue)
            {
                queue.Enqueue(line);
            }
        }
    }

    public void SendBoolTrigger(String variableName, bool value)
    {
        Stream stream = client.GetStream();
        StreamWriter writer = new StreamWriter(stream);
        writer.WriteLine(variableName + ":b:" + value);
    }

    public void SendIntTrigger(String variableName, int value)
    {
        Stream stream = client.GetStream();
        StreamWriter writer = new StreamWriter(stream);
        writer.WriteLine(variableName + ":i:" + value);
    }

    public void SendFloatTrigger(String variableName, float value)
    {
        Stream stream = client.GetStream();
        StreamWriter writer = new StreamWriter(stream);
        writer.WriteLine(variableName + ":f:" + value);
    }
    
    public void CmdSignalVariableChangeToServer<T>(SimpleValueSource<T> source)
    {
        if (source == null || string.IsNullOrEmpty(source.Identifier))
            return;

        // En RPC, on ne peut pas passer n'importe quels pramètres, donc il faut ruser un peu
        Type vType = typeof(T);
        if (vType == typeof(bool))
            SendBoolTrigger(source.Identifier, (bool)(object)source.StoredValue);
        else if (vType == typeof(int))
            SendIntTrigger(source.Identifier, (int)(object)source.StoredValue);
        else if (vType == typeof(float))
            SendFloatTrigger(source.Identifier, (float)(object)source.StoredValue);
        else
            Debug.LogError("Type de données non supporté : " + vType.ToString());
    }
}
