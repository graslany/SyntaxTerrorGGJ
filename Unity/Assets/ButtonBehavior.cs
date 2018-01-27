using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour {

    [SerializeField] float _MoveSpeed;
    float _MaxHeight;
    //bool _Activated = false;    // true when button is pushed all the way through
    bool _HasSomethingOverIt;
    //GameObject _ItemOnButton;
	// Use this for initialization
	void Start ()
    {
        _MaxHeight = transform.localScale.y;
        //_Activated = false;
        _HasSomethingOverIt = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.tag == "Player")
        {
            Debug.Log("triggered");
            //_ItemOnButton = other.collider.gameObject;
            other.collider.gameObject.transform.SetParent(transform);
            _HasSomethingOverIt = true;
        }
    }


    void OnCollisionExit(Collision other)
    {
        
        if (other.collider.gameObject.tag == "Player")
        {
            Debug.Log("triggered ex");
            other.collider.gameObject.transform.SetParent(null);
            //_ItemOnButton = null;
            _HasSomethingOverIt = false;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        Vector3 buttonScale = transform.position;
        if(_HasSomethingOverIt)
        {
            if (buttonScale.y > 0)
            {
                buttonScale.y = System.Math.Max(buttonScale.y - _MoveSpeed, 0);
                if(buttonScale.y == 0)
                {
                    //_Activated = true;
                }
            }
            transform.position = buttonScale;
        }

        if (!_HasSomethingOverIt)
        {
            if (buttonScale.y < _MaxHeight)
            {
                buttonScale.y = System.Math.Min(buttonScale.y + _MoveSpeed, _MaxHeight);
            }
            //_Activated = false;
            transform.position = buttonScale;
        }



    }
}
