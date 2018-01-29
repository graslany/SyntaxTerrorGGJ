using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ConnectedLight : MonoBehaviour, IValuesUser<bool> {

	[Tooltip("Indique si l'ampoule est active")]
	[SerializeField]
	private bool isSwitchedOn = false;
	public bool IsSwitchedOn {
		get {
			return isSwitchedOn_;
		}
		set {
			if (isSwitchedOn_ != value) {
				isSwitchedOn_ = value;
				isSwitchedOn = value;
				ApplyState ();
			}
		}
	}
	private bool isSwitchedOn_;

	[Tooltip("Composant qui génère la lumière de l'ampoule, qui sera coloré selon son état")]
	public Light bulbLight;

	[Tooltip("Renderer qui affiche l'ampoule, qui sera coloré comme la lumière émise")]
	public Renderer bulbRenderer;

	[Tooltip("Couleur de l'ampoule quand elle est active")]
	[SerializeField]
	private Color switchedOnColor = new Color(0, 1, 0);
	public Color SwitchedOnColor {
		get {
			return switchedOnColor_;
		}
		set {
			if (switchedOnColor_ != value) {
				switchedOnColor_ = value;
				switchedOnColor = value;
				ApplyState ();
			}
		}
	}
	private Color switchedOnColor_;

	[Tooltip("Puissance de l'ampoule quand elle est active")]
	[SerializeField]
	private float switchedOnIntensity = 2;
	public float SwitchedOnIntensity {
		get {
			return switchedOnIntensity_;
		}
		set {
			if (switchedOnIntensity_ != value) {
				switchedOnIntensity_ = value;
				switchedOnIntensity = value;
				ApplyState ();
			}
		}
	}
	private float switchedOnIntensity_;

	[Tooltip("Couleur de l'ampoule quand elle est n'est pas active")]
	[SerializeField]
	private Color switchedOffColor = new Color(1, 0, 0);
	public Color SwitchedOffColor {
		get {
			return switchedOffColor_;
		}
		set {
			if (switchedOffColor_ != value) {
				switchedOffColor_ = value;
				switchedOffColor = value;
				ApplyState ();
			}
		}
	}
	private Color switchedOffColor_;

	[Tooltip("Puissance de l'ampoule quand elle n'est pas active")]
	[SerializeField]
	private float switchedOffIntensity = 2;
	public float SwitchedOffIntensity {
		get {
			return switchedOffIntensity_;
		}
		set {
			if (switchedOffIntensity_ != value) {
				switchedOffIntensity_ = value;
				switchedOffIntensity = value;
				ApplyState ();
			}
		}
	}
	private float switchedOffIntensity_;

	protected virtual void Awake() {

	}

	protected virtual void OnEnable () {
		ApplyState ();
	}

	protected virtual void Update () {
		if (Application.isEditor)
			SyncFromEditor ();
	}

	public void OnValueChanged(string variableName, bool newValue) {
		IsSwitchedOn = newValue;
	}

	/// <summary>
	/// Applique l'état courant de la lampe (couleur)
	/// </summary>
	private void ApplyState() {
		Debug.Log ("Apply");

		Color bulbColor = (isSwitchedOn_ ? switchedOnColor_ : switchedOffColor_);
		float bulbIntensity = (isSwitchedOn_ ? switchedOnIntensity_ : switchedOffIntensity_);

		// Modification de la couleur de la lumière
		if (bulbLight != null) {
			bulbLight.color = bulbColor;
			bulbLight.intensity = bulbIntensity;
		}

		// Toucher à des matériaux en EditMode doit être fait avec précautions, il faut
		// lire  la doc à ce propos. Comme ce code n'est pas si important que ça, on se contente
		// de ne toucher à rien en edit mode.
		if (Application.isPlaying) {
			Material bulbMaterial = null;
			if (bulbRenderer != null)
				bulbMaterial = bulbRenderer.material;
			if (bulbMaterial != null)
				bulbMaterial.color = bulbColor;
		}
	}

	/// <summary>
	/// Met à jour l'état si on a affecté des champs depuis l'éditeur
	/// </summary>
	private void SyncFromEditor() {
		bool hasChanges = false;
		SyncFieldFromEditor (isSwitchedOn, ref isSwitchedOn_, ref hasChanges);
		SyncFieldFromEditor (switchedOnColor, ref switchedOnColor_, ref hasChanges);
		SyncFieldFromEditor (switchedOnIntensity, ref switchedOnIntensity_, ref hasChanges);
		SyncFieldFromEditor (switchedOffColor, ref switchedOffColor_, ref hasChanges);
		SyncFieldFromEditor (switchedOffIntensity, ref switchedOffIntensity_, ref hasChanges);

		if (hasChanges) {
			ApplyState ();
		}
	}

	/// <summary>
	/// Applique une éventuelle valeur entrée via l'éditeur Unity au champ ordinaire de
	/// stockage d'une propriété, afin de simuler une affectation normale de la propriété
	/// et de détecter d'éventuels changements de valeur.
	/// </summary>
	private void SyncFieldFromEditor<T>(T editorField, ref T normalField, ref bool hasChanges) {
		if (!Equals (editorField, normalField)) {
			normalField = editorField;
			hasChanges = true;
		}
	}
}
