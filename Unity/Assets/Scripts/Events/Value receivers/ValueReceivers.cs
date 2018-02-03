using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueReceivers : MonoBehaviour {

	public static ValueReceivers Instance {
		get {
			if (instance == null) {
				GameObject gameObj = new GameObject ("Value receivers index");
				instance = gameObj.AddComponent<ValueReceivers> ();
			}
			return instance;
		}
	}
	private static ValueReceivers instance;

	protected virtual void Awake() {
		if (instance != null && instance != this) {
			Debug.LogError ("Multiples instances créées pour le singleton de " + GetType().Name);
			DestroyImmediate (this);
		}
	}

	public void SendValueToReceivers<T>(string identifier, T newValue) {
		// Quick & dirty pour l'instant; TODO: maintenir un index par nom plus tard.
		foreach (ISingleValueReceiver target in GameObject.FindObjectsOfType<SingleValueReceiver>()) {
			if (target.SourceName == identifier) {

				// Vérification du fait qu'on est pas en train d'essayer de fourrer une variable entière dans
				// une cible qui supporte des booléens ou autre erreurs du même genre.
				SingleValueReceiver<T> castTarget = (target as SingleValueReceiver<T>);
				if (castTarget != null) {
					castTarget.ReceiveNewValue (newValue);
				} else {
					Debug.LogWarning ("Découverte d'un récepteur de valeur pour la variable " + (identifier ?? "<null>") + " incapable de recevoir sa nouvelle valeur car il ne peut pas recevoir une valeur de type " + typeof(T).Name + ".");
				}
			}
		}
	}
}
