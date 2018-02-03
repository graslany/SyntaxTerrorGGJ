using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe de base pour les composants qui s'exécutent en EditMode, afin d'appliquer
/// la configuration d'autres composants quand le jeu ne s'exécute pas encore.
/// </summary>
[ExecuteInEditMode]
public abstract class EditorFeedback<TTarget> : MonoBehaviour where TTarget : MonoBehaviour {

	[Tooltip("Composant cible qui sera configuré par celui-ci en EditMode. Recherché si nul à chaque Update")]
	public TTarget target;

	protected virtual void Awake() {
		// Ce composant n'a pas de raison d'être s'il n'est pas en EditMode
		if (Application.isPlaying)
			Destroy (this);
	}

	protected virtual void Update () {
		// Récupération de la cible
		target = (target ?? GetComponent<TTarget>());

		if (target != null) {
			if (TargetMayHaveChanged ())
				ApplyTargetConfiguration ();
		} else {
			Debug.LogWarning ("Ce composant n'a rien à configurer et va s'auto-détruire.");
			Destroy (this);
		}
	}

	/// <summary>
	/// Indique si la configuration du composant cible a pu changer depuis la dernière fois que
	/// cette mthode a été appelée.
	/// </summary>
	protected virtual bool TargetMayHaveChanged() {
		return true;
	}

	/// <summary>
	/// Demande à la cible de se mettre à jour en fonction de sa configuration courante.
	/// </summary>
	protected abstract void ApplyTargetConfiguration();

	/// <summary>
	/// Copie une valeur dans un champ, et affecte true au flag fourni si la valeur initiale
	/// du champ cible était différente de la source.
	/// </summary>
	protected void CompareAndSet<T>(T sourceField, ref T targetField, ref bool fieldChanged) {
		if (!Equals (sourceField, targetField)) {
			targetField = sourceField;
			fieldChanged = true;
		}
	}
}
