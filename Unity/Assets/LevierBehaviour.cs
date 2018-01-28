using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LevierBehaviour : NetworkBehaviour {
    [SerializeField] List<GameObject> _Triggered;
    bool _Activated;
    Animator _Animator;
	// Use this for initialization
	void Start () {
        _Activated = false;
        _Animator = GetComponent<Animator>();
    }

    private void AnimateLevier(bool state = false)
    {
        if (state)
        {
            _Animator.Play("LevierActivated");
        }
        else
        {
            _Animator.Play("LevierDisabled");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _Activated = !_Activated;
            AnimateLevier(_Activated);
            if (_Triggered != null)
            {
                Debug.Log("LOL");
            }
            for (int i = 0; i < _Triggered.Count; i++)
            {
                var danger = _Triggered[i].GetComponent<TrapInterface>();
                if (danger != null)
                {
                    danger.Trigger();
                }
            }
        }
    }
}
