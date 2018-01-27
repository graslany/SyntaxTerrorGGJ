using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButtonVariableController : MonoBehaviour {

	public BooleanValueSourceMB targetVariable;

	protected virtual void OnTriggerEnter() {
		SetVariable (true);
	}

	protected virtual void OnTriggerExit() {
		SetVariable (false);
	}

	private void SetVariable(bool newValue) {
		if (targetVariable != null) {
			targetVariable.Variable.StoredValue = newValue;
		} else {
			Debug.LogError ("La variable que doit modifier ce composant n'est pas affectée");
		}
	}
}
