using UnityEngine;
using System.Collections;

public class MountainCollisionHandler : CollisionHandler {
	public override void HandleCollisionEnter(Rigidbody2D rigid, Collision2D collision) {
		base.HandleCollisionEnter(rigid, collision);

		ShakeScreen(rigid, collision);
		if (player.isGrappling) player.SetState(Player.PlayerStates.Falling);
//		ReduceVelocity();
	}
}