using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Découple les soruces de valeurs de leurs envois au serveur
/// </summary>
public class ValueSourcesSender : MonoBehaviour
{

	/// <summary>
	/// Singleton auto-créé de ce composant
	/// </summary>
	private static ValueSourcesSender Instance {
		get {
			if (instance == null) {
				GameObject gameObj = new GameObject ("Variable changes publisher");
				instance = gameObj.AddComponent<ValueSourcesSender> ();
			}
			return instance;
		}
	}
	private static ValueSourcesSender instance;

	protected virtual void Awake() {
		if (instance != null && instance != this) {
			Debug.LogError ("Multiples instances créées pour le singleton de " + GetType().Name);
			DestroyImmediate (this);
		}
	}
	/// <summary>
	/// Objet serveur chargé de relayer les cangements valeur à tous les clients.
	/// Doit être affecté avec une référence non nulle pour que le composant puisse effectivement
	/// relayer des changements de valeur.
	/// </summary>
	private ServerVariables remoteVariablesContainer;

	/// <summary>
	/// initialise la référence de ce composant à l'objet serveur qui s'occupe concrètement de la publication
	/// des changements.
	/// </summary>
	public static void SetRemoteVariablesContainer(ServerVariables remoteVariablesContainer) {
		Instance.remoteVariablesContainer = remoteVariablesContainer;
	}

	/// <summary>
	///  Publie le fait q'une variable a changé de valeur
	/// </summary>
	public static void SendNewValue<T>(SimpleValueSource<T> changedVariable) {
		ServerVariables remoteVars = Instance.remoteVariablesContainer;
		if (remoteVars != null) {
			remoteVars.SignalVariableChangeToServer (changedVariable);
		} else {
			Debug.LogError ("Impossible de signaler le changement d'une variable, car aucun accès au serveur n'est configuré sur ce composant");
		}
	}
}

