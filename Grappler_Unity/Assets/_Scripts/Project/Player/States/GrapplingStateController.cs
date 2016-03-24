using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class GrapplingStateController : PlayerStateController {
	[SerializeField] private RockSlide rockSlide;
	[SerializeField] private AirTimeTimer airTimeTimer;
	[SerializeField] private Vector2 grappleBoost = new Vector2(25, -100);

	private void Awake() {
		base.BaseAwake();
		state = Player.PlayerStates.Grappling;
	}

	public override void EnterState() {
		base.EnterState();

		airTimeTimer.OnGrapple();
		rockSlide.OnGrapple();

		player.rigidbodyAffecterGroup.ReduceVelocity();
		player.rigidbodyAffecterGroup.SetNonKinematic();
		player.playerAnimator.PlayGrapplingAnimations();
	}
	
	public override void TouchUp() {
		base.TouchUp();
		if (DisconnectPlayer()) SetToFallingState();
	}

	public override void FixedUpdateState() {
		base.FixedUpdateState();
		ApplyGrappleBoost();
	}

	private bool DisconnectPlayer() {
		return player.grapplingManager.Disconnect();
	}

	private void SetToFallingState() {
		player.SetState(Player.PlayerStates.Falling);
	}

	private void ApplyGrappleBoost() {
		player.rigidbodyAffecterGroup.AddForce(grappleBoost, ForceMode2D.Force);

	}
}
