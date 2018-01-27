using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boule_piege : Piege {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void Activer()
    {
        base.Activer();
        Instantiate(base.piege);
    }
}
