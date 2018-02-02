using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Laser))]
public class LaserTrap : TrapBase
{
	private Laser mylaz0r;

	protected override void OnStateChanged (TrapState previousState, TrapState newState)
	{
		mylaz0r = (mylaz0r ?? GetComponent<Laser>());
		bool switchedToTriggered = (newState == TrapState.Triggered);

		// Activation du laser
		mylaz0r.IsFiring = switchedToTriggered;

		// Retour sonore
		if (switchedToTriggered) {
			AudioSource audio = gameObject.GetComponent<AudioSource> ();
			if (audio != null) {
				audio.Play ();
			}
		}
	}
}
