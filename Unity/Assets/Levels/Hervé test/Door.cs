using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    bool canOpenDoor = false;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canOpenDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canOpenDoor = false;
        }
    }

    // Update is called once per frame
    void Update () {
		if (canOpenDoor && Input.GetKeyDown(KeyCode.E))
        {
            print("Opening door");
            if (SceneManager.GetActiveScene().name == "Scene1")
            {
                SceneManager.LoadScene("Scene2");
            } else if (SceneManager.GetActiveScene().name == "Scene2")
            {
                SceneManager.LoadScene("Scene1");
            }
        }
	}
}
