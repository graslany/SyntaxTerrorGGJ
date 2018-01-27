using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour, IValuesUser<bool>
{
    private void Start()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    public void OnValueChanged(string variableName, bool newValue)
    {
        Debug.Log("Received new value for " + variableName + ": " + newValue);
        if (variableName == "p3test")
        {
            gameObject.GetComponent<Renderer>().enabled = newValue;
        }
    }
}
