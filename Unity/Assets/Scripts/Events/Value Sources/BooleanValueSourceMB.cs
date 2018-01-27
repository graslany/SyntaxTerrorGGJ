using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Classe qui réceptionne la valeur d'une variable du jeu.
/// </summary>
public class BooleanValueSourceMB : MonoBehaviour {

	[SerializeField]
	private BooleanValueSource variable;
	public BooleanValueSource Variable {
		get {
			return variable;
		}
	}

	public BooleanValueSourceMB() {
		variable = (variable ?? new BooleanValueSource ());
	}
}
