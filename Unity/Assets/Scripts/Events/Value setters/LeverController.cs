using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Affecte la variable qui représente l'état d'un levier quand le joueur l'active,
/// et produit un retour dans le levier pour signifier qu'il a changé d'état lorsque cela arrive.
/// </summary>
public class LeverController : MonoBehaviour, IValuesUser<bool> {

	[Tooltip("Variable dont la valeur est commandée par ce script")]
	public BooleanValueSourceMB targetVariable;

	/// <summary>
	/// Paramètre de l'animateur qui contrôle son état
	/// </summary>
	private Animator animator;

	/// <summary>
	/// Paramètre de l'animateur qui contrôle son état
	/// </summary>
	private AnimatorControllerParameter animatorParameter;

	protected virtual void Start () {

		// Initialisation de la partie "affectation de variable"
		targetVariable = GetComponent<BooleanValueSourceMB> ();
		if (targetVariable == null)
			Debug.LogError ("Variable cible manquante");

		// Initialisation de la partie "feedback joueur"
		animator = GetComponent<Animator>();
		if (animator != null) {
			animatorParameter = animator.parameters.
				FirstOrDefault (p => p.name == "isSwitchedOn" && p.type == AnimatorControllerParameterType.Bool);
			if (animatorParameter == null)
				Debug.LogError ("Le nom du paramètre de l'animateur de levier n'est pas correctement renseigné dans le script");
		} else 
			Debug.LogError ("Impossible de trouver l'animateur cible à piloter");
	}

	/// <summary>
	/// Anime le levier quand la variable qui le contrôle a changé d'état.
	/// </summary>
	public void OnValueChanged(string variableName, bool newValue) {
		if (targetVariable != null && targetVariable.Variable.Identifier == variableName && animator != null) {
			animator.SetBool (animatorParameter.nameHash, newValue);
		}
	}

    private void OnTriggerStay(Collider other)
    {
		if (Input.GetButtonDown(InputNames.MainInteraction))
        {
			if (targetVariable != null)
				targetVariable.Variable.StoredValue = !targetVariable.Variable.StoredValue;
        }
    }
}
