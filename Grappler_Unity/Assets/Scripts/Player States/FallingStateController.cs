using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class FallingStateController : PlayerStateController {
	[SerializeField] private AnchorableFinder anchorableFinder;

	public override void EnterState() {
		player.playerAnimator.PlayFallingAnimations();
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
		player.SetState(Player.PlayerStates.Kicking);
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
		player.grapplingController.ConnectGrapplerToHighestAnchorable();
	}
}
