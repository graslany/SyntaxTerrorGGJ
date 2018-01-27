using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resendAllj1s : MonoBehaviour, IValuesUser<bool> {
    public void OnValueChanged(string variableName, bool newValue)
    {
        gameObject.GetComponent<BooleanValueSourceMB>().Variable.StoredValue = newValue;
    }
}
