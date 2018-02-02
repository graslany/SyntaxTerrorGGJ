using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston :TrapBase {
	
    Vector3 posStart;

    public float _Damage;
	public float speed = -10;

    void Start()
    {
        posStart = transform.position;
    }

    protected virtual void Update()
    {
		if(State == TrapState.Triggered)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, -50 * speed, 0));
        }
        if (transform.position.y > posStart.y)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

	protected virtual void OnCollisionEnter(Collision other)
    {
        print(other.gameObject.name);
		if (other.gameObject.tag == TagNames.Player)
        {

            var HitPointScript = other.gameObject.GetComponent<PlayerHitPoints>();
            if (HitPointScript != null)
            {
                HitPointScript.TakeDamage(_Damage, DamageSource.Crushed);
            }
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 50*speed, 0));
    }

	protected override void OnStateChanged (TrapState previousState, TrapState newState)
	{
		//
	}
}
