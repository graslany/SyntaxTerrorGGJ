using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boule : Piege {
    public float degats = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("le joueur recois " + degats + "pts de dégats ");
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(500, 500, 500));
            
        }
    }
}
