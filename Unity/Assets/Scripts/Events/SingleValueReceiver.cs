using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Classe qui réceptionne la valeur d'une variable du jeu.
/// </summary>
public abstract class SingleValueReceiver : MonoBehaviour, ISingleValueReceiver {

	[Tooltip("Nom de la source qu'écoute cet objet")]
	[SerializeField]
	private string sourceName = null;
	public string SourceName {
		get {
			return sourceName;
		}
	}
}

/// <summary>
/// Classe qui réceptionne la valeur d'une variable du jeu.
/// </summary>
public abstract class SingleValueReceiver<T> : SingleValueReceiver, ISingleValueReceiver<T> {

	/// <summary>
	/// Dernière valeur reçue par cet objet
	/// </summary>
	public T LastValue {
		get {
			return lastValue;
		}
	}
	private T lastValue;

	/// <summary>
	/// Indique si cet objet a reçu au moins une fois une valeur
	/// </summary>
	public bool ReceivedValueOnce {
		get {
			return receivedValueOnce;
		}
	}
	private bool receivedValueOnce;

	public void ReceiveNewValue(T newValue) {
		// Setter-like
		if (Equals(lastValue, newValue)) {
			return;
		}
		lastValue = newValue;

		// La hiérarchie est parcourue vers le haut jusqu'à trouver un récepteur pour notre valeur.
		Transform currentTarget = gameObject;
		while (currentTarget != null) {

			// Recherche de tous les utilisateurs qui peuvent vouloir notre nouvelle valeur
			IValuesUser<T>[] foundUsers = GetComponents<IValuesUser<T>>();
			if (foundUsers.Length > 0) {
				foreach (IValuesUser<T> user in foundUsers) {
					try {
						user.OnValueChanged(this);
					}
					catch (Exception e) {
						Debug.LogError ("Erreur lors de la transmission d'une valeur : \n" + e.ToString());
					}
				}
				break;
			}

			currentTarget = currentTarget.parent;
		}
	}

	protected virtual void Awake() {
		receivedValueOnce = false;
	}
}
