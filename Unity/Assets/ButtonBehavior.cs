﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour {

    [SerializeField] float _MoveSpeed;
    float _MaxHeight;
    [SerializeField] float _MinHeight;
    [SerializeField] List<string> _TriggerTags;
    bool _GoingDown;
    bool _GoingUp;
    public Piege piege;
	// Use this for initialization
	void Start ()
    {
        _MaxHeight = transform.position.y;
        _GoingDown = false;
        _GoingUp = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (_TriggerTags.Contains(other.gameObject.tag))
        {
            _GoingDown = true;
            piege.Activer();
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (_TriggerTags.Contains(other.gameObject.tag))
        {
            _GoingUp = true;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        Vector3 buttonScale = transform.position;
        if(_GoingDown)
        { 
            if (buttonScale.y > _MinHeight)
            {
                buttonScale.y = System.Math.Max(buttonScale.y - _MoveSpeed, _MinHeight);
                if(buttonScale.y == _MinHeight)
                {
                    gameObject.GetComponent<BooleanValueSourceMB>().Variable.StoredValue = true;
                    _GoingDown = false;
                }
            }
            transform.position = buttonScale;
            return;
        }

        if (_GoingUp)
        {
            if (buttonScale.y < _MaxHeight)
            {
                buttonScale.y = System.Math.Min(buttonScale.y + _MoveSpeed, _MaxHeight);
                if(buttonScale.y == _MaxHeight)
                {
                    _GoingUp = false;
                }
            }
            gameObject.GetComponent<BooleanValueSourceMB>().Variable.StoredValue = false;
            transform.position = buttonScale;
        }



    }
}
