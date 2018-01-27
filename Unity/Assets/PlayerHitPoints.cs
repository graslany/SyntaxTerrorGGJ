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
    int currentHitPoints;
	// Use this for initialization
	void Start ()
    {
        currentHitPoints = _MaxHitPoints;
    }

    public void takeDamage(int aouch, DamageSource source = DamageSource.Default)
    {
        var Blood = gameObject.GetComponentInChildren<ParticleSystem>();
        if (Blood != null)
        {
            Blood.Play();
        }
        currentHitPoints -= aouch;
        if(currentHitPoints <= 0)
        {//oh no we dead
            switch(source)
            {
                case DamageSource.Suffocation:
                case DamageSource.Crushed:
                case DamageSource.Impaled:
                case DamageSource.Burned:
                case DamageSource.Default:
                    if(gameObject.GetComponent<DeathScript>())
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
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
