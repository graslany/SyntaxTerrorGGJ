using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.ThirdPerson;

public class ButtonBehavior : NetworkBehaviour
{

    [SerializeField] float _MoveSpeed;
    float _MaxHeight;
    [SerializeField] float _MinHeight;
    [SerializeField] bool _OnlyOnPressure;
    [SerializeField] List<GameObject> _Triggered;
    [SerializeField] List<string> _TriggerTags;
    [SerializeField] bool _RequiresManualActivation;
    bool _GoingDown;
    bool _GoingUp;
    // Use this for initialization
    void Start()
    {
        _MaxHeight = transform.localPosition.y;
        _GoingDown = false;
        _GoingUp = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (_TriggerTags.Contains(other.gameObject.tag))
        {
            if(!_RequiresManualActivation
                || Input.GetKeyDown(KeyCode.E))
            {
                _GoingDown = true;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (_TriggerTags.Contains(other.gameObject.tag))
        {
            if (!_RequiresManualActivation
               || Input.GetKeyDown(KeyCode.E))
            {
                _GoingDown = true;
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (_TriggerTags.Contains(other.gameObject.tag) && other.gameObject.GetComponent<ThirdPersonCharacterCustom>().isLocal)
        {
            _GoingUp = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 buttonScale = transform.localPosition;
        if (_GoingDown)
        {
            if (buttonScale.y > _MinHeight)
            {
                buttonScale.y = System.Math.Max(buttonScale.y - _MoveSpeed, _MinHeight);
                if (buttonScale.y == _MinHeight)
                {
                    for (int i = 0; i < _Triggered.Count; i++)
                    {
                        
                        var danger = _Triggered[i].GetComponent<TrapInterface>();
                        if (danger != null)
                        {
                            Debug.Log("evbzh");
                            danger.Trigger();
                        }
                    }

                    var distantTrigger = gameObject.GetComponent<BooleanValueSourceMB>();
                    if (distantTrigger != null)
                    {
                        distantTrigger.Variable.StoredValue = true;
                    }
                    _GoingDown = false;
                }
            }
            transform.localPosition = buttonScale;
            return;
        }

        if (_GoingUp)
        {
            if (buttonScale.y < _MaxHeight)
            {
                buttonScale.y = System.Math.Min(buttonScale.y + _MoveSpeed, _MaxHeight);
                if (buttonScale.y == _MaxHeight)
                {
                    _GoingUp = false;
                }
            }

            for (int i = 0; i < _Triggered.Count; i++)
            {
                var danger = _Triggered[i].GetComponent<TrapInterface>();
                if (danger != null
                    && _OnlyOnPressure)
                {
                    danger.UnTrigger();
                }
            }

            var distantTrigger = gameObject.GetComponent<BooleanValueSourceMB>();
            if (distantTrigger != null)
            {
                distantTrigger.Variable.StoredValue = false;
            }
            transform.localPosition = buttonScale;
        }
    }
}
