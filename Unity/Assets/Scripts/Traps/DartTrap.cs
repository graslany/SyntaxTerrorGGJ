using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTrap : TrapBase {

	public Rigidbody dartPrefab;

	public float dartSpeed = 3;

	public float dartDamage = 10;

	public Transform spawnPoint;

	protected virtual void OnTriggerEnter() {
		if (State == TrapState.Armed)
			Spring ();
	}

	protected override void OnStateChanged (TrapState previousState, TrapState newState)
	{
		if (newState == TrapState.Triggered) {
			SpawnDart ();
		}
	}

	private void SpawnDart()
    {
		if (dartPrefab != null && spawnPoint != null) {
			Rigidbody dartBody = Instantiate (
				dartPrefab, spawnPoint.position, spawnPoint.rotation);

			// Affectation d'une vitesse à la fléchette
			dartBody.velocity = dartBody.transform.forward * dartSpeed;

			// Ajout d'un script qui fera des dégâts s'il n'y en a pas encore
			Bullet b = dartBody.GetComponent<Bullet>();
			b = (b ?? dartBody.gameObject.AddComponent<Bullet> ());
			b.damage = dartDamage;
		} else
			Debug.LogError ("Impossible de déclencher le piège : préfab de flèche ou point de départ manquant(s).");
    }
}
