using UnityEngine;
using System.Collections;

public class OnGroundStateController : PlayerStateController {
	[SerializeField] private PlayerAnimator playerAnimator;
	[SerializeField] private OnGroundState onGroundState;

	private void Awake() {
		base.BaseAwake();
		state = Player.PlayerStates.OnGround;
	}

	public override void EnterState() {
		base.EnterState();
		onGroundState.StopRigidbody();
	}
	
	public override void TouchDown() {
		base.TouchDown();
		onGroundState.FreeRigidbody();
		player.grapplingStateController.ConnectGrapplerToHighestAnchorable();
	}
}