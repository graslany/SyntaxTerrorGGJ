using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
	[Tooltip("Portée du laser")]
	public float range = 20;

	[Tooltip("Dégâts infligés par ce laser")]
	public float damagePerSecond;

	public bool IsFiring {
		get {
			return isFiring;
		}
		set {
			if (isFiring != value) {
				isFiring = value;
				Reconfigure ();
			}
		}
	}
	private bool isFiring;

	/// <summary>
	/// LineRenderer chargé de tracer le laser.
	/// </summary>
	private LineRenderer laserRenderer;

	protected virtual void Update () {
		if (isFiring) {
			DrawLaser ();
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, 1, 0)), out hit))
			{
				// Recherche de quelque chose à blesser sur ce qui a été touché
				GameObject hitObject = hit.collider.gameObject;
				PlayerHitPoints healthScript = hitObject.GetComponent<PlayerHitPoints>();
				if (healthScript != null)
				{
					healthScript.TakeDamage(damagePerSecond * Time.deltaTime, DamageSource.Burned);
				}
			}
		}
	}

	private void DrawLaser() {
		laserRenderer = (laserRenderer ?? GetComponent<LineRenderer>());

		// Origine du laser
		laserRenderer.SetPosition(0, transform.position);

		// Point d'impact
		RaycastHit hasHit;
		if (Physics.Raycast(transform.position, transform.forward, out hasHit))
			laserRenderer.SetPosition(1, hasHit.point);
		else
			laserRenderer.SetPosition(1, transform.position + transform.forward * range);
	}

	// Cache ou dessine le laser, suivant qu'il est en train de tirer ou non.
	private void Reconfigure() {
		laserRenderer = (laserRenderer ?? GetComponent<LineRenderer>());
		if (isFiring) {
			laserRenderer.SetPositions (new Vector3[] {default(Vector3), default(Vector3)});
			DrawLaser ();
			laserRenderer.enabled = true;
		} else {
			laserRenderer.enabled = false;
		}
	}


}
