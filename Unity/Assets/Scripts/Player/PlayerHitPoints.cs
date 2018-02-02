using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum DamageSource
{
    Suffocation,
    Crushed,
    Impaled,
    Burned,
    Default
}

public class PlayerHitPoints : MonoBehaviour
{

	[Tooltip("Nombre de points de vie maximaux de l'objet.")]
	[SerializeField]
	private float maxHitPoints;
	float MaxHitPoints {
		get {
			return maxHitPoints;
		}
	}

	/// <summary>
	/// Nombre de points de vie courants de l'objet.
	/// </summary>
	float CurrentHitPoints {
		get {
			return Mathf.Max(0, maxHitPoints - damageReceived);
		}
	}

	/// <summary>
	/// Nombre de points de dégât subis par l'objet.
	/// </summary>
	float DamageReceived {
		get {
			return damageReceived;
		}
	}
    float damageReceived;

	/// <summary>
	/// Inflige une quantité de dégâtts donnée au joueur.
	/// </summary>
	public void TakeDamage(float amount, DamageSource damageType = DamageSource.Default)
	{
		if (amount < 0) {
			Debug.LogError ("Les dommages reçus doivent être une quantité positive. Utiliser Heal() pour soigner l'objet");
			return;
		}

		// Rendu visuel
		var Blood = gameObject.GetComponentInChildren<ParticleSystem>();
		if (Blood != null && !Blood.isPlaying)
		{
			Blood.Play();
		}

		// Comptabilisation des dégâts.
		damageReceived += amount;
		if (CurrentHitPoints <= 0) {
			
			// Oh no we dead
			if (gameObject.GetComponent<DeathScript>())
				gameObject.GetComponent<DeathScript>().DieDieDie(); // I'm not a psychopath... I'm a highly efficient psychopath!
			else
				Destroy(gameObject);
		}
	}

	/// <summary>
	/// Soigne l'objet.
	/// </summary>
	public void Heal(float amount)
	{
		if (amount < 0) {
			Debug.LogError ("Les soins reçus doivent être une quantité positive. Utiliser TakeDamage() pour blesser l'objet");
			return;
		}
		if (CurrentHitPoints <= 0) {
			Debug.LogError ("Nous arrivons trop tard, il est mort, Jim ! (i.e., impossible de soigner un objet mort)");
			return;
		}

		// Comptabilisation des soins.
		damageReceived = Mathf.Max(0, damageReceived - amount);
	}
}
