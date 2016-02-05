using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Player))]
public class GrapplingStateController : PlayerStateController {
	[SerializeField] private GrapplingState grappling;
	[SerializeField] private AnchorableFinder anchorableFinder;

	private void Awake() {
		BaseAwake();
		playerState = Player.PlayerStates.Grappling;
	}

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
			if (ConnectGrapplerIfPossible(anchorable)) {
				player.SetState(Player.PlayerStates.Grappling);
			}
		}
	}
	
	public bool DisconnectGrapplerIfPossible() {
		if (grappling.ReadyToDisconnect()) {
			grappling.ReleaseGrapple();
			return true;
		}
		else return false;
	}
	
	public bool ConnectGrapplerIfPossible(Anchorable anchorable) {
		if (grappling.ReadyToConnect()) {
			grappling.Connect(anchorable);
			player.SetState(Player.PlayerStates.Grappling);
			return true;
		}
		return false;
	}

	public void FinishCooldown() {
		grappling.FinishCooldown();
	}

	public override void EnterState() {
		base.EnterState();
		player.playerAnimator.PlayGrapplingAnimations();
	}
	
	public override void ExitState() {
		base.ExitState();
	}
	
	public override void UpdateState() {
		base.UpdateState();
	}
	
	public override void FixedUpdateState() {
		base.FixedUpdateState();
	}
	
	public override void HandleLeftSwipe() {
		base.HandleLeftSwipe();
	}
	
	public override void HandleRightSwipe() {
		base.HandleRightSwipe();
	}
	
	public override void HandleUpSwipe() {
		base.HandleUpSwipe();
	}
	
	public override void HandleDownSwipe() {
		base.HandleDownSwipe();
	}
	
	public override void HandleTap() {
		base.HandleTap();
	}
	
	public override void HandleTouchUp() {
		base.HandleTouchUp();
		if (DisconnectGrapplerIfPossible()) player.SetState(Player.PlayerStates.Falling);
	}
	
	public override void HandleTouchDown() {
		base.HandleTouchDown();
	}
	
	public override void HandleLeftTouchDown() {
		base.HandleLeftTouchDown();
	}
	
	public override void HandleLeftTouchUp() {
		base.HandleLeftTouchUp();
	}
	
	public override void HandleRightTouchDown() {
		base.HandleRightTouchDown();
	}
	
	public override void HandleRightTouchUp() {
		base.HandleRightTouchUp();
	}
}
