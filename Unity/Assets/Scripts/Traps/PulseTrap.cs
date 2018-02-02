using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseTrap : TrapBase
{
    public float _Damage;
    public GameObject _Ring;
    public float _MaxRadius;
    public List<GameObject> _Players;
    public float _Speed;
    public bool _OneShot;

	bool _HasBeenUsed = false;
    GameObject _PulseRing;

	protected override void OnStateChanged (TrapState previousState, TrapState newState)
	{
		if (newState == TrapState.Triggered && !_HasBeenUsed) {
			if (_PulseRing == null)
			{
				if(!_OneShot
					|| !_HasBeenUsed)
				{
					_PulseRing = Instantiate(_Ring, transform.position, transform.rotation);
					_HasBeenUsed = true;
					AudioSource audio = gameObject.GetComponent<AudioSource>();
					if (audio != null)
					{
						audio.Play();
					}
				}
			}
			_HasBeenUsed = true;
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
					if (player.tag == TagNames.Player
                        && player.GetComponent<PlayerHitPoints>() != null)
                    {
                        player.GetComponent<PlayerHitPoints>().TakeDamage(_Damage, DamageSource.Burned);
                    }
                }
            }
        }
    }

}
