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
		if (Input.GetMouseButtonDown(0)) player.grapplingController.ConnectGrapplerToHighestAnchorable();
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
}
