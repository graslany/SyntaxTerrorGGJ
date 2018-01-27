using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevierBehaviour : MonoBehaviour {
    bool _Activated;
    Animator _Animator;
	// Use this for initialization
	void Start () {
        _Activated = false;
        _Animator = GetComponent<Animator>();
    }

    void PlayLeverAnimation(bool reverse = false)
    {
        if (reverse)
        {
            _Animator.Play("LevierDisabled");
        }
        else
        {
            _Animator.Play("LevierActivated");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayLeverAnimation(_Activated);
            _Activated = !_Activated;
            Debug.Log(_Activated);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
