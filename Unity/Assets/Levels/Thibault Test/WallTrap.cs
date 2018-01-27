using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrap : MonoBehaviour, TrapInterface
{
    bool _IsActivated;
    bool _IsSprung;
    [SerializeField] float _PushForce;
    [SerializeField] float _MaxSpeed;
    public GameObject _OppositeWall;
    // Use this for initialization
    void Start()
    {
        if(_PushForce == 0)
        {
            Debug.Log("Warning : push force is 0");
        }

        if (_MaxSpeed == 0)
        {
            Debug.Log("Warning : _MaxSpeed is 0");
        }

        _IsActivated = true;
        _IsSprung = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_IsSprung
            && gameObject.GetComponent<Rigidbody>().velocity.magnitude < _MaxSpeed)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(-transform.TransformDirection(gameObject.transform.forward) * _PushForce);
        }
        //Trigger();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player"
            && Vector3.Distance(gameObject.transform.position, _OppositeWall.transform.position) < 1.1)
        {
            Debug.Log("death");
           
            var ds = collision.collider.gameObject.GetComponent<DeathScript>();
            if (ds != null)
            {
                ds.DieDieDie();
            }
            else
            {
                Debug.Log("uh?");
            }
        }
        //else
        //{
        //    Debug.Log("distance is " + Vector3.Distance(gameObject.transform.position, _OppositeWall.transform.position));
        //}

        if (collision.collider.gameObject == _OppositeWall)
        {
            _IsSprung = false;
        }
    }

    public void Trigger()
    {
        //if (_IsActivated)
        {
            Spring();
        }
    }

    public void Spring()
    {
        _IsSprung = true;
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
