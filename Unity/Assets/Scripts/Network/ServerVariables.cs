using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Reflection;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

public class Server
{
    private TcpListener server;
    private Thread thread;
    private Thread player1Thread;
    private Thread player2Thread;
    private Thread player3Thread;

    private TcpClient player1;
    private TcpClient player2;
    private TcpClient player3;

    public void Start()
    {
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        server = new TcpListener(localAddr, 9292);
        server.Start();

        thread = new Thread(AcceptThread);
        thread.Start();
        Thread.Sleep(1000);
    }

    public void Stop()
    {
        server.Stop();
        if (player1 != null)
        {
            player1.Close();
        }
        if (player2 != null)
        {
            player2.Close();
        }
        if (player3 != null)
        {
            player3.Close();
        }
    }

    private void AcceptThread()
    {
        while (true)
        {
            Socket socket = server.AcceptSocket();
            TcpClient client = server.AcceptTcpClient();
            if (player1 == null)
            {
                player1 = client;
                player1Thread = new Thread(Player1Thread);
                player1Thread.Start();
            } else if (player2 == null)
            {
                player2 = client;
                player2Thread = new Thread(Player2Thread);
                player2Thread.Start();
            }
            else if (player3 == null)
            {
                player3 = client;
                player3Thread = new Thread(Player3Thread);
                player3Thread.Start();
            } else
            {
                client.Close();
            }
        }
    }

    private void Player1Thread()
    {
        StreamReader reader = new StreamReader(player1.GetStream());
        while (true)
        {
            String line = reader.ReadLine();
            broadcast(line);
        }
    }

    private void Player2Thread()
    {
        StreamReader reader = new StreamReader(player2.GetStream());
        while (true)
        {
            String line = reader.ReadLine();
            broadcast(line);
        }
    }

    private void Player3Thread()
    {
        StreamReader reader = new StreamReader(player3.GetStream());
        while (true)
        {
            String line = reader.ReadLine();
            broadcast(line);
        }
    }

    private void broadcast(String line)
    {
        if (player1 != null)
        {
            StreamWriter p1writer = new StreamWriter(player1.GetStream());
            p1writer.WriteLine(line);
        }
        if (player2 != null) { 
            StreamWriter p2writer = new StreamWriter(player2.GetStream());
            p2writer.WriteLine(line);
        }
        if (player3 != null)
        {
            StreamWriter p3writer = new StreamWriter(player3.GetStream());
            p3writer.WriteLine(line);
        }
    }
}

