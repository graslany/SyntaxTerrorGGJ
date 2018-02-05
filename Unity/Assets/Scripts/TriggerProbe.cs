using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Déporte l'observation des messages OnTriggerEnter/Stay/Exit pour les composants
/// qui on leurs colliders sur des enfants.
/// </summary>
public class TriggerProbe : MonoBehaviour {

	public ITriggerColliderParent target;

	private void OnTriggerEnter(Collider other)
	{
		if (target != null)
			target.OnChildTriggerEnter (other);
	}

	private void OnTriggerStay(Collider other)
	{
		if (target != null)
			target.OnChildTriggerStay (other);
	}

	private void OnTriggerExit(Collider other)
	{
		if (target != null)
			target.OnChildTriggerExit (other);
	}

}
