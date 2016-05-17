using UnityEngine;
using System.Collections;
using System;

public class PlayerStateController : WhitStateController {
	protected Player player;

	private void Awake() {
		BaseAwake();
	}

	protected override void BaseAwake() {
		base.BaseAwake();
		player = GetComponentInParent<Player>();
	}

	protected virtual void Grapple() {
		if (player.grapplingManager.Connect()) player.SetState(Player.PlayerStates.Grappling);
	}

	protected void SetToFallingState() {
		player.SetState(Player.PlayerStates.Falling);
	}
}
