using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class FallingStateController : PlayerStateController {
	[SerializeField] private AnchorableFinder anchorableFinder;

	private void Awake() {
		BaseAwake();
		playerState = Player.PlayerStates.Falling;
	}

	public override void EnterState() {
		base.EnterState();
		player.playerAnimator.PlayFallingAnimations();
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
	}
	
	public override void HandleTouchDown() {
		base.HandleTouchDown();
	}

	public override void HandleLeftTouchDown() {
		base.HandleLeftTouchDown();
		player.grapplingController.ConnectGrapplerToHighestAnchorable();
	}
	
	public override void HandleLeftTouchUp() {
		base.HandleLeftTouchUp();
	}
	
	public override void HandleRightTouchDown() {
		base.HandleRightTouchDown();
		player.grapplingController.ConnectGrapplerToHighestAnchorable();
	}
	
	public override void HandleRightTouchUp() {
		base.HandleRightTouchUp();
	}
}
