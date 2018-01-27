using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Classe qui réceptionne la valeur d'une variable du jeu.
/// </summary>
public class FloatValueSourceMB : MonoBehaviour {

	[SerializeField]
	private FloatValueSource variable;
	public FloatValueSource Variable {
		get {
			return variable;
		}
	}

	public FloatValueSourceMB() {
		variable = (variable ?? new FloatValueSource ());
	}
}
