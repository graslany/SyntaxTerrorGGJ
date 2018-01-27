using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Classe qui réceptionne la valeur d'une variable du jeu.
/// </summary>
public class IntValueSourceMB : MonoBehaviour {

	[SerializeField]
	private IntValueSource variable;
	public IntValueSource Variable {
		get {
			return variable;
		}
	}

	public IntValueSourceMB() {
		variable = (variable ?? new IntValueSource ());
	}
}
