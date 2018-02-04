using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiketrap : TrapBase {

	[Tooltip("Quantité de dommages infligés par ce piège")]
	public float damage;

	[Tooltip("Direction et longueur du déplacmeent local des pointes")]
	public Transform spikesToMove;

	[Tooltip("Direction et longueur du déplacmeent local des pointes")]
	public Vector3 spikesLocalMoveVector = new Vector3(0, 0.3f, 0);

	[Tooltip("Retour sonore d'activation du piège")]
	public AudioSource activationSound;

	Vector3 initialLocalPosition;

    void Start()
    {
		if (spikesToMove != null)
			initialLocalPosition = spikesToMove.localPosition;
		else
			Debug.LogError ("Transform des pointes manquante");
    }

    private void OnTriggerEnter(Collider other)
    {
		// Déclenchement spontané si le joueur marche sur le piège armé.
		if(State == TrapState.Armed && other.gameObject.tag == TagNames.Player)
        {
			Spring ();
        }

		// Application de dégâts s'il marche sur un piège déclenché.
		// C'est le cas aussi si le piège vient d'être déclenché ci-dessus.
		if (State == TrapState.Triggered) {
			var healthScript = other.GetComponent<PlayerHitPoints>();
			if (healthScript != null)
			{
				healthScript.TakeDamage(damage, DamageSource.Impaled);
			}
		}
    }

	protected override void OnStateChanged (TrapState previousState, TrapState newState)
	{
		if (spikesToMove == null)
			return;

		bool switchedToTriggered = (newState == TrapState.Triggered);
		if (switchedToTriggered) {
			spikesToMove.localPosition = initialLocalPosition + spikesLocalMoveVector;
			if (activationSound != null)
				activationSound.Play ();
		} else {
			spikesToMove.localPosition = initialLocalPosition;
		}
	}
}
