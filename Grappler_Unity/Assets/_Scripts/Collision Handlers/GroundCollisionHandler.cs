using UnityEngine;
using System.Collections;

public class GroundCollisionHandler : CollisionHandler {
	private bool hasGrappledSinceHittingGround = true;

	public override void HandleCollisionEnter(Rigidbody2D rigid, Collision2D collision) {
		base.HandleCollisionEnter(rigid, collision);

		if (player.IsOnGround()) return;
		if (!hasGrappledSinceHittingGround) return;

		ShakeScreen(rigid, collision);

		hasGrappledSinceHittingGround = false;
		player.SetState(Player.PlayerStates.OnGround);
	}

	private void Awake() {
		BaseAwake();
		player.SignalEnteredGrapplingState += HandleEnteredGrapplingState;
	}

	private void HandleEnteredGrapplingState() {
		hasGrappledSinceHittingGround = true;
	}
}
