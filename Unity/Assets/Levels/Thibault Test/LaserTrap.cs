using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour, TrapInterface
{
    [SerializeField] bool _IsActivated;
    [SerializeField] float _Damage;
    LineRenderer _LaserTrack;
    float _LastTimeCheck;
    bool _IsSprung;
    // Use this for initialization
    void Start()
    {
        _LaserTrack = GetComponent<LineRenderer>();
        _LaserTrack.SetPosition(0, transform.position);
        _LaserTrack.SetPosition(1, transform.position);
        _IsSprung = false;
        _LastTimeCheck = Time.time;
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
                    if (hasHit.collider.gameObject.tag == "Player")
                    {
                        GameObject player = hasHit.collider.gameObject;
                        if (player != null)
                        {
                            var HitPointScript = player.GetComponent<PlayerHitPoints>();
                            if (HitPointScript != null)
                            {
                                HitPointScript.takeDamage((int)_Damage, DamageSource.Burned);
                            }

                        }
                    }
                }
            }
            else
            {
                _LaserTrack.SetPosition(1, _LaserTrack.GetPosition(0) + transform.TransformDirection(new Vector3(0, 1, 0)) * 1000);
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

    public void UnTrigger()
    {
        _LaserTrack = GetComponent<LineRenderer>();
        if(_LaserTrack != null)
        {
            _LaserTrack.SetPosition(1, transform.position);
        }
        _IsSprung = false;
    }

    public void Spring()
    {
        _LaserTrack = GetComponent<LineRenderer>();
        if(_LaserTrack == null)
        {    
            _LaserTrack = gameObject.AddComponent<LineRenderer>();
        }
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
