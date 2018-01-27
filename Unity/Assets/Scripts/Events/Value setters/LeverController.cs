using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour {

	BooleanValueSourceMB targetVariable;

	protected virtual void Start() {
		targetVariable = GetComponent<BooleanValueSourceMB> ();
		if (targetVariable == null)
			Debug.LogError ("Variable cible manquante");
	}

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
			if (targetVariable != null)
				targetVariable.Variable.StoredValue = !targetVariable.Variable.StoredValue;
        }
    }
}
