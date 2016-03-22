using UnityEngine;
using System.Collections;

public class OnGroundStateController : PlayerStateController {
	private void Awake() {
		base.BaseAwake();
		state = Player.PlayerStates.OnGround;
	}

	public override void EnterState() {
		base.EnterState();
		player.rigidbodyAffecterGroup.StopMoving();
		player.playerAnimator.PlayOnGroundAnimations();
		player.grapplingManager.Disconnect();
	}
	
	public override void TouchDown() {
		base.TouchDown();
		player.rigidbodyAffecterGroup.AllowMovement();
		if (player.grapplingManager.Connect()) player.SetState(Player.PlayerStates.Grappling);
	}
}