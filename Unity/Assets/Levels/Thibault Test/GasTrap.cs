using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTrap : MonoBehaviour, TrapInterface
{

    [SerializeField] bool _IsActivated;
    [SerializeField] List<ParticleSystem> _SprinklerPrefab;
    [SerializeField] float _TickRate;
    [SerializeField] float _Damage;

    float _LastTimeCheck;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!_IsActivated)
        {
            foreach (var sprinkle in _SprinklerPrefab)
            {
                sprinkle.Stop();
            }
        }
        else
        {
            float currentTime = Time.time;
            if(currentTime - _LastTimeCheck > _TickRate)
            {
                GameObject player = GameObject.Find("Player");
                if (player != null)
                {
                    //player.GetComponent<HitPoint>().HP -= _Damage;
                }
                _LastTimeCheck = currentTime;
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
        _LastTimeCheck = Time.time;
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
