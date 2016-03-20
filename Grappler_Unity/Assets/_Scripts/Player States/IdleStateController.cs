using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class IdleStateController : PlayerStateController {
	[SerializeField] private IdleState idleState;

	private void Awake() {
		BaseAwake();
		state = Player.PlayerStates.Idle;
	}

	public override void EnterState() {
		base.EnterState();
		player.kinematicSwitcher.SetKinematic();
		player.playerAnimator.PlayIdleAnimations();
		idleState.ResetRotation();
	}
	
	public override void ExitState() {
		base.ExitState();
	}
	
	public override void UpdateState() {
		base.UpdateState();
	}
	
	public override void FixedUpdateState() {
		base.UpdateState();
	}
	
	public override void LeftSwipe() {
		base.LeftSwipe();
	}
	
	public override void RightSwipe() {
		base.RightSwipe();
	}
	
	public override void UpSwipe() {
		base.UpSwipe();
	}
	
	public override void DownSwipe() {
		base.DownSwipe();
	}
	
	public override void Tap() {
		base.Tap();
	}

	public override void TouchUp() {
		base.TouchUp();
	}
	
	public override void TouchDown() {
		base.TouchDown();
		player.grapplingStateController.ConnectGrapplerToHighestAnchorable();
	}

	public override void LeftTouchDown() {
		base.LeftTouchDown();
	}
	
	public override void LeftTouchUp() {
		base.LeftTouchUp();
	}
	
	public override void RightTouchDown() {
		base.RightTouchDown();
	}
	
	public override void RightTouchUp() {
		base.RightTouchUp();
	}
}
