using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour, IValuesUser<bool> {

	[Tooltip("Scène vers laquelle voyage la porte")]
	public SceneEnum destinationScene;

	[Tooltip("Texte affiché au-dessus de a porte quand elle est verrouillée")]
	public Canvas doorText = null;

	private bool locked = true;

	public float textDisplayDuration = 4;
	private float? textDisplayTime = null;

    private bool canOpenDoor = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagNames.Player)
        {
            canOpenDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
		if (other.gameObject.tag == TagNames.Player)
        {
            canOpenDoor = false;
			if (doorText != null)
				doorText.enabled = false;
        }
    }

    // Update is called once per frame
    void Update () {
		if (canOpenDoor && Input.GetKeyDown(KeyCode.E))
        {
            if (locked)
			{
				doorText.enabled = true;
				textDisplayTime = Time.time;
            }
            else
            {
				Scenes.LoadSceneByID (destinationScene);
            }

			// Texte explicatif
			if (doorText != null && textDisplayTime.HasValue) {
				if (Time.time - textDisplayTime.Value > textDisplayDuration) {
					textDisplayTime = null;
					doorText.enabled = false;
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
