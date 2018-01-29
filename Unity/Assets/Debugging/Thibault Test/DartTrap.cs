using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTrap : MonoBehaviour, TrapInterface
{
    [SerializeField] bool _IsActivated;
    public GameObject _DartPrefab;
	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Trigger();
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

    }

    public void Spring()
    {
        var bullet = (GameObject)Instantiate(
                                    _DartPrefab,
                                    transform.position,
                                    transform.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 8;
    }

    // Update is called once per frame
    public void Deactivate()
    {
        _IsActivated = false;
    }

    public void Reactivate()
    {
        _IsActivated = true;
    }
}
