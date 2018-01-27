using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour, TrapInterface
{
    [SerializeField] bool _IsActivated;
    LineRenderer _LaserTrack;
    bool _IsSprung;
    // Use this for initialization
    void Start()
    {
        _LaserTrack = GetComponent<LineRenderer>();
        _LaserTrack.SetPosition(0, transform.position);
        _LaserTrack.SetPosition(1, transform.position);
        _IsSprung = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if (_LaserTrack != null
            && _IsSprung)
        {
            _LaserTrack.SetPosition(0, transform.position);
            RaycastHit hasHit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, 1, 0)), out hasHit))
            {
                if (hasHit.collider)
                {
                    _LaserTrack.SetPosition(1, hasHit.point);
                }
            }
            else
            {

            }
        }
    }

    public void Trigger()
    {
        if (_IsActivated)
        {
            Spring();
        }
    }

    public void Spring()
    {
        _LaserTrack = GetComponent<LineRenderer>();
        _IsSprung = true;
    }

    // Update is called once per frame
    public void Deactivate()
    {
        _IsActivated = false;
        _IsSprung = false;
    }

    public void Reactivate()
    {
        _IsActivated = true;
    }
}
