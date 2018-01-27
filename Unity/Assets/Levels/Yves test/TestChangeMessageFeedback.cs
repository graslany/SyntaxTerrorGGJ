using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChangeMessageFeedback : MonoBehaviour, IValuesUser<bool> {

	public void OnValueChanged(string variableName, bool newValue) {
		if (newValue)
			GetComponent<TextMesh> ().text = "Le joueur 1 est entré";
		else
			GetComponent<TextMesh> ().text = "Le joueur 1 est sorti";
	}
}
