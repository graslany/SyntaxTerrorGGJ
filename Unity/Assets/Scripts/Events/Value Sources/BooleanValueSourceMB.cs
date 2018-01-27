using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Classe qui réceptionne la valeur d'une variable du jeu.
/// </summary>
public abstract class BooleanValueSourceMB : MonoBehaviour {

	[SerializeField]
	private SimpleValueSource<bool> value;
	SimpleValueSource<bool> Value {
		get {
			return value;
		}
	}

	protected virtual void Awake() {
		
	}
}
