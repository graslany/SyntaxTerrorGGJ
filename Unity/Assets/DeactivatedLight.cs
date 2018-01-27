using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatedLight : MonoBehaviour, IValuesUser<bool>
{
    private void Start()
    {
        gameObject.GetComponent<Light>().enabled = false;
    }

    public void OnValueChanged(string variableName, bool newValue)
    {
        Debug.Log("Received new value for " + variableName + ": " + newValue);
        if (variableName == "p2light")
        {
            gameObject.GetComponent<Light>().enabled = newValue;
        }
    }
}
