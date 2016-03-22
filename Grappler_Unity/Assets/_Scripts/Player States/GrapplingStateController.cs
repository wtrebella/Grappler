using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class GrapplingStateController : PlayerStateController {
	[SerializeField] private RockSlide rockSlide;
	[SerializeField] private AirTimeTimer airTimeTimer;
	[SerializeField] private float horizontalGrappleBoost = 25;

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
		if (player.grapplingManager.Disconnect()) player.SetState(Player.PlayerStates.Falling);
	}

	public override void FixedUpdateState() {
		base.FixedUpdateState();

		player.rigidbodyAffecterGroup.AddForce(new Vector2(horizontalGrappleBoost, -100), ForceMode2D.Force);
	}
}
