using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour, TrapInterface {
    bool move;
    Vector3 posStart;

    [SerializeField] bool _IsActivated;
    bool isSpring = false;
    [SerializeField] int _Damage;
    [SerializeField] float vitesse = -10;
    // Use this for initialization
    // Use this for initialization
    void Start()
    {
        posStart = transform.position;

    }
    void Update()
    {
        if(isSpring)
        {
            Debug.Log("spring");
            GetComponent<Rigidbody>().AddForce(new Vector3(0, -50 * vitesse, 0));
            isSpring = false;
        }
        if (transform.position.y > posStart.y)
        {
            move = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
    public void OnCollisionEnter(Collision other)
    {
        print(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {

            var HitPointScript = other.gameObject.GetComponent<PlayerHitPoints>();
            if (HitPointScript != null)
            {
                HitPointScript.takeDamage((int)_Damage, DamageSource.Crushed);
            }
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 50*vitesse, 0));

    }

    public void Trigger()
    {
        if(_IsActivated)
        {
            Spring();
        }
    }

    public void UnTrigger()
    {
        isSpring = false;
    }

    public void Spring()
    {
        isSpring = true;
    }

    public void Deactivate()
    {
        _IsActivated = false;
    }

    public void Reactivate()
    {
        _IsActivated = true;
    }
}
