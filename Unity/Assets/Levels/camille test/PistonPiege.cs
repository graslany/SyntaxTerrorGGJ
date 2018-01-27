using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonPiege : Piege {
    bool activer = false;
    float time;
    // Use this for initialization
    void Start () {
        time = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if(Time.time-time>6)
        {
            activer = false;
        }
	}
    public override void Activer()
    {
        base.Activer();
        if(!activer)
        {
            print("");
            time = Time.time;
            activer = true;
            piege.GetComponent<Piston>().activer = true;
        }
            
    }
}
