using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Player))]
public class KickingStateController : PlayerStateController {
	[SerializeField] private KickingState kickingState;

	private void Kick() {
		player.grapplingController.DisconnectGrapplerIfPossible();
		player.playerAnimator.PlayKickingAnimations();
		player.trail.Kick();
		kickingState.Kick();
	}

	public override void EnterState() {
		Kick();
	}
	
	public override void ExitState() {
		
	}
	
	public override void UpdateState() {

	}
	
	public override void FixedUpdateState() {

	}

	public override void HandleLeftSwipe() {

	}
	
	public override void HandleRightSwipe() {
		
	}
	
	public override void HandleUpSwipe() {

	}
	
	public override void HandleDownSwipe() {

	}
	
	public override void HandleTap() {

	}

	public override void HandleTouchUp() {

	}
	
	public override void HandleTouchDown() {
		
	}
}
