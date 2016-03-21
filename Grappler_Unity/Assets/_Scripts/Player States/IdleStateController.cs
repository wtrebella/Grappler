using UnityEngine;
using System.Collections;

public class IdleStateController : PlayerStateController {
	private void Awake() {
		BaseAwake();
		state = Player.PlayerStates.Idle;
	}

	public override void EnterState() {
		base.EnterState();

		player.kinematicSwitcher.SetKinematic();
		player.playerAnimator.PlayIdleAnimations();
		ResetRotation();
	}
	
	public override void TouchDown() {
		base.TouchDown();
		player.grapplingStateController.ConnectGrapplerToHighestAnchorable();
	}

	public void ResetRotation() {
		player.body.transform.localRotation = Quaternion.identity;
		player.feet.transform.localRotation = Quaternion.identity;
	}
}
