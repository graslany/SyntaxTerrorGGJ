using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Door : MonoBehaviour, IValuesUser<bool>, ITriggerColliderParent {

	[Tooltip("Scène vers laquelle voyage la porte")]
	public SceneEnum destinationScene;

	[Tooltip("Texte affiché au-dessus de a porte quand elle est verrouillée")]
	public Canvas doorText = null;

	[Tooltip("Animateur qui rend l'état de la porte")]
	public Animator animator;

	private bool p1OK = false;
	private bool p2OK = false;
	private bool p3OK = false;

	public float textDisplayDuration = 4;
	private float? textDisplayTime = null;

    private bool playerIsInFrontOfDoor = false;

	/// <summary>
	/// Paramètre de l'animateur qui contrôle son état
	/// </summary>
	private AnimatorControllerParameter animatorParameter;

	protected virtual void Start() {
		// Initialisation de la partie "feedback joueur"
		animator = (animator ?? GetComponent<Animator>());
		if (animator != null) {
			animatorParameter = animator.parameters.
				FirstOrDefault (p => p.name == "isOpen" && p.type == AnimatorControllerParameterType.Bool);
			if (animatorParameter == null)
				Debug.LogError ("Le nom du paramètre de l'animateur de porte n'est pas correctement renseigné dans le script");
		} else 
			Debug.LogError ("Impossible de trouver l'animateur cible à piloter");
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagNames.Player && other.gameObject.GetComponent<ThirdPersonCharacter>() != null)
        {
            playerIsInFrontOfDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
		if (other.gameObject.tag == TagNames.Player && other.gameObject.GetComponent<ThirdPersonCharacter>() != null)
        {
			playerIsInFrontOfDoor = false;
			if (doorText != null)
				doorText.enabled = false;
        }
    }

	public void OnChildTriggerEnter (Collider other) { OnTriggerEnter (other); }
	public void OnChildTriggerStay (Collider other) { }
	public void OnChildTriggerExit (Collider other) { OnTriggerExit (other); }

    // Update is called once per frame
    void Update () {
		if (playerIsInFrontOfDoor && Input.GetKeyDown(KeyCode.E))
        {
			if (p1OK && p2OK && p3OK)
			{
				Scenes.LoadSceneByID (destinationScene);
            }
            else
			{
				doorText.enabled = true;
				textDisplayTime = Time.time;
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

	public void OnValueChanged(string variableName, bool newValue) {
		if (variableName == VariableNames.Player1Door)
			p1OK = newValue;
		else if (variableName == VariableNames.Player2Door)
			p2OK = newValue;
		else if (variableName == VariableNames.Player3Door)
			p3OK = newValue;

		if (animatorParameter != null) {
			bool doorCanOpen = p1OK && p2OK && p3OK;
			if (doorCanOpen) {
				animator.SetBool (animatorParameter.nameHash, true);
			} else {
				animator.SetBool (animatorParameter.nameHash, false);
			}
		}

	}
}
