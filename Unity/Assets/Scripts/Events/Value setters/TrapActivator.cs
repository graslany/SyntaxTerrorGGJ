using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Convertit une variable booléenne en valeur d'activation pour un bouton
/// </summary>
[RequireComponent(typeof(BooleanValueReceiver))]
public class TrapActivator : MonoBehaviour, IValuesUser<bool> {

	public enum RaisingEdgeBehaviour
	{
		DoNothing,
		ArmIfDisabled,
		Spring
	}

	public enum FallingEdgeBehaviour
	{
		DoNothing,
		RearmIfSprung,
		Disable
	}

	[Tooltip("Piège dont l'état doit être fixé par ce script")]
	public TrapBase targetTrap;

	[Tooltip("Indique quoi faire quand la valeur source passe à vrai.")]
	public RaisingEdgeBehaviour activationBehviour = RaisingEdgeBehaviour.ArmIfDisabled;

	[Tooltip("Indique quoi faire quand la valeur source passe à faux.")]
	public FallingEdgeBehaviour deactivationBehviour = FallingEdgeBehaviour.Disable;

	private bool displayedError = false;

	public void OnValueChanged(string variableName, bool newValue) {
		if (targetTrap != null) {
			if (newValue) {
				switch (activationBehviour) {
				case RaisingEdgeBehaviour.ArmIfDisabled:
					if (targetTrap.State == TrapState.Inactive)
						targetTrap.Arm ();
					break;
				case RaisingEdgeBehaviour.Spring:
					if (targetTrap.State != TrapState.Triggered)
						targetTrap.Spring ();
					break;
				}
			} else {
				switch (deactivationBehviour) {
				case FallingEdgeBehaviour.Disable:
					if (targetTrap.State != TrapState.Inactive)
						targetTrap.Disarm ();
					break;
				case FallingEdgeBehaviour.RearmIfSprung:
					if (targetTrap.State == TrapState.Triggered)
						targetTrap.Arm ();
					break;
				}
			}
		} else if (!displayedError) {
			displayedError = true;
			Debug.LogError ("Impossible d'affecter l'état du bouton : celui-ci n'est pas renseigné.");
		}
	}

}
