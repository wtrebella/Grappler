using UnityEngine;
using System.Collections;

public class OnGroundStateController : PlayerStateController {
	[SerializeField] private PlayerAnimator playerAnimator;
	[SerializeField] private Rigidbody2DStopper rigidStopper;

	private void Awake() {
		base.BaseAwake();
		state = Player.PlayerStates.OnGround;
	}

	public override void EnterState() {
		base.EnterState();
		StopRigidbody();
	}
	
	public override void TouchDown() {
		base.TouchDown();
		FreeRigidbody();
		player.grapplingStateController.ConnectGrapplerToHighestAnchorable();
	}

	public void StopRigidbody() {
		rigidStopper.StartStoppingProcess();
		playerAnimator.PlayOnGroundAnimations();
		player.grapplingStateController.DisconnectGrapplerIfPossible();
	}

	public void FreeRigidbody() {
		rigidStopper.Cancel();
	}
}