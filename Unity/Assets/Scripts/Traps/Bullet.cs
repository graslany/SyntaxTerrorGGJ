using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float damage;

	private float spawnTime;

	private static readonly float maxLifeTime = 5;

	protected virtual void Start() {
		spawnTime = Time.time;
	}

	protected virtual void update() {
		if (Time.time - spawnTime > maxLifeTime)
			Destroy (gameObject);
	}

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
			Destroy (gameObject);
	}
}
