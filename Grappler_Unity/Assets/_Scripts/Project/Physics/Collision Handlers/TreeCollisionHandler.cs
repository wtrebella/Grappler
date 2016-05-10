using UnityEngine;
using System.Collections;

public class TreeCollisionHandler : CollisionHandler {
	public override void HandleTriggerEnter(Rigidbody2D rigid, Collider2D collider) {
		base.HandleTriggerEnter(rigid, collider);

		if (!player.isSkating) {
			ScreenShaker.instance.ShakeLerp(0.2f);

			ReduceVelocity();
		}
	}
}