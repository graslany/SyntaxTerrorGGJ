using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : Piege {
    bool move;
    Vector3 posStart;

    public bool activer = false;
    public float vitesse = -10;
    // Use this for initialization
    public float degats = 1;
    // Use this for initialization
    void Start()
    {
        posStart = transform.position;
        print(transform.position.y > posStart.y);

    }
    void Update()
    {
        if(activer)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, -50 * vitesse, 0));
            activer = false;
        }
        if (transform.position.y > posStart.y)
        {
            move = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
    public override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
        print(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
           
            Debug.Log("le joueur recois " + degats + "pts de dégats ");
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 50*vitesse, 0));

    }
}
