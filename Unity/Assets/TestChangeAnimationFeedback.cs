using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChangeAnimationFeedback : MonoBehaviour, IValuesUser<bool>
{
    public void OnValueChanged(string variableName, bool newValue)
    {
        if (newValue)
            GetComponent<Animator>().Play("LevierActivated");
        else
            GetComponent<Animator>().Play("LevierDisabled");
    }
}
