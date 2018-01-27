using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour {
    public GameObject text;
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
            text.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
		if (canOpenDoor && Input.GetKeyDown(KeyCode.E))
        {
            text.SetActive(true);
            text.GetComponent<Text>().text = "Door is locked. Maybe there's a switch somewhere...";
            return;
            //print("Opening door");
            //if (SceneManager.GetActiveScene().name == "Scene1")
            //{
            //    SceneManager.LoadScene("Scene2");
            //} else if (SceneManager.GetActiveScene().name == "Scene2")
            //{
            //    SceneManager.LoadScene("Scene1");
            //}
        }
	}
}
