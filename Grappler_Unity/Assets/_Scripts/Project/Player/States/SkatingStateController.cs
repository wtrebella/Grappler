using UnityEngine;
using System.Collections;

public class SkatingStateController : PlayerStateController {
	private void Awake() {
		BaseAwake();
		state = Player.PlayerStates.Skating;
	}

	public override void EnterState() {
		base.EnterState();

		player.rigidbodyAffecterGroup.SetKinematic();
		player.playerAnimator.PlayIdleAnimations();
		ResetRotation();
	}
	
	public override void TouchDown() {
		base.TouchDown();
		if (player.grapplingManager.Connect()) player.SetState(Player.PlayerStates.Grappling);
	}

	public void ResetRotation() {
		player.body.transform.localRotation = Quaternion.identity;
		player.feet.transform.localRotation = Quaternion.identity;
	}
}
