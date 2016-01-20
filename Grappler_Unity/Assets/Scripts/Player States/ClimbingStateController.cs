using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Player))]
public class ClimbingStateController : PlayerStateController {
	[SerializeField] private ClimbingState climbingState;
	[SerializeField] private AnchorableFinder anchorableFinder;

	public override void EnterState() {
		player.playerAnimator.PlayClimbingAnimations();
		climbingState.StartClimbing(player.GetBodyPosition().y);
	}
	
	public override void ExitState() {
		climbingState.StopClimbing();
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
		player.grapplingController.ConnectGrapplerIfPossible();
	}
	
	public override void HandleTap() {
		
	}

	public override void HandleTouchUp() {
		
	}
	
	public override void HandleTouchDown() {
		
	}
}
