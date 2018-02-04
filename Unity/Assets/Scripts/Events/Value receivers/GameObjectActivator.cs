using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Active un GameObject quand une variable devient vraie.
/// </summary>
public class GameObjectActivator : MonoBehaviour, IValuesUser<bool> {

	[Tooltip("Object cible à activer")]
	public GameObject targetGameObject;

	[Tooltip("Indique si l'activation du gameObject cible est permanente, même si la variable source repasse à faux")]
	public bool activateOnceAndForAll = true;

	public void OnValueChanged(string variableName, bool newValue) {
		if (targetGameObject == null) {
			Debug.LogError ("Object cible à activer indéfini.");
			return;
		}

		if (newValue == true) {
			targetGameObject.SetActive (true);
		} else {
			if (!activateOnceAndForAll)
				targetGameObject.SetActive (false);
		}
	}
}
