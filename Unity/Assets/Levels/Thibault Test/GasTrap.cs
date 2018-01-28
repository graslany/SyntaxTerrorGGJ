using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GasTrap : NetworkBehaviour, TrapInterface
{

    [SerializeField] bool _IsActivated;
    [SerializeField] List<ParticleSystem> _SprinklerPrefab;
    [SerializeField] float _Damage;
    bool _IsSprung = false;

    float _LastTimeCheck;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_IsSprung)
        {
            bool _IsPlaying = false;
            foreach (var sprinkle in _SprinklerPrefab)
            {
                if (sprinkle.isPlaying)
                {
                    _IsPlaying = true;
                }
            }
            if (!_IsPlaying)
            {
                _IsSprung = false;
            }
        }
        if (!_IsActivated)
        {
            foreach (var sprinkle in _SprinklerPrefab)
            {
                sprinkle.Stop();
            }
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (_IsActivated && _IsSprung)
        {
            var Player = coll.transform.GetComponent<GameObject>();
            if (Player != null)
            {
                var HitPointScript = Player.GetComponent<PlayerHitPoints>();
                if (HitPointScript != null)
                {
                    HitPointScript.takeDamage((int)_Damage, DamageSource.Suffocation);
                }
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
        foreach (var sprinkle in _SprinklerPrefab)
        {
            sprinkle.Stop();
        }
    }

    public void Spring()
    {
        Debug.Log("SPRING");
        _LastTimeCheck = Time.time;
        _IsSprung = true;
        foreach (var sprinkle in _SprinklerPrefab)
        {
            sprinkle.Play();
        }
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
