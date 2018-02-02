using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrap : TrapBase {
	
    bool isactive = false;

	protected override void OnStateChanged (TrapState previousState, TrapState newState)
	{
		gameObject.SetActive(!isactive);
		isactive = !isactive;
	}
}
