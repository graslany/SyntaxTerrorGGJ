using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float damage;

	protected virtual void OnCollisionEnter(Collision collision) {
		// Si on touche un collider "dur", il faut s'auto-détruire afin de ne pas laisser
		// de "munition non explosées" dans le niveau qui pourraient faire des dégâts
		// si on marche dessus => alwaysSelfDestruct = true passé à la méthode.
		OnBulletHit (collision.collider, true);
	}

	protected virtual void OnCollisionStay(Collision collision) {
		OnBulletHit (collision.collider, true);
	}

	protected virtual void OnTriggerEnter(Collider collider) {
		OnBulletHit (collider, false);
	}

	protected virtual void OnTriggerStay(Collider collider) {
		OnBulletHit (collider, false);
	}

	protected virtual void OnBulletHit(Collider collider, bool alwaysSelfDestruct) {
		bool willSelfDestruct = alwaysSelfDestruct;
		PlayerHitPoints target = collider.GetComponent<PlayerHitPoints> ();
		if (target != null) {
			target.TakeDamage (damage);
			willSelfDestruct = true;
		}
		if (willSelfDestruct)
			Destroy (this);
	}
}
