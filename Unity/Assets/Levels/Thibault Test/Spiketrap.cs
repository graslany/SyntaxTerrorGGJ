using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiketrap : MonoBehaviour,TrapInterface {
    [SerializeField] bool _IsActivated;
    public GameObject _spritePrefab;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Trigger();
    }

    public void Trigger()
    {
        Debug.Log("trig");
        if (_IsActivated)
        {
            Spring();
        }
    }

    public void Spring()
    {
        print("tets");
        _spritePrefab.transform.Translate(new Vector3(0, 0.5f, 0));
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
