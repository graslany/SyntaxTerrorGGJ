using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.ThirdPerson;

public class PleasurePlateController : AbstractButton
{

	[Tooltip("Variable qui stocke l'état d'activation")]
	public BooleanValueSourceMB stateVariable;

	[Tooltip("Bidule à translater pour indiquer que le bouton est en train d'être activé")]
	public Transform targetTransform;

	[Tooltip("Sens et direction du mouvement du bidule")]
	public Vector3 targetLocalMoveDistance = new Vector3 (0, -0.3f, 0);

	[Tooltip("Durée de la translation")]
	public float animationLength = 1;

	/// <summary>
	/// Position dans l'animation du bidule moble (valeur dans [0, 1], 0 = OFF).
	/// </summary>
	private float animationPosition;

	/// <summary>
	/// "Morceaux de joueur" présents sur la plaque
	/// </summary>
	private List<GameObject> playerPartsOnPlate;

	/// <summary>
	/// Position dans l'animation du bidule moble (valeur dans [0, 1], 0 = OFF).
	/// </summary>
	private Vector3 initialLocalPosition;

	protected virtual void Awake() {
		playerPartsOnPlate = new List<GameObject> ();
	}

	protected virtual void Start()
	{
		if (stateVariable == null) {
			Debug.LogError ("Aucune variable n'est connectée au composant pour stocker son état.");
			// Pour ne pas flooder la console avec des NullRefs à chaque frame.
			enabled = false;
		}
		
		if (targetTransform != null)
			initialLocalPosition = targetTransform.localPosition;
		else
			Debug.LogWarning ("Il manque à ce bouton un objet à déplacer pour indiquer son état.");
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
		GameObject stuff = other.gameObject;
		if (stuff.tag == TagNames.Player && !playerPartsOnPlate.Contains(stuff))
        {
			// S'il n'y avait aucun morceau de joueur sur la plaque, il faut l'activer ou inverser son état
			if (playerPartsOnPlate.Count == 0) {
				switch (Mode) {
				case ButtonMode.ActiveWhenPressed:
					stateVariable.Variable.StoredValue = true;
					break;
				case ButtonMode.SwitchWhenPressed:
					stateVariable.Variable.StoredValue = !stateVariable.Variable.StoredValue;
					break;
				case ButtonMode.DoesNothing:
					// On se contente d'enregistrer que le joueur est présent mais on ne fat rien pour le moment.
					break;
				}
			}

			// On ajoute le nouveau collider aux colliders présents.
			playerPartsOnPlate.Add (stuff);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
	{
		bool wasPreviouslypressed = (playerPartsOnPlate.Count > 0);
		GameObject stuff = other.gameObject;
		playerPartsOnPlate.RemoveAll (p => ReferenceEquals (p, stuff));

		// S'il n'y a plus de morceaux de joueurs sur la plaque, elle peut devenir inactive (selon son mode de fonctionnement).
		if (wasPreviouslypressed && playerPartsOnPlate.Count == 0 && Mode == ButtonMode.ActiveWhenPressed) {
			stateVariable.Variable.StoredValue = false;
		}
    }

	protected virtual void Update()
    {

		bool wasPreviouslypressed = (playerPartsOnPlate.Count > 0);
		playerPartsOnPlate.RemoveAll (p => p == null);

		// S'il n'y a plus de morceaux de joueurs sur la plaque, elle peut devenir inactive (selon son mode de fonctionnement).
		if (wasPreviouslypressed && playerPartsOnPlate.Count == 0 && Mode == ButtonMode.ActiveWhenPressed) {
			stateVariable.Variable.StoredValue = false;
		}

		// Retour visuel, si disponible.
		if (targetTransform != null) {
			float targetAnimationPosition = (playerPartsOnPlate.Count == 0 ? 0 : 1);
			if (animationPosition != targetAnimationPosition) {

				// Calcul de la nouvelle position dans l'animation (0 = début, 1 = fin)
				if (animationLength > 0)
					animationPosition = Mathf.Clamp01(animationPosition + (2 * (targetAnimationPosition - 0.5f)) * Time.deltaTime / animationLength);
				else
					animationPosition = targetAnimationPosition;

				// Positionnement du bidule
				targetTransform.localPosition = initialLocalPosition + animationPosition * targetLocalMoveDistance;
			}
		}
	}

	protected override void OnModeChanged(ButtonMode oldMode, ButtonMode newMode) {
		base.OnModeChanged (oldMode, newMode);

		switch(newMode) {
		case ButtonMode.ActiveWhenPressed:
			if (playerPartsOnPlate.Count > 0)
				// Si une plaque devient sensible à la pression alors que le joueur est dessus,
				// Elle doit faire passer sa variable cible à true.
				stateVariable.Variable.StoredValue = true;
			break;
		}
	}
}
