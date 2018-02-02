using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrap : TrapBase
{
	private static float DefaultPushForce = 10;
	private static float DefaultMaxSpeed = 10;

	public float pushForce = DefaultPushForce;
	public float maxSpeed = DefaultMaxSpeed;
    public GameObject oppositeWall;

    void Start()
    {
        if(pushForce <= 0)
        {
			Debug.LogError("Force de poussée négative, réinitialisée à une valeur positive arbitraire...");
			pushForce = DefaultPushForce;
        }

        if (maxSpeed <= 0)
        {
			Debug.LogError("Vitesse maximale négative, réinitialisée à une valeur positive arbitraire...");
			maxSpeed = DefaultMaxSpeed;
        }
    }

    protected virtual void Update()
    {
		if (State == TrapState.Triggered
            && gameObject.GetComponent<Rigidbody>().velocity.magnitude < maxSpeed)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * pushForce);
        }
    }

	protected virtual void OnCollisionStay(Collision collision)
    {
		if (collision.collider.gameObject.tag == TagNames.Player
            && Vector3.Distance(gameObject.transform.position, oppositeWall.transform.position) < 1.1)
        {
           
            var ds = collision.collider.gameObject.GetComponent<DeathScript>();
            if (ds != null)
            {
                ds.DieDieDie();
            }
            else
            {
                Debug.LogWarning("Le joueur n'a pas de points de vie !?");
            }
        }

        if (collision.collider.gameObject == oppositeWall)
        {
			Disarm ();
        }
    }

	protected override void OnStateChanged (TrapState previousState, TrapState newState)
	{
		//
	}
}
