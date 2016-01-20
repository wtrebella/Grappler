using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class ClimbingRopeStateController : PlayerStateController {
	[SerializeField] private ClimbingRopeState climbingRopeState;

	public override void EnterState() {
		player.playerAnimator.PlayClimbingRopeAnimations();
		climbingRopeState.ClimbUpRope();
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
