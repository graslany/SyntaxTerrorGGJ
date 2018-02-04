using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Convertit une variable booléenne en valeur d'activation pour un bouton
/// </summary>
[RequireComponent(typeof(BooleanValueReceiver))]
public class ButtonActivator : MonoBehaviour, IValuesUser<bool> {

	[Tooltip("Bouton dont l'état doit être fixé par ce script")]
	public AbstractButton targetButton;

	[Tooltip("Valeur d'activation du bouton quand la variable source vaut faux")]
	public ButtonMode falseActivationValue = ButtonMode.DoesNothing;

	[Tooltip("Valeur d'activation du bouton quand la variable source vaut vrai")]
	public ButtonMode trueActivationValue = ButtonMode.ActiveWhenPressed;

	private bool displayedError = false;

	public void OnValueChanged(string variableName, bool newValue) {
		if (targetButton != null) {
			targetButton.Mode = (newValue ? trueActivationValue : falseActivationValue);
		} else if (!displayedError) {
			displayedError = true;
			Debug.LogError ("Impossible d'affecter l'état du bouton : celui-ci n'est pas renseigné.");
		}
	}

}
