using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void DieDieDie()
    {
        var Blood = gameObject.GetComponentInChildren<ParticleSystem>();
        if (Blood != null)
        {
            Blood.transform.SetParent(null);
            var effect = Blood.main;
            Blood.Stop();
            effect.duration = 3.0f;
            Blood.Play();
        }
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
