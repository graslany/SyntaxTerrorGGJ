using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageSource
{
    Suffocation,
    Crushed,
    Impaled,
    Burned,
    Default
}

public class PlayerHitPoints : MonoBehaviour
{
    [SerializeField] int _MaxHitPoints;
    [SerializeField] float _TickRate;

    float _LastTimeHit;
    int currentHitPoints;
	// Use this for initialization
	void Start ()
    {
        currentHitPoints = _MaxHitPoints;
        _LastTimeHit = Time.time;
    }

    public void takeDamage(int aouch, DamageSource source = DamageSource.Default)
    {
        float currentTime = Time.time;
        if (currentTime - _LastTimeHit > _TickRate)
        {
            var Blood = gameObject.GetComponentInChildren<ParticleSystem>();
            if (Blood != null)
            {
                Blood.Play();
            }
            currentHitPoints -= aouch;
            if (currentHitPoints <= 0)
            {//oh no we dead
                switch (source)
                {
                    case DamageSource.Suffocation:
                    case DamageSource.Crushed:
                    case DamageSource.Impaled:
                    case DamageSource.Burned:
                    case DamageSource.Default:
                        if (gameObject.GetComponent<DeathScript>())
                        {
                            gameObject.GetComponent<DeathScript>().DieDieDie();
                        }
                        else
                        {
                            Destroy(gameObject);
                        }
                        break;
                }
            }
            _LastTimeHit = currentTime;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
