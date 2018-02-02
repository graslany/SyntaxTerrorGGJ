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
			return isSwitchedOn;
		}
		set {
			if (isSwitchedOn != value) {
				isSwitchedOn = value;
				ApplyState ();
			}
		}
	}

	[Tooltip("Composant qui génère la lumière de l'ampoule, qui sera coloré selon son état")]
	public Light bulbLight;

	[Tooltip("Renderer qui affiche l'ampoule, qui sera coloré comme la lumière émise")]
	public Renderer bulbRenderer;

	[Tooltip("Couleur de l'ampoule quand elle est active")]
	[SerializeField]
	private Color switchedOnColor = new Color(0, 1, 0);
	public Color SwitchedOnColor {
		get {
			return switchedOnColor;
		}
		set {
			if (switchedOnColor != value) {
				switchedOnColor = value;
				ApplyState ();
			}
		}
	}

	[Tooltip("Puissance de l'ampoule quand elle est active")]
	[SerializeField]
	private float switchedOnIntensity = 2;
	public float SwitchedOnIntensity {
		get {
			return switchedOnIntensity;
		}
		set {
			if (switchedOnIntensity != value) {
				switchedOnIntensity = value;
				ApplyState ();
			}
		}
	}

	[Tooltip("Couleur de l'ampoule quand elle est n'est pas active")]
	[SerializeField]
	private Color switchedOffColor = new Color(1, 0, 0);
	public Color SwitchedOffColor {
		get {
			return switchedOffColor;
		}
		set {
			if (switchedOffColor != value) {
				switchedOffColor = value;
				ApplyState ();
			}
		}
	}

	[Tooltip("Puissance de l'ampoule quand elle n'est pas active")]
	[SerializeField]
	private float switchedOffIntensity = 2;
	public float SwitchedOffIntensity {
		get {
			return switchedOffIntensity;
		}
		set {
			if (switchedOffIntensity != value) {
				switchedOffIntensity = value;
				ApplyState ();
			}
		}
	}

	protected virtual void OnEnable () {
		ApplyState ();
	}

	public void OnValueChanged(string variableName, bool newValue) {
		IsSwitchedOn = newValue;
	}

	/// <summary>
	/// Applique l'état courant de la lampe (couleur)
	/// </summary>
	public void ApplyState() {

		Color bulbColor = (isSwitchedOn ? switchedOnColor : switchedOffColor);
		float bulbIntensity = (isSwitchedOn ? switchedOnIntensity : switchedOffIntensity);

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
}
