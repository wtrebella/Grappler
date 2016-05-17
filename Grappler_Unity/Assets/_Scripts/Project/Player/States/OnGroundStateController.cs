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

		player.SetState(Player.PlayerStates.Dead);
	}
}