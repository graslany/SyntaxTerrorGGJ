﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrapBase : MonoBehaviour, ITrap {

	[Tooltip("Etat du piège")]
	[SerializeField]
	private TrapState state = TrapState.Inactive;
	public TrapState State {
		get {
			return state;
		}
		private set {
			TrapState previousState = state;
			if (previousState != value) {
				state = value;
				OnStateChanged (previousState, state);
			}
		}
	}

	TrapState ITrap.State {
		get {
			return state;
		}
	}

	/// <summary>
	/// Active le piège, qui pourra ensuite être déclenché.
	/// </summary>
	public void Arm()
	{
		State = TrapState.Armed;
	}

	/// <summary>
	/// Désactive le piège, qui ne pourra plus être déclenché.
	/// </summary>
	public void Disarm()
	{
		State = TrapState.Inactive;
	}

	/// <summary>
	/// Déclenche le piège.
	/// </summary>
	public void Spring()
	{
		if (State == TrapState.Armed)
			State = TrapState.Triggered;
	}

	protected abstract void OnStateChanged(TrapState previousState, TrapState newState);
}