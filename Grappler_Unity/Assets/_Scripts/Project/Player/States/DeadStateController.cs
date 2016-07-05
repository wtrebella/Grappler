using UnityEngine;
using System.Collections;
using System;

public class DeadStateController : PlayerStateController {
	public Action SignalEnterDeadState;

	[SerializeField] private GameCamera cameraMover;
	[SerializeField] private RockSlide rockSlide;

	private void Awake() {
		base.BaseAwake();
		state = Player.PlayerStates.Dead;
	}

	public override void EnterState() {
		base.EnterState();
		player.grapplingManager.Disconnect();
		GrapplingManager.instance.DisableGrappling();
		if (SignalEnterDeadState != null) SignalEnterDeadState();
	}
}
