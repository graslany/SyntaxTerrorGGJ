using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiketrap : MonoBehaviour,TrapInterface {
    [SerializeField] bool _IsActivated;
    [SerializeField] int _Damage;
    Vector3 pos;
    //public GameObject _spritePrefab;
    // Use this for initialization
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != pos && !_IsActivated)
            transform.position = pos;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            var HitPointScript = other.gameObject.GetComponent<PlayerHitPoints>();
            if (HitPointScript != null)
            {
                HitPointScript.takeDamage((int)_Damage, DamageSource.Burned);
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
        Vector3 refpos = new Vector3(pos.x, pos.y + 0.3f, pos.z);
        if (transform.position != refpos)
        {
            transform.position = refpos;
        }
    }

    public void Spring()
    {
        Vector3 p = new Vector3(pos.x, pos.y + 0.3f, pos.z);
        if (transform.position != p)
            transform.position = p;
    }

    public void Deactivate()
    {
        _IsActivated = false;
    }

    public void Reactivate()
    {
        _IsActivated = true;
    }

}
