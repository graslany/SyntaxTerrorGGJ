using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractButton : MonoBehaviour {

	[Tooltip("Indique si le se comporte comme un boutton poussoir ou comme une check box.")]
	[SerializeField]
	private ButtonMode mode;
	public ButtonMode Mode {
		get {
			return mode;
		}
		set {
			if (mode != value) {
				ButtonMode oldMode = mode;
				mode = value;
				OnModeChanged (oldMode, mode);
			}
		}
	}

	protected virtual void OnModeChanged(ButtonMode oldMode, ButtonMode newMode) { }
}
