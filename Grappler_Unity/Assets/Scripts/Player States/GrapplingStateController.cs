using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Player))]
public class GrapplingStateController : PlayerStateController {
	[SerializeField] private GrapplingState grappling;
	[SerializeField] private AnchorableFinder anchorableFinder;

	public void ConnectGrapplerIfPossible() {
		Anchorable anchorable;
		
		if (anchorableFinder.FindAnchorableInCircle(out anchorable)) {
			ConnectGrapplerIfPossible(anchorable);
		}
	}

	public void ConnectGrapplerToHighestAnchorable() {
		if (!grappling.ReadyToConnect()) return;
		Anchorable anchorable;
		if (anchorableFinder.FindAnchorableInScreenOverlap(out anchorable)) {
			ConnectGrapplerIfPossible(anchorable);
			player.SetState(Player.PlayerStates.Grappling);
		}
	}
	
	public bool DisconnectGrapplerIfPossible() {
		if (grappling.ReadyToDisconnect()) {
			grappling.ReleaseGrapple();
			return true;
		}
		else return false;
	}
	
	public void ConnectGrapplerIfPossible(Anchorable anchorable) {
		if (grappling.ReadyToConnect()) {
			grappling.Connect(anchorable);
			player.SetState(Player.PlayerStates.Grappling);
		}
	}

	public void FinishCooldown() {
		grappling.FinishCooldown();
	}

	public override void EnterState() {
		player.playerAnimator.PlayGrapplingAnimations();
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

	public override void HandleLeftTouchDown() {
		
	}
	
	public override void HandleLeftTouchUp() {
		if (DisconnectGrapplerIfPossible()) player.SetState(Player.PlayerStates.Falling);
	}
	
	public override void HandleRightTouchDown() {
//		player.SetState(Player.PlayerStates.Kicking);
	}
	
	public override void HandleRightTouchUp() {
		if (DisconnectGrapplerIfPossible()) player.SetState(Player.PlayerStates.Falling);
	}
}
