using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverFeedback : MonoBehaviour, IValuesUser<bool> {

	public string variableName;
	private Animator _animator;

	void Start () {
        _animator = GetComponent<Animator>();
		if (_animator != null)
			Debug.LogError ("Impossible de trouver l'animateur cible à piloter");
    }

	public void OnValueChanged(string variableName, bool newValue) {
		if (_animator != null) {
			if (newValue)
				_animator.Play("LevierDisabled");
			else
				_animator.Play("LevierActivated");
		}
	}
}
