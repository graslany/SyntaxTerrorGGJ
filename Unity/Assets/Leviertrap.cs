using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leviertrap : MonoBehaviour,TrapInterface {
    bool isactive = false;
    bool _IsActivated = true;
    public void Deactivate()
    {
        _IsActivated = false;
    }

    public void Reactivate()
    {
        _IsActivated=true;
    }

    public void Spring()
    {
        transform.gameObject.SetActive(!isactive);
        isactive = !isactive;
    }

    public void Trigger()
    {
        if (_IsActivated)
        {
            Spring();
        }
    }

    public void UnTrigger()
    {
        transform.gameObject.SetActive(!isactive);
        isactive = !isactive;

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
