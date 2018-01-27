using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void DieDieDie()
    {
        var Blood = gameObject.GetComponent<ParticleSystem>();
        if (Blood != null)
        {
            var effect = Blood.main;
            effect.duration = 3.0f;
        }
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
