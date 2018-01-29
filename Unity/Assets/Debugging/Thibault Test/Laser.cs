using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    LineRenderer _LaserTrack;
    // Use this for initialization
    void Start ()
    {
        _LaserTrack = GetComponent<LineRenderer>();
        Debug.Log("created + " + _LaserTrack);
    }
	
	// Update is called once per frame
	public void Update ()
    {
        Debug.Log("up");
        if (_LaserTrack != null)
        {
            Debug.Log("upper");
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


}
