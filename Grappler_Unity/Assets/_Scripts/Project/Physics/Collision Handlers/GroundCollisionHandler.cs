using UnityEngine;
using System.Collections;

public class GroundCollisionHandler : CollisionHandler {
	public override void HandleCollisionEnter(Rigidbody2D rigid, Collision2D collision) {
		base.HandleCollisionEnter(rigid, collision);

		ShakeScreen(rigid, collision);
		player.SetState(Player.PlayerStates.OnGround);

		ReduceVelocity();
	}
}
