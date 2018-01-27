using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    void OnGUI()
    {
        PlayerObject player = PlayerObject.instance;
        if (GUI.Button(new Rect(10, 70, 50, 30), "Start server"))
        {
            Debug.Log("Server start");

            SceneManager.LoadScene("Scene1");

            player.StartClient(true);
            
            DontDestroyOnLoad(player.gameObject);
        }

        if (GUI.Button(new Rect(50, 70, 50, 30), "Join"))
        {
            SceneManager.LoadScene("P2Scene");
            Debug.Log("Client start");
            player.StartClient(false);
            DontDestroyOnLoad(player.gameObject);
        }

        if (GUI.Button(new Rect(100, 70, 50, 30), "Join 2"))
        {
            SceneManager.LoadScene("P3Scene");
            Debug.Log("Client start");
            player.StartClient(false);
            DontDestroyOnLoad(player.gameObject);
        }

    }
}
