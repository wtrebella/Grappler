using UnityEngine;
using System.Collections;

public class PausedStateController : PlayerStateController {
	private void Awake() {
		base.BaseAwake();
		state = Player.PlayerStates.Paused;
	}

	public override void EnterState() {
		base.EnterState();
		var rigids = GetComponentsInChildren<Rigidbody2D>();
		foreach (Rigidbody2D rigid in rigids) rigid.isKinematic = true;
	}

	public override void ExitState() {
		base.ExitState();
		var rigids = GetComponentsInChildren<Rigidbody2D>();
		foreach (Rigidbody2D rigid in rigids) rigid.isKinematic = false;
	}
}