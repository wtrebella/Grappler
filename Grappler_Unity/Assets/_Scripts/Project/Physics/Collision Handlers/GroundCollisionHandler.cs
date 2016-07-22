using UnityEngine;
using System.Collections;

public class GroundCollisionHandler : CollisionHandler {
	public override void HandleCollisionEnter(Rigidbody2D rigid, Collision2D collision) {
		base.HandleCollisionEnter(rigid, collision);
		if (player.isDead) return;
		ShakeScreen(rigid, collision);

		if (player.isGrappling) player.SetState(Player.PlayerStates.Falling);
//		player.SetState(Player.PlayerStates.OnGround);

//		ReduceVelocity();
	}
}
