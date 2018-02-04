using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooleanValueRepeater : MonoBehaviour, IValuesUser<bool> {

	public BooleanValueSourceMB[] targets;

	public void OnValueChanged(string variableName, bool newValue) {
		if (targets != null)
			foreach (BooleanValueSourceMB t in targets)
				t.Variable.StoredValue = newValue;
	}
}
