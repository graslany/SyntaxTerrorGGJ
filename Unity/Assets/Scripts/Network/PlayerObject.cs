using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour {

	protected virtual void Awake() {
	}

	public void OnSecene1ColliderEnter() {
		CmdScene1ColliderEnter ();
	}

	public void OnSecene1ColliderExit() {
		CmdScene1ColliderExited ();
	}

	[Command]
	private void CmdScene1ColliderEnter() {
		RpcScene1ColliderEnter ();
	}

	[Command]
	private void CmdScene1ColliderExited() {
		RpcScene1ColliderExited ();
	}

	[ClientRpc]
	public void RpcScene1ColliderEnter() {
		Player2Display p2 = GameObject.FindObjectOfType<Player2Display> ();
		if (p2 != null)
			p2.Player1EnteredZone ();
	}

	[ClientRpc]
	public void RpcScene1ColliderExited() {
		Player2Display p2 = GameObject.FindObjectOfType<Player2Display> ();
		if (p2 != null)
			p2.Player1ExitedZone ();
	}
}
