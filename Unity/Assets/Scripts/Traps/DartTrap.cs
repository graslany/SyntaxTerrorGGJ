using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTrap : TrapBase {

	public Rigidbody dartPrefab;
	public float dartSpeed;

	protected override void OnStateChanged (TrapState previousState, TrapState newState)
	{
		if (newState == TrapState.Triggered) {
			SpawnDart ();
		}
	}

	private void SpawnDart()
    {
		if (dartPrefab != null) {
			Rigidbody dartBody = Instantiate (
				                     dartPrefab,
				                     transform.position,
				                     transform.rotation);

			// Affectation d'une vitesse à la fléchette
			dartBody.velocity = dartBody.transform.forward * dartSpeed;
		} else
			Debug.LogError ("Impossible de déclencher le piège : préfab de flèche manquant.");
    }
}
