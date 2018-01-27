using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour, IValuesUser<bool> {
    public GameObject text;
    bool canOpenDoor = false;
    bool locked = true;

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
            if (locked)
            {
                text.GetComponent<Text>().text = "Door is locked. Maybe there's a switch somewhere...";
            }
            else
            {
                print("Opening door");
                if (SceneManager.GetActiveScene().name == "Scene1")
                {
                    SceneManager.LoadScene("Scene2");
                }
                else if (SceneManager.GetActiveScene().name == "Scene2")
                {
                    SceneManager.LoadScene("Scene1");
                }
            }
        }
	}

    public void OnValueChanged(string variableName, bool newValue)
    {
        Debug.Log("Received new value for " + variableName + ": " + newValue);
        if (variableName == "activated")
        {
            locked = !newValue;
        }
    }
}
