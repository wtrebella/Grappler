using UnityEngine;
using System.Collections;
using System;

public class ClimbingController : PlayerComponentController {
	[SerializeField] private Climbing climbing;
	[SerializeField] private AnchorableFinder anchorableFinder;

	public override void EnterState() {
		player.playerAnimator.PlayClimbingAnimations();
		climbing.StartClimbing();
	}
	
	public override void ExitState() {
		climbing.StopClimbing();
	}
	
	public override void UpdateState() {
		
	}
	
	public override void FixedUpdateState() {
		
	}
	
	public override void HandleLeftSwipe() {
		
	}
	
	public override void HandleRightSwipe() {
		player.grapplingController.ConnectGrapplerIfPossible();
	}
	
	public override void HandleUpSwipe() {
		
	}
	
	public override void HandleDownSwipe() {
		
	}
	
	public override void HandleTap() {
		
	}
}
