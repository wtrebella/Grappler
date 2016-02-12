using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class OnGroundStateController : PlayerStateController {
	[SerializeField] private PlayerAnimator playerAnimator;
	[SerializeField] private OnGroundState onGroundState;

	private void Awake() {
		base.BaseAwake();
		playerState = Player.PlayerStates.OnGround;
	}

	public override void EnterState() {
		base.EnterState();
		onGroundState.StopRigidbody();
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
		onGroundState.FreeRigidbody();
		player.grapplingController.ConnectGrapplerToHighestAnchorable();
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