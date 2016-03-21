using UnityEngine;
using System.Collections;

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
	
	public override void TouchDown() {
		base.TouchDown();
		player.grapplingStateController.ConnectGrapplerToHighestAnchorable();
	}
}
