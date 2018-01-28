using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour {

	[Tooltip("Sous-objet qui doit être déplacé par le script")]
	public Transform targetChild;
	private Vector3 initialLocalPosition;

	[Tooltip("Variable booléenne indiquant si la plaque est en train d'être appuyée")]
	public BooleanValueSourceMB targetVariable;

	[Tooltip("Temps de déplacement de la plateforme")]
	public float travelTime;
	private float travelTimePosition;

	[Tooltip("Direction du déplacement (local space)")]
	public Vector3 travelLocalVector;

	[Tooltip("Tags des objets qui peuvent déclencher la plaque")]
	public List<string> triggerTags;

	bool movingUp;
	bool movingDown;

	protected virtual void Awake() {
		triggerTags = (triggerTags ?? new List<string>());
	}

	void Start()
	{
		movingUp = false;
		movingDown = false;
		travelTimePosition = 0;

		if (targetChild != null) {
			initialLocalPosition = targetChild.localPosition;
		} else
			Debug.LogError ("Objet enfant cible non renseigné");

		if (targetVariable == null)
			Debug.LogError ("Variable cible non renseignée");
	}

	void OnTriggerEnter(Collider other)
	{
		if (triggerTags.Contains(other.gameObject.tag))
		{
			movingUp = false;
			movingDown = true;

			if (targetVariable != null)
				targetVariable.Variable.StoredValue = true;
		}
	}


	void OnTriggerExit(Collider other)
	{
		if (triggerTags.Contains(other.gameObject.tag))
		{
			movingUp = true;
			movingDown = false;

			if (targetVariable != null)
				targetVariable.Variable.StoredValue = false;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (targetChild == null)
			return;
		if (targetVariable == null)
			return;

		// Calcul de la valeur d'interpolation entre les positions de départ et d'arrivée
		Vector3 moveSpeed = Vector3.zero;
		float newTravelTimePosition = 0;
		if (movingUp) {
			if (travelTime > 0)
				newTravelTimePosition = travelTimePosition - Time.deltaTime / travelTime;
			else
				newTravelTimePosition = 0;
		} else if (movingDown) {
			if (travelTime > 0)
				newTravelTimePosition = travelTimePosition + Time.deltaTime / travelTime;
			else
				newTravelTimePosition = 1;
		}
		newTravelTimePosition = Mathf.Clamp01 (newTravelTimePosition);
		travelTimePosition = newTravelTimePosition;

		// Déplacement de la plaque
		targetChild.localPosition = initialLocalPosition + travelTimePosition * travelLocalVector;
	}
}
