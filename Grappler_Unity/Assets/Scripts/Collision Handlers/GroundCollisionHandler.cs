using UnityEngine;
using System.Collections;

public class GroundCollisionHandler : CollisionHandler {
	private bool hasGrappledSinceHittingGround = true;

	public override void HandleCollision(Collision2D collision) {
		base.HandleCollision(collision);

		if (player.IsOnGround()) return;
		if (!hasGrappledSinceHittingGround) return;

		hasGrappledSinceHittingGround = false;
		ScreenShaker.instance.CollisionShake(collision.relativeVelocity.magnitude);
		player.SetState(Player.PlayerStates.OnGround);
	}

	private void Awake() {
		BaseAwake();
		player = GetComponent<Player>();
		player.SignalEnteredGrapplingState += HandleEnteredGrapplingState;
	}

	private void HandleEnteredGrapplingState() {
		hasGrappledSinceHittingGround = true;
	}
}
