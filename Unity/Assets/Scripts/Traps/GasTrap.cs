using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GasTrap : TrapBase
{

	[Tooltip("Modèle de ParticleSystem utilisé pour rendre visuellement le piège à gas")]
	public ParticleSystem SprinklerPrefab;

	[Tooltip("Quantité de dégâts appliqués au joueur chaque seconde")]
    public float DamagePerSecond;

	[Tooltip("Positions où faire apparaître les particlesSystems pour le rendu du piège")]
	public List<Transform> sprinklersPositions;

	/// <summary>
	/// ParticleSystems créés par ce script.
	/// </summary>
	private List<ParticleSystem> sprinklers;


    void OnTriggerStay(Collider coll)
    {
		switch (State) {
		case TrapState.Armed:
			Spring ();
			break;
		case TrapState.Triggered:
			if (State == TrapState.Triggered) {
				var victim = coll.transform.GetComponent<PlayerHitPoints> ();
				if (victim != null)
					victim.TakeDamage (DamagePerSecond * Time.deltaTime, DamageSource.Suffocation);
			}
			break;
		}
	}

	protected override void OnStateChanged (TrapState previousState, TrapState newState)
	{
		ReconfigureSprinklers (previousState, newState);
		ProduceSoundFeedback (previousState, newState);
	}

	/// <summary>
	/// Positionne et déclenche ou supprime les retours visuels du piège quand il est activé.
	/// </summary>
	private void ReconfigureSprinklers (TrapState previousState, TrapState newState) {
		if (newState == TrapState.Triggered)
			StartSprinklers ();
		else
			StopSprinklers ();
	}

	/// <summary>
	/// Suppprime les particle systmes courants.
	/// </summary>
	private void StartSprinklers() {
		
		// Instanciaiton des PS la première fois
		if (sprinklers == null && sprinklersPositions != null && SprinklerPrefab != null) {
			sprinklers = new List<ParticleSystem> ();
			foreach (Transform sprinklerPosition in sprinklersPositions) {
				ParticleSystem sprinkler = Instantiate (SprinklerPrefab, sprinklerPosition);
				sprinkler.transform.localPosition = Vector3.zero;
				sprinkler.transform.localRotation = Quaternion.identity;
				sprinkler.transform.localScale = Vector3.one;
				sprinklers.Add (sprinkler);
			}
		}

		// Activation des particleSystems
		if (sprinklers != null) {
			foreach (ParticleSystem sprinkler in sprinklers) {
				sprinkler.Play();
			}
		}
	}

	/// <summary>
	/// Suppprime les particle systmes courants.
	/// </summary>
	private void StopSprinklers() {
		if (sprinklers != null) {
			foreach (ParticleSystem sprinkler in sprinklers) {
				sprinkler.Stop();
			}
		}
	}

	/// <summary>
	/// Produit un retour sonore lors d'un changement d'état.
	/// </summary>
	private void ProduceSoundFeedback (TrapState previousState, TrapState newState)  {
		if (newState == TrapState.Triggered) {
			AudioSource audio = GetComponent<AudioSource> ();
			if (audio != null) {
				audio.Play ();
			}
		}
	}
}
