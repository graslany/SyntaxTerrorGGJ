using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseTrap : MonoBehaviour, TrapInterface
{
    [SerializeField] bool _IsActivated;
    [SerializeField] int _Damage;
    [SerializeField] GameObject _Ring;
    [SerializeField] float _MaxRadius;
    [SerializeField] List<GameObject> _Players;
    [SerializeField] float _Speed;
    [SerializeField] bool _OneShot;
    bool _HasBeenTriggered = false;
    bool _HasBeenUsed;
    GameObject _PulseRing;

    // Use this for initialization
    void Start()
    {
        _HasBeenUsed = false;

    }

    public void Deactivate()
    {
        _IsActivated = false;
    }

    public void Reactivate()
    {
        _IsActivated = true;
    }

    public void Spring()
    {
        if (_PulseRing == null)
        {
            if(!_OneShot
                || !_HasBeenUsed)
            {
                _PulseRing = Instantiate(_Ring, transform.position, transform.rotation);
                _HasBeenUsed = true;
                
            }
            
        }
    }

    void Update()
    {
        if(_PulseRing != null)
        {
            if (_PulseRing.GetComponent<Primitives>().segmentRadius < _MaxRadius)
            {
                _PulseRing.GetComponent<Primitives>().segmentRadius += _Speed;
                _PulseRing.GetComponent<Primitives>().Torus();
            }
            else
            {
                Destroy(_PulseRing);
                foreach (var player in _Players)
                {
                    if (player.tag == "Player"
                        && player.GetComponent<PlayerHitPoints>() != null)
                    {
                        player.GetComponent<PlayerHitPoints>().takeDamage(_Damage, DamageSource.Burned);
                    }
                }
            }
        }
    }

    public void Trigger()
    {
        if(_IsActivated
            && !_HasBeenTriggered)
        {
            Spring();
            _HasBeenTriggered = true;
        }
    }

    public void UnTrigger()
    {
        _HasBeenTriggered = false;
    }




	
}
