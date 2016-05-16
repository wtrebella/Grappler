using UnityEngine;
using System.Collections;

public class IdleStateController : PlayerStateController {
	private void Awake() {
		BaseAwake();
		state = Player.PlayerStates.Idle;
	}

	public override void EnterState() {
		base.EnterState();

		player.rigidbodyAffecterGroup.SetKinematic();
		player.playerAnimator.PlayIdleAnimations();
		ResetRotation();
	}
	
	public override void TouchDown() {
		base.TouchDown();

	}

	public override void RightSwipe() {
		base.RightSwipe();

		if (player.grapplingManager.Connect()) player.SetState(Player.PlayerStates.Grappling);
	}

	public void ResetRotation() {
		player.body.transform.localRotation = Quaternion.identity;
		player.feet.transform.localRotation = Quaternion.identity;
	}

	public override void UpdateState() {
		base.UpdateState();

		if (Input.GetKeyDown(KeyCode.RightArrow)) Grapple();
	}

	private void Grapple() {
		if (player.grapplingManager.Connect()) player.SetState(Player.PlayerStates.Grappling);
	}
}
